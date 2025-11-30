using UnityEngine;

public class SaveDataManager : Singleton<SaveDataManager>
{
    public void SaveData(GlobalData data)
    {
        PlayerPrefs.SetFloat("playerX", data.globalPosition.x);
        PlayerPrefs.SetFloat("playerY", data.globalPosition.y);
        PlayerPrefs.SetFloat("playerZ", data.globalPosition.z);
        
        PlayerPrefs.SetInt("dialogue1", data.dialogueIndices[0]);
        PlayerPrefs.SetInt("dialogue2", data.dialogueIndices[1]);
        PlayerPrefs.SetInt("dialogue3", data.dialogueIndices[2]);
        
        PlayerPrefs.SetInt("playerState",data.playerState);
        PlayerPrefs.SetInt("hat", data.hat);
    }
    public GlobalData LoadData()
    {
        if (!PlayerPrefs.HasKey("playerX"))
            return null;
        
        return new GlobalData()
        {
            dialogueIndices = new int[]{PlayerPrefs.GetInt("dialogue1"),
                PlayerPrefs.GetInt("dialogue2"),
                PlayerPrefs.GetInt("dialogue3")},
            
            globalPosition = new Vector3(PlayerPrefs.GetFloat("playerX"),
                PlayerPrefs.GetFloat("playerY"),
                PlayerPrefs.GetFloat("playerZ")),
            
            hat =  PlayerPrefs.GetInt("hat"),
            playerState = PlayerPrefs.GetInt("playerState")
        };
    }
}
