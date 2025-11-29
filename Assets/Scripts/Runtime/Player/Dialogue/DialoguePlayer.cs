using System;
using System.Collections;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialoguePlayer : Singleton<DialoguePlayer>
{
   [SerializeField] private float _timeBetweenLetters;
   [SerializeField] private TextMeshProUGUI _textObject;
   [SerializeField] private RectTransform _textBox;
   [SerializeField] private Vector2 _offset;
   
   [SerializeField] private UnityEvent _onDialogueStart,  _onDialogueEnd;
   private bool _playing;
   public void PlayDialogue(Dialogue dialogue, Vector2 pos, Action onSuccess)
   {
      if(_playing) return;
      
      _textBox.gameObject.SetActive(true);

      var offset = _offset * (Camera.main.WorldToViewportPoint(pos).x > 0.5f ? -1 : 1); 
      var textPos  = (Vector3)pos + new Vector3(offset.x,_offset.y);
      textPos.z = 2;
      _textBox.transform.position = textPos;
      
      _onDialogueStart?.Invoke();
      StartCoroutine(DialogueCoroutine(dialogue, onSuccess));
      
   }
   private IEnumerator DialogueCoroutine(Dialogue dialogue, Action onSuccess)
   {
      _playing = true;
      string[] lines = dialogue.lines;
      bool success = true;
      
      if (dialogue.hasCondition)
      {
         success = DialogueConditions.Instance.IsConditionMet(dialogue.condition);
         lines = success ? dialogue.lines : dialogue.linesIfNotCondition;
      }
      foreach (var line in lines)
      {
         _textObject.text = line;
         _textObject.maxVisibleCharacters = 0;
         
         for (int i = 0; i <= line.Length; i++)
         {
            _textObject.maxVisibleCharacters = i;
            yield return new WaitForSeconds(_timeBetweenLetters);
         }
         yield return new WaitUntil(()=> Input.GetKeyDown(KeyCode.Space));
      }
      _textBox.gameObject.SetActive(false);
      
      if(success)
         onSuccess?.Invoke();
      _onDialogueEnd?.Invoke();
      
      _playing = false;
   }
}

[System.Serializable]
public class Dialogue
{
   [TextArea] public string[] lines;
   public bool hasCondition;
   [ShowIf("hasCondition")] public string condition;
   [ShowIf("hasCondition")] [TextArea] public string[] linesIfNotCondition;
}

