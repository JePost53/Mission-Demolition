using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JengaBlockSounds : MonoBehaviour
{

    public AudioClip BONKsfx;
    public AudioSource audSource;
    public float magnitudeFactor = 10.0f;
    public float pitchFactor = 1;

    void Start()
    {
        //BONKsfx = audSource.clip;
    }


    private void OnCollisionEnter(Collision collision)
    {
        float audioLevel = collision.relativeVelocity.magnitude / magnitudeFactor;
        float pitchLevel = Random.Range(80, 110) / 100.0f;
        //audSource.pitch = pitchLevel;
        //Debug.Log("Velocity magnitude: " + collision.relativeVelocity.magnitude + " || pitchLevel: " + pitchLevel);
        audSource.PlayOneShot(BONKsfx, audioLevel);
    }
}
