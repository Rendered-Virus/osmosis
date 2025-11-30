using System;
using System.Collections;
using UnityEngine;

public class CryingChild : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private GameObject _tears;
    [SerializeField] private GameObject _cat;

    private IEnumerator Start()
    {
        _anim = GetComponent<Animator>();
        
        yield return new WaitForEndOfFrame();
        
        if(GetComponent<DialogueSpeaker>().currentDialogueIndex == 2)
            StopCrying();
    }

    public void StopCrying()
    {
        _anim.CrossFade("nocry",0);
        _tears.SetActive(false);
        _cat.SetActive(true);
    }
}
