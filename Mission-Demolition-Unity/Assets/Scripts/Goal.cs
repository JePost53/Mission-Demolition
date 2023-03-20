using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Renderer) )]
public class Goal : MonoBehaviour
{
    static public bool goalMet = false;

    void OnTriggerEnter(Collider other)
    {
        Projectile proj = other.GetComponent<Projectile>();
        if (proj != null)
        {
            Goal.goalMet = true;
            Material mat = GetComponent<Renderer>().material;
             // Set the alpha of the color to higher opacity
            Color c = mat.color;
            c.a = 0.75f;
            mat.color = c;
        }
    }
}
