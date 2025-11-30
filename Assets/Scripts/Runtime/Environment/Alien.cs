using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Alien : MonoBehaviour
{
    [SerializeField] private GameObject _alienModel;
    [SerializeField] private UnityEvent _onComplete;
    [SerializeField] private float _delay;
    [SerializeField] private GameObject _cowModel;
    
    private void Start()
    {
        var index = SaveDataManager.Instance.LoadData().dialogueIndices[GetComponentInChildren<DialogueSpeaker>().speakerID];
        if (index == 2)
        {
            Destroy(gameObject);
        }
    }

    public void TakeOff()
    {
        _alienModel.SetActive(false);
        GetComponent<Animator>().CrossFade("takeoff",0);
        Invoke(nameof(TakenOff), _delay);
    }

    private void TakenOff()
    {
        _onComplete.Invoke();
    }

    public void TurnOnCow()
    {
        if(!DialogueConditions.Instance.IsConditionMet(2))return;
        
        _cowModel.SetActive(true);
        
        PlayerHat.Instance.RemoveHat();
    }
}
