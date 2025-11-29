using UnityEngine;

public class Pickup : MonoBehaviour
{
   [SerializeField] private int _condition;

   public void GetPickedUp()
   {
      DialogueConditions.Instance.SetCondition(_condition);
      Destroy(gameObject);
      //player hat
   }
}
