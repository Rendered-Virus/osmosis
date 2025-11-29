using System;
using UnityEngine;

public class DialogueConditions : Singleton<DialogueConditions>
{
    [SerializeField] private bool _resetConditionsOnStart = true;
    private void Start()
    {
        if (_resetConditionsOnStart)
            ResetConditions();
    }

    private void ResetConditions()
    {
        PlayerPrefs.DeleteAll();
    }

    public bool IsConditionMet(int condition)
    {
        return PlayerPrefs.GetInt(condition.ToString()) == 1;
    }

    public void SetCondition(int condition)
    {
       PlayerPrefs.SetInt(condition.ToString(), condition);
    }
}
