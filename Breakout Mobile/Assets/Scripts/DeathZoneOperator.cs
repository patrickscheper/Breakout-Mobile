using UnityEngine;
using System.Collections;

public class DeathZoneOperator : MonoBehaviour
{
    //This checks if the ball reaches underneath the screen, destroys it and decreases the players health.

    public GameObject platform;

    void Awake()
    {
        if (platform == null)
            Debug.LogError("A platform needs to be assaigned in the inspector of 'DeathZoneOperator.cs'.");

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        BallOperator ballOperator = col.transform.GetComponent<BallOperator>();
        if (ballOperator)
        {
            platform.GetComponent<PlatformOperator>().SpawnBall();
            GameOperator.lives--;
            Destroy(col.gameObject);
        }
    }
}
