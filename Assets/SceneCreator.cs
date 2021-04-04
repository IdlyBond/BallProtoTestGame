using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCreator : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SetupScene());
    }

    private IEnumerator SetupScene()
    {
        yield return new WaitForSeconds(10f);
        Instantiate(GameAssets.i.playerPrefab, transform.position, Quaternion.identity);
    }
}
