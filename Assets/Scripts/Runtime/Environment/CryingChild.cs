using UnityEngine;

public class CryingChild : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private GameObject _tears;
    [SerializeField] private GameObject _cat;

    public void StopCrying()
    {
        _anim.CrossFade("nocry",0);
        _tears.SetActive(false);
        _cat.SetActive(true);
    }
}
