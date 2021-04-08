using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ShowInfoUI : MonoBehaviour
{
    private TextMeshProUGUI _text;

    public Level level;

    public bool showScore;

    public string captionBefore;
    public string captionAfter;

    public enum Level
    {
        THIS,
        NEXT,
        BEFORE
    }

    public void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Updating());
    }

    private IEnumerator Updating()
    {
        while (true)
        {
            UpdateText(); 
            yield return new WaitForSeconds(0.3f);
        }
        
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }


    public void UpdateText()
    {
        if (showScore)
        {
            _text.text = captionBefore + (LevelManager.playerInfo.score + 1) + captionAfter;
        }
        else
        {
            var levelCount = LevelManager.playerInfo.level;
            switch (level)
            {
                case Level.THIS:
                    _text.text = captionBefore + (levelCount + 1) + captionAfter;
                    break;
                case Level.NEXT:
                    _text.text = captionBefore + (levelCount + 2) + captionAfter;
                    break;
                case Level.BEFORE:
                    _text.text = captionBefore + levelCount + captionAfter;
                    break;
            }
        }
    }
}