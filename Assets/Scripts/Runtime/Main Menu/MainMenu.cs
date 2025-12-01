using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton, _creditsButton,_backButton;
    [SerializeField] private Button _thridPartyButton, aiButton;
    [SerializeField] private float _creditsDuration;
    [SerializeField] private CanvasGroup _creditsCanvasGroup;
    [SerializeField] private CanvasGroup _thirdPartyCanvasGroup, _aiCanvasGroup;
    
    
    private void Start()
    {
        _startButton.onClick.AddListener(() =>
        {
            PlayerPrefs.DeleteAll();
            GameManager.Instance.EnterGlobalLevel();
        });
        _thridPartyButton.onClick.AddListener(() =>
        {   
            _thirdPartyCanvasGroup.DOFade(1, _creditsDuration);
            _aiCanvasGroup.alpha = 0;
        });
        aiButton.onClick.AddListener(() =>
        {   
            _aiCanvasGroup.DOFade(1, _creditsDuration);
            _thirdPartyCanvasGroup.alpha = 0;
        });
        _creditsButton.onClick.AddListener(Credits);
        _backButton.onClick.AddListener(EndCredits);
    }

    private void Credits()
    {
        _creditsCanvasGroup.interactable = true;
        _creditsCanvasGroup.blocksRaycasts = true;
        _creditsCanvasGroup.DOFade(1, _creditsDuration);
    }

    private void EndCredits()
    {
        _creditsCanvasGroup.interactable = false;
        _creditsCanvasGroup.blocksRaycasts = false;
        _creditsCanvasGroup.DOFade(0, _creditsDuration);
    }
}
