using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TimeDisplay : MonoBehaviour
{
    private Text _text;
    private Timer _timer;
    
    private void Awake()
    {
        _text = GetComponent<Text>();
        _timer = new Timer();
    }
    
    private void Start()
    {
        _timer.Start();
    }

    
    private void Update()
    {
        _text.text = _timer.Seconds.ToString();
        
        if(_timer.Seconds == 20)
            _timer.Stop();
    }
}
