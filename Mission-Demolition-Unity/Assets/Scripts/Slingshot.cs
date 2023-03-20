using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{

    [Header("Inscribed")]
    public GameObject projectilePrefab;
    public GameObject linePrefab;

    [Header("Dynamic")]
    public GameObject launchPoint;
    public GameObject projectile;
    public bool aimingMode = false;

    public float velocityMultiplier;

    public bool jengaProjectile = false;

    void Awake()
    {
        launchPoint = transform.Find("LaunchPoint").gameObject;
        launchPoint.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        aimingUpdate();
    }

    void aimingUpdate()
    {
        if (!aimingMode)
            return;

        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3d = Camera.main.ScreenToWorldPoint(mousePos2D);
        mousePos3d.z = launchPoint.transform.position.z;

        Vector3 mouseDelta = mousePos3d - launchPoint.transform.position;
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (maxMagnitude < mouseDelta.magnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        Vector3 newProjectilePos = launchPoint.transform.position + mouseDelta;
        projectile.transform.position = newProjectilePos;
        if (jengaProjectile)
            projectile.transform.LookAt(launchPoint.transform.position);

            // Fire the projectile!!!
        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            launchPoint.SetActive(false);

            Rigidbody projRb = projectile.GetComponent<Rigidbody>();
            projRb.isKinematic = false;
            projRb.velocity = -mouseDelta * velocityMultiplier;
            Camera.main.GetComponent<FollowCam>().pointOfInterest = projectile;
            Instantiate<GameObject>(linePrefab, projectile.transform);
            projectile = null;
        }
    }


    void OnMouseEnter()
    {
        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        if (!aimingMode)
            launchPoint.SetActive(false);
    }

    void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate<GameObject>(projectilePrefab);
        projectile.transform.position = launchPoint.transform.position;
        projectile.transform.GetComponent<Rigidbody>().isKinematic = true;
    }
}
