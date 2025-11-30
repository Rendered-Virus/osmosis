using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public UnityEvent OnRespawn, OnGlobalLoad;
    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeDuration;
    
    [SerializeField] private Transform _player;
    [SerializeField] private DialogueSpeaker _dialogueSpeaker1, _dialogueSpeaker2, _dialogueSpeaker3;
    [SerializeField] private PlayerState _playerState;
    public bool load;
    public bool playerDead;

    protected override void Awake()
    {
        base.Awake();
        load = SceneManager.GetActiveScene().buildIndex == 0;
    }

    private void Start()
    {
        _fadeImage.DOFade(0, _fadeDuration);
    }

    public void EnterGlobalLevel()
    {
        _fadeImage.DOFade(1, _fadeDuration).OnComplete(()=>SceneManager.LoadScene(0));
    }

    public void EnterLevel(int level)
    {
        SaveData();
        _fadeImage.DOFade(1, _fadeDuration).OnComplete(()=>SceneManager.LoadScene(level));
    }
    public void DoRespawn()
    {
        playerDead = true;
        _fadeImage.DOFade(1, _fadeDuration).OnComplete(Respawn);
    }

    private void Respawn()
    {
        OnRespawn?.Invoke();
        playerDead = false;
        _fadeImage.DOFade(0, _fadeDuration);
    }
    
    private void SaveData()
    {
        var saveData = new GlobalData();
        
        saveData.globalPosition = _player.position;
        saveData.dialogueIndices = new[] { _dialogueSpeaker1.currentDialogueIndex,
            _dialogueSpeaker2.currentDialogueIndex,
            _dialogueSpeaker3.currentDialogueIndex };
        saveData.playerState = _playerState.currentState;
        SaveDataManager.Instance.SaveData(saveData);
    }

    public void BossDead()
    {
       
    }
}

public class GlobalData
{
    public Vector3 globalPosition;
    public int[] dialogueIndices;
    public int playerState;
}
