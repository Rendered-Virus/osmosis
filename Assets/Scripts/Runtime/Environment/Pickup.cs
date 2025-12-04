using UnityEngine;

public class Pickup : MonoBehaviour
{
   [SerializeField] private int _condition;
   [SerializeField] private PlayerInteraction _playerInteraction;

   public void GetPickedUp()
   {
      DialogueConditions.Instance.SetCondition(_condition);
      _playerInteraction.DestroyInteractable(GetComponent<Interactable>());
      CrossSceneLoading.Instance.SetHat(transform.GetChild(0));
      Destroy(gameObject);
   }
}
