using UnityEngine;

public class SpawnEnemiesOnPlatform : MonoBehaviour
{
    public Transform enemiesObj;
    public bool spawnOnStart;
    void Start()
    {
        if (!spawnOnStart) return;
        var enemyPrefab = GameAssets.i.enemyPrefab;
        var rand = Random.Range(-0.5f,0.5f);
        for (int i = 0; i < 3; i++)
        {
            for (int j = -4; j < 5; j++)
            {
                if (Random.value < 0.5 + rand/2) continue;
                var enemy = 
                    Instantiate(enemyPrefab, 
                        transform.position + Vector3.forward * i + Vector3.left * j + Vector3.up/1.5f, 
                        Quaternion.identity, enemiesObj);
            
            }
        }

    }

}
