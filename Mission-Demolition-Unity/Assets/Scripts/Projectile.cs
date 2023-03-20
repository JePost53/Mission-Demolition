using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool awake = true;

    static List<Projectile> PROJECTILES = new List<Projectile>();

    [Header("Inscribed")]
    public float minSpeed = 0.1f;

    [Header("Dynamic")]
    public float speed = 0f;


    void Start()
    {
        PROJECTILES.Add(this);
    }

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

    private void OnDestroy()
    {
        PROJECTILES.Remove(this);
    }

    static public void DESTROY_PROJECTILES()
    {
        foreach (Projectile p in PROJECTILES)
            Destroy(p.gameObject);
    }
}
