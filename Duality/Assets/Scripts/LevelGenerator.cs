using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] public float minHeight = 0f;
    [SerializeField] public float floorHeight = 3f;
    
    [SerializeField] private Transform container;
    [SerializeField] private List<Floor> floorList = new List<Floor>();
    [SerializeField] private List<Enemy> enemyList = new List<Enemy>();
    [SerializeField] private List<Trap> trapList = new List<Trap>();

    public float trapSpawnChance = 0.05f;
    public float enemySpawnChance = 0.05f;

    public EnergyPickup energyPickupPrefab;
    public float energyPickUpChance = 0.05f;

    public float cameraSpeed = 0.5f;


    public void Generate()
    {
        float topFloorHeight = GameManager.Instance.TopFloor * floorHeight;
        float bottomFloorHeight = GameManager.Instance.BottomFloor * floorHeight;
        float expectedHeight = GameManager.Instance.ExpectedHeight;
        float renderRangeUp = GameManager.Instance.renderRangeUp;
        float renderRangeDown = GameManager.Instance.renderRangeDown;

        if (GameManager.Instance.ExpectedHeight + renderRangeUp > topFloorHeight)
        {
            GenerateNextFloor();
        }
        if (expectedHeight - renderRangeDown > bottomFloorHeight)
        {
            RemoveBottomFloor();
        }
    }

    public void RemoveBottomFloor()
    {
        Debug.Log($"Removed floor on level: { GameManager.Instance.BottomFloor}");
        Destroy(GameManager.Instance.instantinatedFloors[GameManager.Instance.BottomFloor].gameObject);
        GameManager.Instance.instantinatedFloors.Remove(GameManager.Instance.BottomFloor);
        GameManager.Instance.BottomFloor++;
    }
    public void GenerateNextFloor()
    {
        Debug.Log($"Placed floor on level: { GameManager.Instance.TopFloor + 1}");
        
        if(floorList.Count == 0)
            return;
        
        Floor floor = GenerateFloor(GetRandomFloor(), GameManager.Instance.TopFloor + 1);

        if (Random.Range(0f, 1f) <= enemySpawnChance)
        {
            SpawnEnemy(GetRandomEnemy(), floor);
        }

        if (Random.Range(0f, 1f) <= trapSpawnChance)
        {
            UseRandomTrap(floor);
        }

    }

    private void UseRandomTrap(Floor floor)
    {
        WeightedRandomBag<Trap> randomBag = new WeightedRandomBag<Trap>();
        foreach (var trap in trapList)
        {
            randomBag.AddEntry(trap, trap.randomWeight);
        }

        randomBag.GetRandom(new System.Random()).OnFloorGenerated(floor);
    }

    private Enemy GetRandomEnemy()
    {
        WeightedRandomBag<Enemy> randomBag = new WeightedRandomBag<Enemy>();
        foreach (var enemy in enemyList)
        {
            randomBag.AddEntry(enemy, enemy.randomWeight);
        }

        return randomBag.GetRandom(new System.Random());
    }

    private void SpawnEnemy(Enemy enemy, Floor floor)
    {
        Instantiate(enemy, GetRandomPosX(floor), Quaternion.identity, floor.transform);
    }

    public Floor GetRandomFloor()
    {
        WeightedRandomBag<Floor> randomBag = new WeightedRandomBag<Floor>();
        foreach (var floor in floorList)
        {
            randomBag.AddEntry(floor, floor.weight);
        }

        return randomBag.GetRandom(new System.Random());
    }

    public Floor GenerateFloor(Floor floor, int level)
    {
        Vector2 position = new Vector2(0, level * floorHeight);
        var go = Instantiate(floor.gameObject, position, Quaternion.identity, container);
        var newFloor = go.GetComponent<Floor>();


        GameManager.Instance.instantinatedFloors.Add(level, newFloor);

        if (Random.Range(0f, 1f) <= energyPickUpChance)
        {
            SpawnEnergyPrefab(newFloor);
        }

        if (level > GameManager.Instance.TopFloor)
            GameManager.Instance.TopFloor = level;

        return newFloor;
    }

    public void SpawnEnergyPrefab(Floor floor)
    {
        Instantiate(energyPickupPrefab, GetRandomPosX(floor), Quaternion.identity, floor.transform);
    }

    public static Vector2 GetRandomPosX(Floor floor, float yOffset = 0.5f, float minDistanceToWall = 0.5f)
    {
        float towerWidth = GameManager.Instance.towerWidth;

        float maxRadius = (towerWidth - minDistanceToWall) / 2;
        float minRadius = (-towerWidth + minDistanceToWall) / 2;

        float positionX = Mathf.Lerp(minRadius, maxRadius, Random.Range(0f, 1f));

        Vector2 position = new Vector2(positionX, floor.transform.position.y + yOffset);
        return position;
    }

    public static float GetRandomPosX(float minDistanceToWall = 0.5f)
    {
        float towerWidth = GameManager.Instance.towerWidth;

        float maxRadius = (towerWidth - minDistanceToWall) / 2;
        float minRadius = (-towerWidth + minDistanceToWall) / 2;

        float positionX = Mathf.Lerp(minRadius, maxRadius, Random.Range(0f, 1f));

        return positionX;
    }
}
