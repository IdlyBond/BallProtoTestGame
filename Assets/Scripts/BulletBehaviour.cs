using System.Collections;
using AndromedaCore.LevelManagement;
using MudBun;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private Transform body;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private LayerMask enemyLayer;
    
    [SerializeField] private Animator graphicsAnimator;
    
    private Rigidbody _rb;
    private Collider _c;
    
    void Start()
    {
        _rb = GetComponentInChildren<Rigidbody>();

        _rb.velocity = body.forward * bulletSpeed;
    }

    public void SetSize(float s)
    {
        body.localScale = Vector3.one * s;
    }

    public void OnCollisionEnter(Collision other)
    {
        var coll = other.collider;
        if (!coll) return;
        if (coll.CompareTag("Enemy"))
        {
            print("collided");
            var overlappedEnemies = Physics.OverlapSphere(body.position, 2f * body.localScale.x, enemyLayer);
            foreach (var enemy in overlappedEnemies)
            {
                var eb = enemy.GetComponentInParent<EnemyBehaviour>();
                eb.TriggerDestroyed(body);
            }
            WorldBroadcast.OnBulletDestroyed.Publish(overlappedEnemies.Length);
            StartCoroutine(DestroySelf());
        } else if (coll.CompareTag("Final"))
        {
            WorldBroadcast.OnBulletDestroyed.Publish(0);
            StartCoroutine(DestroySelf());
        }
        print("collided anyway");
    }

    private IEnumerator DestroySelf()
    {
        _rb.velocity = Vector3.zero;
        graphicsAnimator.SetTrigger("Die");
        yield return new WaitUntil(() => 
            graphicsAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        body.GetChild(0).gameObject.SetActive(false);
        Destroy(transform.parent.gameObject);
    }
}
