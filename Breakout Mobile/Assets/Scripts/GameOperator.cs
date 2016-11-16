using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOperator : MonoBehaviour
{
    //Here are all the static variables (lives, scores etc.). These will be transfered to each new scene.

    //Static variables
    public static int lives = 3;
    public static int cur_Bricks = 0;
    public static int score = 0;

    //Inspector variables
    [Header("General Game Information")]
    public int lives_ins;
    public int cur_Bricks_ins;
    public int score_ins;

    [Header("Extra")]
    public Text text;
    private int scene;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Update()
    {
        text.text = "LEVEL" + (scene + 1) + " " + score + " " + "LIVES" + lives;

        lives_ins = lives;
        cur_Bricks_ins = cur_Bricks;
        score_ins = score;

        if (cur_Bricks <= 0)
            SceneManager.LoadScene(++scene);


        if (cur_Bricks <= 0 && scene == 3 || lives <= 0)
        {
            lives = 3;
            score = 0;
            scene = 0;
            cur_Bricks = 0;
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
    }
}
