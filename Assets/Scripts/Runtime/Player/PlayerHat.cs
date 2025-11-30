using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerHat : Singleton<PlayerHat>
{
    [SerializeField] private Transform _hatHolder;
    [SerializeField] private GameObject[] _hats;
    
    public int currentHat;

    private void Start()
    {
        var data = SaveDataManager.Instance.LoadData();
        if(data != null)
            SetHat(data.hat);
    }

    public void SetHat(int hat)
    {
        currentHat = hat;
        UpdateHat();
        PlayerPrefs.SetInt("hat", currentHat);
    }

    private void UpdateHat()
    {
        for (int i = 0; i < _hats.Length; i++)
        {
            _hats[i].SetActive(i == currentHat);
        }
    }

    public void RemoveHat()
    {
        currentHat = -1;
        UpdateHat();
    }
}
