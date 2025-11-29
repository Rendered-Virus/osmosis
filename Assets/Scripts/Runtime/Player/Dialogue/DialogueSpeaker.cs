using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class DialogueSpeaker : MonoBehaviour
{
    public Dialogue[] dialogues;
    private int _currentDialogueIndex;

    public void PlayDialogue()
    {
        DialoguePlayer.Instance.PlayDialogue(dialogues[_currentDialogueIndex],transform.position, ()=> NextDialogue());
    }

    private void NextDialogue()
    {
        _currentDialogueIndex = Mathf.Clamp(_currentDialogueIndex + 1, 0, dialogues.Length - 1);
    }
    
}
