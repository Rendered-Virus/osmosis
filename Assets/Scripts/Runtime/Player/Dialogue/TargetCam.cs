using Unity.Cinemachine;
using UnityEngine;

public class TargetCam : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _cam;
    public void Target()
    {
        _cam.gameObject.SetActive(true);
        _cam.Follow = transform;
    }
}
