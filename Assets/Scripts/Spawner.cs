using System;
using System.Collections.Generic;
using Data;
using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour
{  
    #pragma warning disable CS0649
                     private static Random        _random = new Random();
    [SerializeField] private        PlainObject[] _prefabs;
                     private bool                 _namesLoaded;
                     private string[]             _names;
    #pragma warning restore CS0649


    public PlainObject SpawnObject(Vector3 spawnPoint)
    {
        var obj = Instantiate(_prefabs[_random.Next(0, _prefabs.Length)], spawnPoint, Quaternion.identity);

        if (_namesLoaded)
            obj.gameObject.name = _names[_random.Next(0, _names.Length)];

        return obj;
    }
    
    
    
    private void Awake()
    {
        var loader = FindObjectOfType<BundleLoader>();
        loader.BundleLoaded += OnBundleLoaded;

        var namesLoader = FindObjectOfType<ObjectNames>();
        namesLoader.NamesLoaded += OnNamesLoaded;
    }

    private void OnNamesLoaded(ObjectNames obj)
    {
        _namesLoaded = true;
        _names = obj.Names;
    }

    private void OnBundleLoaded(BundleLoader obj)
    {
        var prefabs = new List<PlainObject>();
        foreach (var bundlePref in obj.Prefabs)
        {
            prefabs.Add(bundlePref.GetComponent<PlainObject>());
        }
        
        _prefabs = prefabs.ToArray();
    }
}
