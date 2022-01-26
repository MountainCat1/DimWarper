using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    // Editor 
    public float towerWidth = 8;
    public float floorHeight = 3f;
    public float renderRangeUp = 10f;
    public float renderRangeDown = 25f;
    public float deathDistance = 30f;
    public Transform container;
    public List<Floor> floorList = new List<Floor>();

    private Dictionary<int, Floor> instantinatedFloors = new Dictionary<int, Floor>();
    private int topFloor = 0;
    private int bottomFloor = 1;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singeleton duplicated!");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        Time.timeScale = 2;
    }

    private void Start()
    {
        GenerateNextFloor();

    }

    private void Update()
    {
        float topFloorHeight = topFloor * floorHeight;
        float bottomFloorHeight = bottomFloor * floorHeight;


        if (PlayerController.Instance.transform.position.y + renderRangeUp   > topFloorHeight)
        {
            GenerateNextFloor();
        }
        if (PlayerController.Instance.transform.position.y - renderRangeDown  > bottomFloorHeight)
        {
            RemoveBottomFloor();
        }
        if (PlayerController.Instance.transform.position.y + deathDistance < bottomFloorHeight)
        {
            Lose();
        }
        
    }

    public void Lose()
    {
        Debug.Log("Defeat!!!");
        Application.Quit();
    }

    public void RemoveBottomFloor()
    {
        Debug.Log($"Removed floor on level: {bottomFloor}");
        Destroy(instantinatedFloors[bottomFloor].gameObject);
        instantinatedFloors.Remove(bottomFloor);
        bottomFloor++;
    }
    public void GenerateNextFloor()
    {
        Debug.Log($"Placed floor on level: {topFloor + 1}");
        GenerateFloor(GetRandomFloor(), topFloor + 1 );
    }

    public Floor GetRandomFloor()
    {
        return floorList[Random.Range(0, floorList.Count - 1)];
    }

    public void GenerateFloor(Floor floor, int level)
    {
        Vector2 position = new Vector2(0, level * floorHeight);
        var go = Instantiate(floor.gameObject, position, Quaternion.identity, container);
        
        instantinatedFloors.Add(level, go.GetComponent<Floor>());

        if (level > topFloor)
            topFloor = level;
    }
}
