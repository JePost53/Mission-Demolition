using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S; // a private Singleton

    [Header("Inscribed")]
    public Text uitLevel;
    public Text uitShots;
    public Vector3 castlePos; // The place to put structures
    public GameObject[] castles; //An array of the castles
    public float GoalWaitTime = 2;

    [Header("Dynamic")]
    public int level; // Current level
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot"; // FollowCam mode


    void Start()
    {
        S = this; // Define the Singleton

        level = 0;
        shotsTaken = 0;
        levelMax = castles.Length;
        StartLevel();
    }


    void Update()
    {
        UpdateGUI();

        // Check for level end
        if ( (mode == GameMode.playing) && Goal.goalMet)
        {
            // Change mode to stop checking for level end
            mode = GameMode.levelEnd;

            // Start next level in 2 seconds
            Invoke("NextLevel", GoalWaitTime);
        }
    }


    void StartLevel()
    {
        // Get rid of old castle if it exists
        if (castle != null)
            Destroy(castle);

        // Destroy old projectiles if they exist
        Projectile.DESTROY_PROJECTILES();

        // Create new castle
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;

        // Reset the goal
        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;
    }

    void UpdateGUI()
    {
        // Show the data in the GUITexts
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShots.text = "Shots taken: " + shotsTaken;
    }

    void NextLevel()
    {
        level++;
        if (level >= levelMax)
        {
            level = 0;
            shotsTaken = 0;
        }
        StartLevel();
    }

    static public void SHOT_FIRED()
    {
        S.shotsTaken++;
    }

    static public GameObject GET_CASTLE()
    {
        return S.castle;
    }
}
