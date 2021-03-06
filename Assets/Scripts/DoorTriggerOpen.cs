using System;
using AndromedaCore.LevelManagement;
using UnityEngine;

public class DoorTriggerOpen : MonoBehaviour
{
    void Start()
    {
        WorldBroadcast.WinConditionAchieved.Subscribe(OnWin);
    }

    private void OnDestroy()
    {
        WorldBroadcast.WinConditionAchieved.Unsubscribe(OnWin);
    }

    private void OnWin(GameObject o)
    {
        SoundManager.PlaySound(SoundManager.Sound.Win, 1f, 1f);
        LeanTween.rotateLocal(gameObject, new Vector3(0f, -109.25f, 0f), 1.2f);
    }
    
}
