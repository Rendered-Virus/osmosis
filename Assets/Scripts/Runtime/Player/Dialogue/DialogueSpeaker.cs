using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class DialogueSpeaker : MonoBehaviour
{
    public int  speakerID;
    public Dialogue[] dialogues;
    public int currentDialogueIndex;

    private void Start()
    {
        if(GameManager.Instance.load)
            Load();
    }

    private void Load()
    {
        var data = SaveDataManager.Instance.LoadData();
        if (data != null)
        {
            if(data.dialogueIndices.Length - 1 > speakerID)
                currentDialogueIndex = data.dialogueIndices[speakerID];
        }
    }

    public void PlayDialogue()
    {
        DialoguePlayer.Instance.PlayDialogue(dialogues[currentDialogueIndex],transform.position, NextDialogue);
    }

    private void NextDialogue()
    {
        currentDialogueIndex = Mathf.Clamp(currentDialogueIndex + 1, 0, dialogues.Length - 1);
    }
    
}
