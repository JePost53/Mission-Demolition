using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowCam : MonoBehaviour
{
    static private FollowCam S; //Private singleton

    public GameObject pointOfInterest;

    public enum eView { none, slingshot, castle, both};

    [Header("Inscribed")]
    public GameObject midPoint;

    public float camZ;
    public float minY;
    public float minX;
    public float maxX;

    // When the projectile goes below this velocity, the camera returns
    public float returnProjectileVelocity;
    public float waitReturnTime = 3;
    public float easing = 0.05f;

    [Header("Dynamic")]

    public eView nextView = eView.slingshot;

    public float camSnapDistance = 0.1f;
    public Vector3 camStartPos;
    public Vector3 destinationPos;

    public float waitedTime = 0;


    void Awake()
    {
        S = this;
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
        {
            destinationPos = camStartPos;
            return;
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
        Vector3 dest = destinationPos;
        if ((transform.position - destinationPos).magnitude > camSnapDistance)
        {
            dest = Vector3.Lerp(transform.position, dest, easing);
            dest.y = Mathf.Clamp(dest.y, minY, 100);
            dest.x = Mathf.Clamp(dest.x, minX, maxX);
            transform.position = dest;
        }
        dest.z = camZ;
        transform.position = dest;

        Camera.main.orthographicSize = transform.position.y + 10;
    }


    public void SwitchView(eView newView)
    {
        if (newView == eView.none)
            newView = nextView;
        switch(newView)
        {
            case eView.slingshot:
                pointOfInterest = null;
                nextView = eView.castle;
                break;
            case eView.castle:
                pointOfInterest = MissionDemolition.GET_CASTLE();
                nextView = eView.both;
                break;
            case eView.both:
                pointOfInterest = midPoint;
                nextView = eView.slingshot;
                break;
        }
    }

    public void SwitchView()
    {
        SwitchView(eView.none);
    }

    static public void SWITCH_VIEW(eView newView)
    {
        S.SwitchView(newView);
    }
}
