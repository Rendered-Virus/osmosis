using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   [Header("Stats")]
   [SerializeField] private float _speed;
   [SerializeField] private float _midAirSpeed;
   [SerializeField] private Vector2 _jumpForce;
   [SerializeField] private int _jumpCount;
   [SerializeField] private float _extraGravity;
   [SerializeField] private float _terminalVelocity;
   
   
   [SerializeField] private Vector2 _groundCheckBox;
   [SerializeField] private float _groundCheckOffset;
   [SerializeField] private AudioClip _jumpSound;
   
   [Header("References")]
   [SerializeField] private Animator _animator;
   
   private int _currentJumpCount;
   private Rigidbody2D _rigidbody2D;
   private bool _facingRight = true;
   private bool _isGrounded;
   private float _currentSpeed;
   private bool _jumpedOnce;
   private bool _isFacingRight;
   private Vector2 _externalVelocity;

   private void Awake()
   {
      _rigidbody2D = GetComponent<Rigidbody2D>();
      _animator.SetBool("Begin",true);
   }

   private void FixedUpdate()
   {
      IsGrounded();
      Move();
      ExtraGravity();
      Rotate();
   }

   private void Rotate()
   {
     _animator.SetBool("Run",false);
      
      if(Input.GetAxisRaw("Horizontal") == 0) return;
      
      _animator.SetBool("Run",true);
      transform.localScale = new Vector3(_facingRight ? 1 : -1,transform.localScale.y, transform.localScale.z);
   }


   private void Update()
   {
     JumpCheck();
   }

   private void JumpCheck()
   {
      if (Input.GetKeyDown(KeyCode.Space))
      {
         _jumpedOnce = _isGrounded ? false : _jumpedOnce;
         _currentJumpCount = _isGrounded ? 0 : _currentJumpCount;

         if (_currentJumpCount >= _jumpCount) return;
         
         if (_jumpedOnce || _isGrounded)
         {
            Jump();
            _currentJumpCount++;
            
           _animator.SetTrigger("Jump" + _currentJumpCount);
            
            if(!_jumpedOnce)
               _jumpedOnce = true;
         }
         
      }
   }
   private void ExtraGravity()
   {
     _animator.SetBool("Falling",false);
      if(_isGrounded || _rigidbody2D.linearVelocityY >= 0 || _rigidbody2D.linearVelocityY < -_terminalVelocity) return;
      
      _animator.SetBool("Falling",true);
      _rigidbody2D.AddForceY(-_extraGravity, ForceMode2D.Force);
   }
   private void Jump()
   {
      _rigidbody2D.AddForceY(_jumpForce.y);
      _rigidbody2D.AddForceX(_jumpForce.x * (_facingRight ? 1 : -1));
      AudioManager.Instance.PlaySfxWithPitchShift(_jumpSound,0.1f,0.3f);
   }

   private void IsGrounded()
   {
      var point = transform.position;
      point.y += _groundCheckOffset;
      
      var ground = Physics2D.OverlapBoxAll(point, _groundCheckBox,0,LayerMask.GetMask("Ground"));
      _isGrounded =  ground.Length > 0;
      _animator.SetBool("Grounded",_isGrounded);
   }

   private void Move()
   {
      var x = Input.GetAxisRaw("Horizontal");

      _currentSpeed = _isGrounded ? _speed : _midAirSpeed;
      _facingRight = x >= 0;
      _rigidbody2D.AddForceX(_currentSpeed * x * Time.fixedDeltaTime,ForceMode2D.Impulse);
      _rigidbody2D.position += _externalVelocity;   
      if (x != 0)
         _isFacingRight = _facingRight;
   }

   private void OnDrawGizmosSelected()
   { 
      Gizmos.color = Color.red;
      var point = transform.position;
      point.y += _groundCheckOffset;
      
      Gizmos.DrawWireCube(point, _groundCheckBox);
   }

   public void Stop()
   {
      _rigidbody2D.linearVelocity = Vector2.zero;
      _animator.SetBool("Run",false);
   }

   public void SetMaxJumps(int maxJumps)
   {
      _jumpCount = maxJumps;
   }
   public bool IsFacingRight() => _isFacingRight;

   public void SetVelocity(Vector2 velocity)
   {
      _externalVelocity = velocity;
   }
}