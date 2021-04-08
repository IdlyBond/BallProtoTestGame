using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Transform body;
    private bool _destroyed;
    private Transform bulletKilled;
    
    private void Start()
    {
        StartCoroutine(LifeBehaviour());
    }

    private IEnumerator LifeBehaviour()
    {
        var rotation = (Vector3.left * Random.Range(0.5f, 1f) + Vector3.up * Random.Range(0.5f, 1f)) * 0.4f;
        var startPos = body.localPosition;
        var randomPosStart = Random.Range(0f, 200f);
        while (!_destroyed)
        {
            body.localPosition = startPos + Vector3.up * Mathf.PingPong(Time.time / 5f + randomPosStart, 0.6f);
            body.Rotate(rotation);
            yield return null;
        }

        var distance = Vector3.Distance(bulletKilled.position, body.position);
        var nextPos = body.position + Vector3.up / 3f + Vector3.up * (distance * 0.8f);
        var rotate = Vector3.left * 5f;
        while ((nextPos - body.position).y > 0.4f)
        {
            var multi3 = nextPos - body.position;
            body.Rotate(rotate * (multi3.y * Time.deltaTime * 107));
            body.position += (multi3) / 30f * (Time.deltaTime * 107);
            yield return null;
        }
        yield return new WaitForSeconds(Mathf.Clamp(Random.value * distance / 20f - 0.5f, 0f, 10f));
        Destroy(transform.parent.gameObject);
        Instantiate(GameAssets.i.enemyDeathParticlesPrefab, body.position, Quaternion.identity);
    }

    public void TriggerDestroyed(Transform bulletKilled)
    {
        this.bulletKilled = bulletKilled;
        _destroyed = true;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
