using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public float floorHeight = 3f;
    public Transform container;
    public List<Floor> floorList = new List<Floor>();
    public List<Enemy> enemyList = new List<Enemy>();

    public float enemySpawnChance = 0.05f;

    public EnergyPickup energyPickupPrefab;
    public float energyPickUpChance = 0.05f;

    public Dictionary<int, Floor> instantinatedFloors = new Dictionary<int, Floor>();
    public int TopFloor { get; set; } = 0;
    public int BottomFloor { get; set; } = 1;


    public void Generate()
    {
        float topFloorHeight = TopFloor * floorHeight;
        float bottomFloorHeight = BottomFloor * floorHeight;
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
        Debug.Log($"Removed floor on level: {BottomFloor}");
        Destroy(instantinatedFloors[BottomFloor].gameObject);
        instantinatedFloors.Remove(BottomFloor);
        BottomFloor++;
    }
    public void GenerateNextFloor()
    {
        Debug.Log($"Placed floor on level: {TopFloor + 1}");
        Floor floor = GenerateFloor(GetRandomFloor(), TopFloor + 1);

        if (Random.Range(0f, 1f) <= enemySpawnChance)
        {
            SpawnEnemy(GetRandomEnemy(), floor);
        }
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
        Instantiate(enemy, GetRandomPos(floor), Quaternion.identity, floor.transform);
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


        instantinatedFloors.Add(level, newFloor);

        if (Random.Range(0f, 1f) <= energyPickUpChance)
        {
            SpawnEnergyPrefab(newFloor);
        }

        if (level > TopFloor)
            TopFloor = level;

        return newFloor;
    }

    private void SpawnEnergyPrefab(Floor floor)
    {
        Instantiate(energyPickupPrefab, GetRandomPos(floor), Quaternion.identity, floor.transform);
    }


    Vector2 GetRandomPos(Floor floor, float yOffset = 0.5f, float minDistanceToWall = 0.5f)
    {
        float towerWidth = GameManager.Instance.towerWidth;

        float maxRadius = (towerWidth - minDistanceToWall) / 2;
        float minRadius = (-towerWidth + minDistanceToWall) / 2;

        float positionX = Mathf.Lerp(minRadius, maxRadius, Random.Range(0f, 1f));

        Vector2 position = new Vector2(positionX, floor.transform.position.y + yOffset);
        return position;
    }
}
