using System;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _onTrigger;
    [SerializeField] private bool _onlyOnce = true;
    private bool _triggered = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_onlyOnce && _triggered) return;
        
        if (other.CompareTag("Player"))
            _onTrigger?.Invoke();
        _triggered = true;
    }
}
