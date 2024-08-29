using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    private List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;
    public Vector2 direction = Vector2.right;
    private Vector2 input;
    public int initialSize = 4;
    public float moveInterval = 0.1f; // Time between moves

    private float nextMoveTime = 0f;

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        // Simplified input handling
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            input = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            input = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            input = Vector2.right;
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            input = Vector2.left;

        // Update direction immediately
        if (input != Vector2.zero)
            direction = input;
    }

    private void FixedUpdate()
    {
        // Check if it's time to move
        if (Time.time >= nextMoveTime)
        {
            nextMoveTime = Time.time + moveInterval;

            // Simplified segment movement
            for (int i = segments.Count - 1; i > 0; i--)
            {
                segments[i].position = segments[i - 1].position;
            }

            // Update snake position
            float x = Mathf.Round(transform.position.x) + direction.x;
            float y = Mathf.Round(transform.position.y) + direction.y;
            transform.position = new Vector2(x, y);
        }
    }

    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    public void ResetState()
    {
        direction = Vector2.right;
        transform.position = Vector3.zero;

        // Simplified reset logic
        for (int i = segments.Count - 1; i >= 0; i--)
        {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(transform);

        for (int i = 0; i < initialSize - 1; i++)
        {
            Grow();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            ScoreManagment.instance.AddPoint();
            Grow();
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }
}