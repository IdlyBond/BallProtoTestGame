using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (!_i) _i = Instantiate(Resources.Load("GameAssets") as GameObject).GetComponent<GameAssets>();
            return _i;
        }
    }

    [SerializeField] public GameObject prepareBulletPrefab;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public GameObject playerPrefab;
    [SerializeField] public GameObject enemyPrefab;
    
    [SerializeField] public GameObject platform;
    [SerializeField] public GameObject startPlatform;
    [SerializeField] public GameObject endPlatform;
    
    [SerializeField] public GameObject enemyDeathParticlesPrefab;
    
    

}
