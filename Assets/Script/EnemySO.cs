using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObjects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public Sprite[] arraySprites;
    public Transform[] patrolPoints;

    public int MoveEnemy(Transform enemyTransform, float speed, int patrolIndex)
    {
        if (patrolPoints.Length == 0) 
        {
            return patrolIndex;
        } 

        Transform targetPoint = patrolPoints[patrolIndex];

        enemyTransform.position = Vector2.MoveTowards(enemyTransform.position,targetPoint.position,speed * Time.deltaTime);

        if (Vector2.Distance(enemyTransform.position, targetPoint.position) < 0.1f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }

        return patrolIndex;
    }
}