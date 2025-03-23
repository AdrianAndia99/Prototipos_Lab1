using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemySO enemyData;
    [SerializeField] private float speed = 2f;

    private int patrolIndex = 0;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (enemyData.arraySprites.Length > 0)
        {
            sr.sprite = enemyData.arraySprites[Random.Range(0, enemyData.arraySprites.Length)];
        }
    }

    private void Update()
    {
        if (enemyData != null)
        {
            int previousPatrolIndex = patrolIndex;
            patrolIndex = enemyData.MoveEnemy(transform, speed, patrolIndex);

            if (previousPatrolIndex != patrolIndex)
            {
                ChangeSpriteRandomly();
            }
        }
    }

    private void ChangeSpriteRandomly()
    {
        if (enemyData.arraySprites.Length == 0) 
        {
            return;
        }

        sr.sprite = enemyData.arraySprites[Random.Range(0, enemyData.arraySprites.Length)];
    }
}