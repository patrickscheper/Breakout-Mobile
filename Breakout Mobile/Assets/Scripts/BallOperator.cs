using UnityEngine;
using System.Collections;

public class BallOperator : MonoBehaviour
{
    //The ball changes color everytime it spawns.

    public Sprite[] balls = new Sprite[6];
    private Rigidbody2D rb;

    public float minSpeed;
    public float maxSpeed;
    public float minVerticalMovement = 0.1f;

    [HideInInspector]
    public bool launched;

    void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer.sprite = balls[Random.Range(0, balls.Length)];
    }

    void LateUpdate()
    {

        if(launched)
        {
            Vector2 currentDirection = rb.velocity;
            float speed = currentDirection.magnitude;
            currentDirection.Normalize();

            //Make sure the ball never goes straight horizotal else it could never come down to the paddle.
            if (currentDirection.x > -minVerticalMovement && currentDirection.x < minVerticalMovement)
            {
                currentDirection.x = currentDirection.x < 0 ? -minVerticalMovement : minVerticalMovement;
                currentDirection.y = currentDirection.y < 0 ? -1 + minVerticalMovement : 1 - minVerticalMovement;
                rb.velocity = currentDirection * speed;
            }

            if (speed < minSpeed || speed > maxSpeed)
            {
                speed = Mathf.Clamp(speed, minSpeed, maxSpeed);

                rb.velocity = currentDirection * speed;
            }

        }

    }

}
