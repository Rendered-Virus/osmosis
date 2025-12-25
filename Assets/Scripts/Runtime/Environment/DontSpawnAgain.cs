using System;
using UnityEngine;

public class DontSpawnAgain : MonoBehaviour
{
    [SerializeField] private int _key;
    [SerializeField] private bool _reverse;

    private void Start()
    {
        var key = PlayerPrefs.HasKey(_key.ToString());
        key = _reverse ? !key : key;
        
        if (key)
        {
            Destroy(gameObject);
        }
    }

}