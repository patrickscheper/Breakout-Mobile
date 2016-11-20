using UnityEngine;
using System.Collections;

public class PlatformOperator : MonoBehaviour
{


    [Header("Main Properties")]
    public GameObject mainCamera;

    private Camera mainCamera_cam;
    private Collider2D _collider2D;
    private bool Launch;

    [Header("Ball Properties")]
    public float ballForce;
    public GameObject ballPrefab;
    public GameObject newBall;

    //Position and Lerp properties
    [Header("Movement Properties")]
    public float speed;

    private Vector2 rays;
    private float time;

    void Awake()
    {
        if (mainCamera == null)
        {
            Debug.LogError("A camera needs to be assaigned in the inspector of 'PlatformOperator.cs'.");
        }

        _collider2D = GetComponent<Collider2D>();
        mainCamera_cam = mainCamera.GetComponent<Camera>();

        SpawnBall();

    }

    void Update()
    {
#if UNITY_EDITOR
        EditorUpdate();

#elif UNITY_ANDROID
        AndroidUpdate();
#endif
    }

    void EditorUpdate()
    {
        if (Input.GetMouseButton(0))
            rays = mainCamera_cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rays = new Vector2(0, 0);
            rays += (Vector2)transform.position + new Vector2(-5, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rays = new Vector2(0, 0);
            rays += (Vector2)transform.position + new Vector2(5, 0);
        }


        if (newBall)
        {
            Rigidbody2D ballBody = newBall.GetComponent<Rigidbody2D>();
            ballBody.position = transform.position + new Vector3(0, 3.5f);

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                ballBody.AddForce(new Vector2(0, ballForce));
                ballBody.GetComponent<BallOperator>().launched = true;
                newBall = null;
            }
        }

        float time = (speed/2) * Time.deltaTime;
        float newX = Mathf.Lerp(transform.position.x, rays.x, time);
        newX = Mathf.Clamp(newX, -20, 20);

        transform.position = new Vector2(newX, transform.position.y);
    }

    void AndroidUpdate()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                rays = mainCamera_cam.ScreenToWorldPoint(touch.position);

                if (touch.phase == TouchPhase.Moved)
                {
                    if (newBall)
                    {
                        Rigidbody2D ballBody = newBall.GetComponent<Rigidbody2D>();
                        ballBody.position = transform.position + new Vector3(0, 3);
                    }
                }

                if (touch.phase == TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                    if (newBall)
                    {
                        Rigidbody2D ballBody = newBall.GetComponent<Rigidbody2D>();
                        ballBody.position = transform.position + new Vector3(0, 3);

                        if (Launch)
                        {
                            ballBody.AddForce(new Vector2(0, ballForce));
                            ballBody.GetComponent<BallOperator>().launched = true;
                            newBall = null;
                            Launch = false;
                        }
                    }
            }
        }

        float time = speed * Time.deltaTime;
        float newX = Mathf.Lerp(transform.position.x, rays.x, time);
        newX = Mathf.Clamp(newX, -20, 20);

        transform.position = new Vector2(newX, transform.position.y);
    }

    public void SpawnBall()
    {
        if (ballPrefab == null)
        {
            Debug.LogError("A ball prefab needs to be assaigned in the inspector of 'PlatformOperator.cs'.");
            return;
        }

        Launch = true;

        Vector3 ballPos = transform.position + new Vector3(0, 3);
        newBall = (GameObject)Instantiate(ballPrefab, ballPos, Quaternion.identity);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        foreach (ContactPoint2D contact in col.contacts)
        {

            if (contact.collider != _collider2D)
            {
                float calc = contact.point.x - transform.position.x;
                contact.collider.GetComponent<Rigidbody2D>().AddForce(new Vector3(100f * calc, 0, 0));
            }
        }
    }

}
