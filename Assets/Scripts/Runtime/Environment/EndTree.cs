using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class EndTree : MonoBehaviour
{
    [SerializeField] private float _finalY;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _duration;

    [SerializeField] private TextMeshProUGUI _theEnd;
    [SerializeField] private Transform _playerGFX;
    [SerializeField] private Transform _treePos;
    [SerializeField] private float _scale;
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private Transform _hatTransform;
    [SerializeField][Range(0f,1f)] private float _forwardDirectionInterpolant;

    public void GoUp()
    {
        Camera.main.GetComponent<CinemachineBrain>().DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Styles.Cut,0);
        _camera.gameObject.SetActive(true);
        StartCoroutine(End());
    }

    private IEnumerator End()
    {
        yield return null;
        _playerGFX.SetParent(null);
        _playerGFX.position = _treePos.position;
        _playerGFX.GetComponent<Animator>().enabled = false;
        
        _playerGFX.localScale = Vector3.one * _scale;
        _playerGFX.localEulerAngles = _rotation;
        
        _hatTransform.forward = Vector3.Lerp(-Vector3.forward, _hatTransform.forward, _forwardDirectionInterpolant);
        _camera.DOMoveY(_finalY, _duration);
        yield return new WaitForSeconds(_duration);
        
        
        _theEnd.DOFade(1, 1.5f);
        yield return new WaitForSeconds(3);
        GameManager.Instance.LoadScene(0);
    }
}
