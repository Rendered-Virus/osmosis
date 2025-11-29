using DG.Tweening;
using UnityEngine;

public class rewardJumpOut : MonoBehaviour
{
    [SerializeField] private Transform _position;
    [SerializeField] private float _duration;
    [SerializeField] private float _delay;
    
    public void Jump()
    {
        Invoke(nameof(JumpOut),_delay);
    }

    private void JumpOut()
    {
        transform.DOMoveX(_position.position.x, _duration).SetEase(Ease.InQuad);
        transform.DOMoveY(_position.position.y + 2, _duration * 0.5f).
            OnComplete(()=>transform.DOMoveY(_position.position.y, _duration * 0.5f));
    }
}
