using AndromedaCore.LevelManagement;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private Transform body;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private LayerMask enemyLayer;
    
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
                Destroy(enemy.gameObject);
            }
            WorldBroadcast.OnBulletDestroyed.Publish(overlappedEnemies.Length);
            Destroy(transform.parent.gameObject);
        } else if (coll.CompareTag("Final"))
        {
            WorldBroadcast.OnBulletDestroyed.Publish(0);
            Destroy(transform.parent.gameObject);
        }
        print("collided anyway");
    }
}
