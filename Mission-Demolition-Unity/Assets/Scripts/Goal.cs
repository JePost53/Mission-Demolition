using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Renderer) )]
public class Goal : MonoBehaviour
{
    static public bool goalMet = false;

    private float lerpProgress = 0;
    private Vector3 startScale;
    public Vector3 goalMetScale;
    public float lerpTime = 10;

    void Start()
    {
        startScale = transform.localScale;
    }

    void OnTriggerEnter(Collider other)
    {
        Projectile proj = other.GetComponent<Projectile>();
        if (proj != null)
        {
            Debug.Log("GOAL!!!!!!!!");
            Goal.goalMet = true;
            //Material mat = GetComponent<Renderer>().material;
             // Set the alpha of the color to higher opacity
            //Color c = mat.color;
            //c.a = 0.75f;
            //mat.color = c;
        }
    }

    void Update()
    {
        if (Goal.goalMet)
            GoalMetLerp();
        else
            Movement();
    }
    

    void Movement()
    {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
    }

     // Lerp the goal's visual properties when it is achieved
    void GoalMetLerp()
    {
        if (lerpProgress > 1)
            return;

         // Lerp the goal's scale to the target scale
        transform.localScale = Vector3.Lerp(startScale, goalMetScale, lerpProgress);

         // Lerp the goal's alpha to 0
        Material mat = GetComponent<Renderer>().material;
        Color c = mat.color;
        c.a = 1 - lerpProgress;
        mat.color = c;

        lerpProgress += Time.deltaTime / lerpTime;
    }
}
