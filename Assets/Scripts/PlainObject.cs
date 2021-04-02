using System;
using System.Collections;
using Data;
using UniRx;
using UnityEngine;
using SRandom = System.Random;

[RequireComponent(typeof(MeshRenderer))]
public class PlainObject : MonoBehaviour
{
    #pragma warning disable CS0649
    [SerializeField] private uint            _clicks;
    [SerializeField] private int             _switchTime = 15;
                     private Timer           _timer;
                     private MeshRenderer    _renderer;
    [SerializeField] private ColorSet        _colorSet;
    [SerializeField] private ManagedColorSet _managedSet;
                     private Color           _currentColor;
                     private SRandom         _random;
    #pragma warning restore CS0649
    
    
    public uint Clicks => _clicks;

    
    public void IncreaseClicks()
    {
        _clicks++;
        ChangeColorForClicks();
    }
    
    
    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _currentColor = _renderer.material.color;
        _random = new SRandom();
        SetTimer();
        LerpColor();
    }

    private void SetTimer()
    {
        _timer = new Timer();
    }
    
    private void Start()
    {
        _timer.Start();
        Observable.Interval(TimeSpan.FromSeconds(_switchTime)).Subscribe(x => LerpColor());
  
    }
    
    private void OnDestroy()
    {
        _timer.Remove();
    }
    
    private void ChangeColorForClicks()
    {
        foreach (var pair in _managedSet.Pairs)
        {
            if (pair.Clicks == _clicks)
               Observable.FromCoroutine(() => SetColor(pair.Color)).Subscribe();
        }
    }

    private void LerpColor()
    {
        var newColor = _colorSet.Colors[_random.Next(0, _colorSet.Colors.Length)];
        while (newColor == _currentColor)
        {
            newColor = _colorSet.Colors[_random.Next(0, _colorSet.Colors.Length)];
        }
        
        Observable.FromCoroutine(() => SetColor(newColor)).Subscribe();
    }

    private IEnumerator SetColor(Color changeTo)
    {
        float elapsed = 0;
        var goal = 1f;

        while (elapsed < goal)
        {
            _renderer.material.color = Color.Lerp(_currentColor, changeTo, elapsed / goal);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _currentColor = _renderer.material.color;
    }
    
    
    private void OnGUI()
    {
        var position = Camera.main.WorldToScreenPoint(transform.position);
       
        
        var time = TimeSpan.FromSeconds(_timer.RawTime);
        string label = $"Имя: <color=red><b>{gameObject.name}</b></color>\n" +
                       $"Клики: <b>{_clicks}</b>\n" +
                       $"Таймер: <b>{time:hh':'mm':'ss}</b>\n" +
                       $"Интервал: <b>{_switchTime}</b> сек.";
        
        var style = new GUIStyle();
        style.normal.textColor = Color.black;
        style.normal.background = Texture2D.grayTexture;
        style.alignment = TextAnchor.MiddleLeft;
        style.padding = new RectOffset(5, 5, 5, 5);

        var content = new GUIContent(label);
        var labelSize = style.CalcSize(content);

        var _labelRect = new Rect()
        {
            width = labelSize.x,
            height = labelSize.y
        };

        _labelRect.x = position.x - _labelRect.width / 2;
        _labelRect.y = Screen.height - position.y - _labelRect.height * 1.5f;
        
        GUI.Label(_labelRect, content, style);
    }
}
