using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreWallsCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform CollisionTransform;

    void Start()
    {
        Transform CollisionTransform = this.transform;
        Physics.IgnoreCollision(CollisionTransform.GetComponent<Collider>(), GetComponent<BoxCollider>());
    }

    // Update is called once per frame
    void Update()
    {
    }
}