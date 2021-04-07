using System.Collections;
using UnityEngine;

public class PrepareBulletBehaviour : MonoBehaviour
{

    [SerializeField] private Transform body;
    
    private float size;
    private bool isGrowing;
    void Awake()
    {
        GetComponentInChildren<Animator>().Play("PrepareAnimation");
        StartCoroutine(Prepare());
    }

    private IEnumerator Prepare()
    {
        isGrowing = true;
        while (isGrowing)
        {
            body.localScale = Vector3.one * size;
            yield return null;
        }
    }

    public void Ready()
    {
        print("BulletIsReady");
        isGrowing = false;
        var parent = transform.parent;
        var bullet = Instantiate(GameAssets.i.bulletPrefab, parent.position, Quaternion.identity)
            .GetComponentInChildren<BulletBehaviour>();
        bullet.SetSize(size);
        Destroy(transform.parent.gameObject);
    }

    public void SetSize(float s) { size = s; }



}
