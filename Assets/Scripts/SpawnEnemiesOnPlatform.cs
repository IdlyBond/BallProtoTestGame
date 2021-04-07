using UnityEngine;

public class SpawnEnemiesOnPlatform : MonoBehaviour
{
    public Transform enemiesObj;
    public GameObject[] designs;
    public bool spawnOnStart;
    public bool spawnDesign;
    void Start()
    {
        
        if (spawnDesign)
        {
            SpawnDesign();
        }
        
        if (spawnOnStart)
        {
            SpawnEnemies();
        }
        
    }

    private void SpawnEnemies()
    {
        var enemyPrefab = GameAssets.i.enemyPrefab;
        var rand = Random.Range(-0.5f, 0.5f);
        for (int i = 0; i < 3; i++)
        {
            for (int j = -3; j < 4; j++)
            {
                if (Random.value < 0.5 + rand/2) continue;
                var enemy = 
                    Instantiate(enemyPrefab, 
                        transform.position + Vector3.forward * i + Vector3.left * j + Vector3.up/1.5f, 
                        Quaternion.identity, enemiesObj);
            
            }
        }
    }
    
    private void SpawnDesign()
    {
        var index = Random.Range(0, designs.Length);
        for (int i = 0; i < designs.Length; i++)
        {
            if (i != index)
            {
                Destroy(designs[i]);
            } else designs[i].SetActive(true);
        }
    }
}
