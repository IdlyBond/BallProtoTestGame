using UnityEngine;
using System.Collections;

public class BouncingBall : MetaBall {
    public float speed;

    private Container container;
    private Vector3 direction;

    public void Start() {
        //base.Start();
        this.direction = Random.onUnitSphere;
        this.container = this.GetComponentInParent<Container>();
    }

    
}
