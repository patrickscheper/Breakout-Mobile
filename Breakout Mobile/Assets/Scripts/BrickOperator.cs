using UnityEngine;
using System.Collections;

public class BrickOperator : MonoBehaviour
{
    //This is the operator for the bricks. Every brick has it's own lives, and a crackedBrick Sprite. (The Game Designer could quickly changes the lives to create a harder brick.)

    public int lives = 1;
    public Sprite crackedBrick;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameOperator.cur_Bricks++;
    }

    void UpdateLives()
    {
        if (lives == 1)
        {
            GameOperator.score += 25;
            spriteRenderer.sprite = crackedBrick;
        }
        if (lives <= 0)
        {
            GameOperator.score += 50;
            GameOperator.cur_Bricks--;
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D()
    {
        lives--;
        UpdateLives();
    }
}
