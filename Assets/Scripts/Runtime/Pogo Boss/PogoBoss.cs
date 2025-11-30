using System;
using System.Collections;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PogoBoss : MonoBehaviour
{
   [SerializeField] private Transform _player;
   [SerializeField] private float _timeBetweenAttacks;
   [SerializeField] private Vector2 _startPos;
   [SerializeField] private Transform[] _jumpPoints;
   [SerializeField] private float _jumpHeight;
   [SerializeField] private float _jumpDuration;
   private float _timeToAttack;
   [SerializeField] private Transform _indicator;
   [SerializeField]private float _indicatorScale;
   [SerializeField] private float _indicatorAnimationTime;
   [SerializeField] private float _postJumpDelay;
   private int _currentJumpIndex = 2;
   [SerializeField] private UnityEvent _onDeath;
   private bool _isFighting;
   private bool _isDead;
   
   private void Start()
   {
      GameManager.Instance.OnRespawn.AddListener(BeginFighting);
   }

   public void BeginSpeaking()
   {
      _isFighting = true;
      GetComponent<DialogueSpeaker>().PlayDialogue();
   }
   public void BeginFighting()
   {
      if(!_isFighting || _isDead)return;
      
      _timeToAttack = _timeBetweenAttacks;
      StopAllCoroutines();
      StartCoroutine(JumpLoop());
   }

   private IEnumerator JumpLoop()
   {
      yield return new WaitForSeconds(1.5f);
      
      while (_timeToAttack > 0)
      {
         _currentJumpIndex = Random.value > 0.5f ? _currentJumpIndex - 1 : _currentJumpIndex + 1;
         _currentJumpIndex = math.clamp(_currentJumpIndex, 0, _jumpPoints.Length - 1);
         
         var jumpPos = _jumpPoints[_currentJumpIndex].position;
         transform.DOMoveZ(jumpPos.z,_jumpDuration *0.1f);
         transform.DOMoveX(jumpPos.x,_jumpDuration).SetEase(Ease.InQuad);
         transform.DOMoveY(_jumpHeight,_jumpDuration * 0.5f).SetEase(Ease.OutCubic).OnComplete(()=>
            transform.DOMoveY(jumpPos.y,_jumpDuration * 0.5f)).SetEase(Ease.InQuad);
         
         yield return new WaitForSeconds(_jumpDuration);
         yield return new WaitForSeconds(_postJumpDelay);
      }

      Attack();
   }

   private void Attack()
   {
      _indicator.gameObject.SetActive(true);
      _indicator.localScale = Vector3.one;
      
      _indicator.DOScale(_indicatorScale, _indicatorAnimationTime).OnComplete(() =>
      {
         _indicator.gameObject.SetActive(false);
         var targetPos = _player.position;

         var hit = Physics2D.Raycast(targetPos, Vector2.down, 50, LayerMask.GetMask("Ground"));
         if(hit)
            targetPos.y = hit.point.y;
         if(targetPos.y < 3f)
            targetPos.y = 0f;
         
         transform.DOMoveZ(targetPos.z,_jumpDuration *0.1f);
         transform.DOMoveX(targetPos.x,_jumpDuration).OnComplete(() =>
         {
            if (transform.position.y < 1)
            {
               if(GameManager.Instance.playerDead)
                  return;
               
               _isDead = true;
               GameManager.Instance.BossDead();
               _onDeath?.Invoke();
               return;
            }
            _timeToAttack = _timeBetweenAttacks;
            StartCoroutine(JumpLoop());
         });
         transform.DOMoveY(_jumpHeight, _jumpDuration * 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
            transform.DOMoveY(targetPos.y, _jumpDuration * 0.5f)).SetEase(Ease.InQuad);

      });
   }

   private void Update()
   {
      _indicator.position = _player.position - Vector3.forward;
      _timeToAttack -= Time.deltaTime;
   }
}
