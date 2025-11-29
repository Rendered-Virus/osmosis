using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public int currentState;

    public void SetState(int playerState)
    {
        currentState  = playerState;
    }
}