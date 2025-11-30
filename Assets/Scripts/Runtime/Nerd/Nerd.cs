using System;
using UnityEngine;
using UnityEngine.Events;

public class Nerd : MonoBehaviour
{
   [SerializeField] private float _speed;
   [SerializeField] private GameObject _laptop;
   private Animator _animator;
   [SerializeField] private PlayerMovement _playerMovement;
   [SerializeField] private UnityEvent _onDeath;

   private void Start()
   {
      _animator = GetComponent<Animator>();
   }

   public void RunAway()
   {
      _playerMovement.enabled = false;
      _laptop.SetActive(false);
      _animator.CrossFade("run",0);
      GetComponent<Rigidbody2D>().linearVelocity = Vector3.right * _speed;
      Invoke("Die",1f);
   }

   private void Die()
   {
      _playerMovement.enabled = true;
      _onDeath?.Invoke();
   }
}
