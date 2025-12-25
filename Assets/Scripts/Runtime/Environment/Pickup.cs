using UnityEngine;

public class Pickup : MonoBehaviour
{
   [SerializeField] private int _condition;

   public void GetPickedUp()
   {
      DialogueConditions.Instance.SetCondition(_condition);
      PlayerInteraction.Instance.DestroyInteractable(GetComponent<Interactable>());
      CrossSceneLoading.Instance.SetHat(transform.GetChild(0));
      Destroy(gameObject);
   }
}
