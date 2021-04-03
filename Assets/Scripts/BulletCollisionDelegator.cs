using System;
using UnityEngine;

public class BulletCollisionDelegator : MonoBehaviour
{
    private BulletBehaviour _bb;
    private void Awake()
    {
        _bb = GetComponentInParent<BulletBehaviour>();
    }

    private void OnCollisionEnter(Collision other)
    {
        _bb.OnCollisionEnter(other);
    }
}
