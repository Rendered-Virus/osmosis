using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
  public string interactText; 
  [SerializeField] private UnityEvent OnInteract;
  
  public void Interact()
  { 
    OnInteract?.Invoke();
  }
}
