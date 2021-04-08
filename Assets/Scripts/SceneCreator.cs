using System.Collections;
using Cinemachine;
using UnityEngine;

public class SceneCreator : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SetupScene());
    }

    private IEnumerator SetupScene()
    {
        var startPlatform = Instantiate(GameAssets.i.startPlatform, Vector3.zero, Quaternion.identity);
        var startPlatPos = startPlatform.transform.position;
        startPlatform.transform.position += Vector3.back * 40f;
        LeanTween.move(startPlatform, startPlatPos, 0.5f);
        yield return new WaitForSeconds(0.5f);
        for (int i = 1; i <= 4; i++)
        {
            var plat = Instantiate(GameAssets.i.platform, new Vector3(0f, 0f, 10f * i), Quaternion.identity);
            var pos = plat.transform.position;
            plat.transform.position += Vector3.up * 20f;
            LeanTween.move(plat, pos, 0.5f);
            SoundManager.PlaySound(SoundManager.Sound.BubbleHigh, 0.67f, 0.96f);
            yield return new WaitForSeconds(0.23f);
        }
        var exitPlat = Instantiate(GameAssets.i.endPlatform, new Vector3(-0.44f, 0f, 48.28f), Quaternion.identity);
        var exitPos = exitPlat.transform.position;
        exitPlat.transform.position += Vector3.up * 20f;
        LeanTween.move(exitPlat, exitPos, 0.5f);
        SoundManager.PlaySound(SoundManager.Sound.BubbleHigh, 0.67f, 0.7f);
        yield return new WaitForSeconds(0.35f);
        var player = Instantiate(GameAssets.i.playerPrefab, new Vector3(0f, 1.25f, 1.5f), Quaternion.identity);

    }
}
