using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

public class Timer : IDisposable
{
    private bool      _cancelled;
    private float     _rawTime;
    private Coroutine _coroutine;
    private MonoDummy _mono;
    private string    _name;
    private bool      _quitting;

    public Timer()
    {
        _cancelled = false;
        _rawTime = 0;
        _name = $"~Timer_{GetHashCode()}";
        
        var obj = new GameObject()
        {
            name = _name,
            hideFlags = HideFlags.HideInHierarchy
        };
        _mono = obj.AddComponent<MonoDummy>();

        Application.quitting += () => _quitting = true;
    }
    
    
    
    public float RawTime => _rawTime;
    
    public int   Seconds => (int) _rawTime % 1000;
    
    public int   Minutes => Seconds / 60;
    
    public int   Hours   => Minutes / 60;
    


    public void Start()
    {
        _rawTime = 0;
        _coroutine = _mono.StartCoroutine(Tick());
    }

    public void Stop()
    {
        _cancelled = true;
        _mono.StopCoroutine(_coroutine);
    }
    
    
    public void Remove()
    {
        if(!_quitting)
            Object.Destroy(_mono.gameObject);
        
        Dispose();
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
    
    
    private IEnumerator Tick()
    {
        while (!_cancelled)
        {
            _rawTime += Time.deltaTime;
            yield return null;
        }
    }
    
    
    
    // Пустышка для возможности использовать функционал MonoBehaviour
    private sealed class MonoDummy : MonoBehaviour {}
}