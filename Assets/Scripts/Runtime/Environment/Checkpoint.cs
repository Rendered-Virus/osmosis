using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public void SetCheckpoint()
    {
        PlayerLoad.Instance.SetCheckPoint(transform.position);
    }
}
