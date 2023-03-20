using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowCam : MonoBehaviour
{
    public GameObject pointOfInterest;

    [Header("Dynamic")]
    public float camZ;
    public float minY;
    public float minX;
    public float maxX;

    public float camSnapDistance = 0.1f;
    public Vector3 camStartPos;
    public Vector3 destinationPos;

     // When the projectile goes below this velocity, the camera returns
    public float returnProjectileVelocity; 
    public float waitReturnTime = 3;
    public float waitedTime = 0;

    public float easing = 0.05f;

    void Awake()
    {
        camZ = this.transform.position.z;
        camStartPos = transform.position;
        destinationPos = camStartPos;
    }


    void FixedUpdate()
    {
        POIHandler();
        Move();
    }

    public void POIHandler()
    {
        if (!pointOfInterest)
            return;

        Text poiText = pointOfInterest.GetComponent<Text>();
        if (poiText != null)
        {

        }

        Rigidbody poiRb = pointOfInterest.GetComponent<Rigidbody>();
        //if (poiRb != null && poiRb.IsSleeping())
        if (poiRb != null && (poiRb.velocity.magnitude <= returnProjectileVelocity || pointOfInterest.transform.position.y < minY))
        {
            DelayedCameraReturn();
            return;
        }

        destinationPos = pointOfInterest.transform.position;
    }

        // Used for making the camera return after a certain amount of time since the projectile stopped
    public void DelayedCameraReturn()
    {
        waitedTime += Time.deltaTime;
        if (waitedTime >= waitReturnTime)
        {
            waitedTime = 0;
            pointOfInterest = null;
            destinationPos = camStartPos;
        }
    }

        // Universal movement method for nicely-lerped camera movement, given a universal target position: destinationPos
    public void Move()
    {
        if ((transform.position - destinationPos).magnitude > camSnapDistance)
        {
            Vector3 dest = destinationPos;
            dest = Vector3.Lerp(transform.position, dest, easing);
            dest.z = camZ;
            dest.y = Mathf.Clamp(dest.y, minY, 100);
            dest.x = Mathf.Clamp(dest.x, minX, maxX);
            transform.position = dest;
        }
        else
            transform.position = destinationPos;

        Camera.main.orthographicSize = transform.position.y + 10;
    }
}
