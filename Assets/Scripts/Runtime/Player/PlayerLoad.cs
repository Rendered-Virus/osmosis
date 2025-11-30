using System;
using UnityEngine;

public class PlayerLoad : Singleton<PlayerLoad>
{
    private Vector3 _respawnPosition;
    
    public void SetCheckPoint(Vector3 position)
    {
        _respawnPosition = position;    
    }
    private void Start()
    {
        if(GameManager.Instance.load)
            Load();
        GameManager.Instance.OnRespawn.AddListener(Respawn);
    }

    private void Respawn()
    {
        transform.position = _respawnPosition;
    }

    private void Load()
    {
        var data = SaveDataManager.Instance.LoadData();
        if (data  != null)
        {
            transform.position = data.globalPosition;
        }
    }
}
