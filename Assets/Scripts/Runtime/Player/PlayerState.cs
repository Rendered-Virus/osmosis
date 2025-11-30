using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerState : MonoBehaviour
{
    public int currentState;
    [SerializeField] private GameObject[] _stateModels;
    [SerializeField] private UnityEvent[] _stateEvents;
    
    public void SetState(int playerState)
    {
        currentState  = playerState;
        PlayerPrefs.SetInt("playerState", currentState);
        UpdateState();
    }

    private void Start()
    {
        if (currentState != 0)
        {
            UpdateState();
            return;
        }
        var data = SaveDataManager.Instance.LoadData();
        if (data != null)
        {
            SetState(data.playerState);
        }
    }

    private void UpdateState()
    {
        for (int i = 0; i < _stateModels.Length; i++)
        {
            _stateModels[i].SetActive(i == currentState);
            
            if (i == currentState)
                _stateEvents[i]?.Invoke();
        }
    }
}