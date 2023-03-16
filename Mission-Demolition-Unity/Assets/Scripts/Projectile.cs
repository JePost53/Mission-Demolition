using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool awake = true;

    [Header("Inscribed")]
    public float minSpeed;

    [Header("Dynamic")]
    public float speed;

    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 vel = rb.velocity;
        if (vel.magnitude <= minSpeed)
        {
            rb.velocity = new Vector3();
            rb.Sleep();
            awake = false;
            return;
        }
        awake = true;
    }
}
