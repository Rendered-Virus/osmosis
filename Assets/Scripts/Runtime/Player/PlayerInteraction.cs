using System;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Vector2 _interactionArea;
    [SerializeField] private TextMeshProUGUI _interactionText;
    private Interactable _currentTarget;
    private void FixedUpdate()
    {
        var target =Physics2D.OverlapBox(transform.position, _interactionArea,0,
            LayerMask.GetMask("Interactable"));
        
        if (_currentTarget == null && target)
        {
            _currentTarget = target.GetComponent<Interactable>();
            _interactionText.gameObject.SetActive(true);
            _interactionText.text = "Press E to " + _currentTarget.interactText;
        }
        else if (_currentTarget && !target)
        {
            _currentTarget = null;
            _interactionText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && _currentTarget)
            _currentTarget.Interact();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(transform.position, _interactionArea);
    }
}
