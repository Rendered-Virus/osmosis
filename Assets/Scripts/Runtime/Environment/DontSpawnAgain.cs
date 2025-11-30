using System;
using UnityEngine;

public class DontSpawnAgain : MonoBehaviour
{
    [SerializeField] private int _key;

    private void Start()
    {
        if (PlayerPrefs.HasKey(_key.ToString()))
        {
            Destroy(gameObject);
        }
    }

}