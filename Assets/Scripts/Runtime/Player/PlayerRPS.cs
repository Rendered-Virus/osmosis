using UnityEngine;

public class PlayerRPS : MonoBehaviour
{
   public void Set(int n)
   {
      PlayerPrefs.SetInt("rps", n);
   }
}
