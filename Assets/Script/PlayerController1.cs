using UnityEngine;
using System;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerController1 : MonoBehaviour
{
    private float velocity = 5f;
    private Vector2 direction;
    private Rigidbody2D rb;

    public static event Action OnFood;
    public static event Action OnEnemy;
    [SerializeField] private FoodAndWalls foodAndWalls;

    [SerializeField] private GameObject bodyPartPrefab;
    [SerializeField] private float invulnerabilityTime = 1.5f;

    private List<Transform> bodyParts = new List<Transform>();
    private List<Vector3> previousPositions = new List<Vector3>();
    private bool isInvulnerable = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        StartCoroutine(InvulnerabilityDelay());
    }

    private void FixedUpdate()
    {
        previousPositions.Insert(0, transform.position);
        rb.velocity = direction * velocity;

        for (int i = 0; i < bodyParts.Count; i++)
        {
            bodyParts[i].position = previousPositions[Mathf.Min(i + 1, previousPositions.Count - 1)];
        }

        if (previousPositions.Count > bodyParts.Count + 1)
        {
            previousPositions.RemoveAt(previousPositions.Count - 1);
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            Vector2 newDir = new Vector2(Mathf.Sign(direction.x), 0);

            if (direction != -newDir)
            {
                direction = newDir;
            }
        }
        else if (Mathf.Abs(direction.y) > 0)
        {
            Vector2 newDir = new Vector2(0, Mathf.Sign(direction.y));

            if (direction != -newDir)
            {
                direction = newDir;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isInvulnerable && (other.CompareTag("Enemy") || other.CompareTag("Wall") || other.CompareTag("Body")))
        {
            return;
        }
        DiferentCollisions(other);
    }


    public void DiferentCollisions(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            OnFood?.Invoke();
            foodAndWalls.GenerateFood();
            StartCoroutine(InvulnerabilityDelay());
            Grow();
        }
        else if (collision.CompareTag("Enemy") || collision.CompareTag("Wall") || collision.CompareTag("Body"))
        {
            OnEnemy?.Invoke();
        }
    }

    private void Grow()
    {
        GameObject newBody = Instantiate(bodyPartPrefab, transform.position, transform.rotation);
        newBody.tag = "Body";
        bodyParts.Add(newBody.transform);
    }

    private IEnumerator InvulnerabilityDelay()
    {
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = true;
    }
}