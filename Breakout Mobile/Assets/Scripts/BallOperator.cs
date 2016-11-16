using UnityEngine;
using System.Collections;

public class BallOperator : MonoBehaviour
{
    //The ball changes color everytime it spawns.

    public Sprite[] balls = new Sprite[6];

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = balls[Random.Range(0, balls.Length)];
    }
}
