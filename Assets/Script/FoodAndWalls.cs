using UnityEngine;

public class FoodAndWalls : MonoBehaviour
{
    [SerializeField] private GameObject Food;
    [SerializeField] private GameObject Wall;

    public void Start()
    {
        GenerateWalls();
        GenerateFood();
    }

    public void GenerateFood()
    {
        Vector2 randomPos = new Vector2(Random.Range(-8, 8), Random.Range(-4, 4));
        Instantiate(Food, randomPos, Quaternion.identity);
    }

    public void GenerateWalls()
    {
        for (int x = -9; x <= 9; x++)
        {
            Instantiate(Wall, new Vector2(x, -5), Quaternion.identity);
            Instantiate(Wall, new Vector2(x, 5), Quaternion.identity);
        }
        for (int y = -4; y <= 4; y++)
        {
            Instantiate(Wall, new Vector2(-9, y), Quaternion.identity);
            Instantiate(Wall, new Vector2(9, y), Quaternion.identity);
        }
    }
}