using System;
using DG.Tweening;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private float _moveTime;
    private Vector2 _velocity;
    private Vector2 _pastPosition;

    private void Start()
    {
        DOTween.defaultUpdateType  = UpdateType.Fixed;
        transform.DOMove(_endPosition, _moveTime).SetLoops(-1, LoopType.Yoyo);
    }

    private void FixedUpdate()
    {
        _velocity = (Vector2)transform.position - _pastPosition;
        _pastPosition = transform.position;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.TryGetComponent<PlayerMovement>(out var playerMovement))
        {
            playerMovement.SetVelocity(_velocity);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.TryGetComponent<PlayerMovement>(out var playerMovement))
        {
            playerMovement.SetVelocity(Vector2.zero);
        }
    }
}
