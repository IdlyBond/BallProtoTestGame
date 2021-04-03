using System;
using System.IO;
using AndromedaCore.LevelManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]private GameObject startScreen;
    [SerializeField]private GameObject winScreen;
    [SerializeField]private GameObject looseScreen;

    public static PlayerInfo playerInfo;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        LoadPlayerInfo();
        WorldBroadcast.WinConditionAchieved.Subscribe(OnWin);
        WorldBroadcast.LooseConditionAchieved.Subscribe(OnLoose);
        WorldBroadcast.OnBulletDestroyed.Subscribe(OnBulletDestroyed);
        SetScreens(start: true);
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        SetScreens();
    }

    public void OnWin(GameObject o)
    {
        playerInfo.level++;
        UpdatePlayerInfo();
        SetScreens(win: true);
    }
    
    public void OnLoose(GameObject o)
    {
        SetScreens(loose: true);
    }

    private void OnBulletDestroyed(int count)
    {
        playerInfo.score += count * 5;
        UpdatePlayerInfo();
        print("Level: " + playerInfo.level);
        print("Score: " + playerInfo.score);
    }

    private void SetScreens(bool start = false, bool win = false, bool loose = false)
    {
        startScreen.SetActive(start);
        winScreen.SetActive(win);
        looseScreen.SetActive(loose);
    }

    private void UpdatePlayerInfo()
    {
        var json = JsonUtility.ToJson(playerInfo);
        File.WriteAllText("data.json", json);
    }

    private void LoadPlayerInfo()
    {
        playerInfo = !File.Exists("data.json") 
            ? new PlayerInfo() 
            : JsonUtility.FromJson<PlayerInfo>(File.ReadAllText("data.json"));
    }

    private void OnApplicationQuit()
    {
        UpdatePlayerInfo();
    }
}

[Serializable]
public class PlayerInfo
{
    public int level;
    public int score;
}

