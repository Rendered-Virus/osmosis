using System;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _range;

    private void Start()
    {
        var player = Physics2D.OverlapCircle(transform.position, _range,LayerMask.GetMask("Player"));
        if(player) 
            GameManager.Instance.DoRespawn();
    }
}
