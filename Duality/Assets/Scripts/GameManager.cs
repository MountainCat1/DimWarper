using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    // Editor 
    public float towerWidth = 8;
    public float floorHeight = 3f;
    public float renderRangeUp = 10f;
    public float renderRangeDown = 20f;
    public float deathDistance = 30f;
    public float dangerDistance = 25f;
    public Transform container;
    public List<Floor> floorList = new List<Floor>();

    public float cameraRotationSpeed = 1f;
    public float cameraSpeed = 1f;
    public float cameraCatchUpSpeed = 6f;
    public float breakHeight = 2f;
    private float expectedHeight = 0f;

    private Dictionary<int, Floor> instantinatedFloors = new Dictionary<int, Floor>();
    private int topFloor = 0;
    private int bottomFloor = 1;

    public GameObject deathScreen;

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


        if (expectedHeight + renderRangeUp > topFloorHeight)
        {
            GenerateNextFloor();
        }
        if (expectedHeight - renderRangeDown > bottomFloorHeight)
        {
            RemoveBottomFloor();
        }
        if (expectedHeight + deathDistance < bottomFloorHeight)
        {
            Lose();
        }

        MoveCamera();
    }

    private void MoveCamera()
    {
        float playerHeight = PlayerController.Instance.transform.position.y;

        if (playerHeight - breakHeight > expectedHeight)
            expectedHeight += Time.deltaTime * cameraCatchUpSpeed;

        expectedHeight += Time.deltaTime * cameraSpeed;

        float step = Time.deltaTime * cameraCatchUpSpeed;

        Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, new Vector3(0, expectedHeight, -10), step);

        if(playerHeight + deathDistance < expectedHeight)
        {
            Lose();
        }

        float rotateStep = Time.deltaTime * cameraRotationSpeed;
        Quaternion targetRotation;
        if (playerHeight + dangerDistance < expectedHeight)
        {
            Vector3 relativePos = PlayerController.Instance.transform.position - Camera.main.transform.position;

            targetRotation = Quaternion.LookRotation(relativePos, Vector3.up);
            targetRotation = Quaternion.Euler(targetRotation.eulerAngles.x, 0, 0);
        }
        else
        {
            targetRotation = Quaternion.identity;
        }

        float smoothFactor = Quaternion.Angle(Camera.main.transform.rotation, targetRotation);
        Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation, targetRotation, rotateStep * smoothFactor);
    }

    public void Lose()
    {
        Debug.Log("Defeat!!!");
        //deathScreen.SetActive(true);
        SceneManager.LoadScene("GameOver");
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
        WeightedRandomBag<Floor> randomBag = new WeightedRandomBag<Floor>();
        foreach (var floor in floorList)
        {
            randomBag.AddEntry(floor, floor.weight);
        }

        return randomBag.GetRandom(new System.Random());
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
