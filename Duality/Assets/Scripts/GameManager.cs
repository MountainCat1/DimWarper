using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    // Game properties
    public int Coins { get; set; }
    public float Energy { get => energy; set => energy = value > maxEnergy ? maxEnergy : value; }
    private float energy;

    public bool Won { get; private set; } = false;

    // Editor 
    public float towerWidth = 8;
    public float renderRangeUp = 10f;
    public float renderRangeDown = 20f;
    public float deathDistance = 30f;
    public float dangerDistance = 25f;


    public Dictionary<int, Floor> instantinatedFloors = new Dictionary<int, Floor>();
    public int TopFloor { get; set; } = 0;
    public int BottomFloor { get; set; } = 1;


    public Transform cameraTransform;
    public float cameraRotationSpeed = 1f;
    public float cameraSpeedMultiplier = 1f;
    public float cameraCatchUpSpeed = 6f;
    public float breakHeight = 2f;
    public float ExpectedHeight { get; set; } = 0f;

    public Animator blackScreeAnimator;
    public Animator soundtrackAnimator;
    public GameObject deathScreen;
    public float timeToShowGameOverScreen = 1f;

    public float maxEnergy = 100f;
    public float energyRegen = 0.2f;
    public float actionEnergyCost = 25f;

    public LevelGenerator ActiveLevelGenerator { get => LevelGeneratorManager.Instance.GetActiveLevelGenerator(ExpectedHeight); }



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

        Energy = 100f;
    }

    private void Start()
    {
        LevelGeneratorManager.Instance.GetActiveLevelGenerator(ExpectedHeight).GenerateNextFloor();
    }

    private void Update()
    {
        LevelGeneratorManager.Instance.GetActiveLevelGenerator(ExpectedHeight).Generate();

        //float topFloorHeight = activeLevelGenerator.TopFloor * activeLevelGenerator.floorHeight;
        float bottomFloorHeight = BottomFloor * LevelGeneratorManager.Instance.GetActiveLevelGenerator(ExpectedHeight).floorHeight;

        if (ExpectedHeight + deathDistance < bottomFloorHeight)
        {
            Lose();
        }

        MoveCamera();

        Energy += energyRegen * Time.deltaTime;
    }

    private void MoveCamera()
    {
        float playerHeight = PlayerController.Instance.transform.position.y;

        if (playerHeight - breakHeight > ExpectedHeight)
            ExpectedHeight += Time.deltaTime * cameraCatchUpSpeed;

        ExpectedHeight += Time.deltaTime * ActiveLevelGenerator.cameraSpeed * cameraSpeedMultiplier;

        float step = Time.deltaTime * cameraCatchUpSpeed;

        cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, new Vector3(0, ExpectedHeight, -10), step);

        if (playerHeight + deathDistance < ExpectedHeight)
        {
            Lose();
        }

        float rotateStep = Time.deltaTime * cameraRotationSpeed;
        Quaternion targetRotation;
        if (playerHeight + dangerDistance < ExpectedHeight)
        {
            Vector3 relativePos = PlayerController.Instance.transform.position - cameraTransform.position;

            targetRotation = Quaternion.LookRotation(relativePos, Vector3.up);
            targetRotation = Quaternion.Euler(targetRotation.eulerAngles.x, 0, 0);
        }
        else
        {
            targetRotation = Quaternion.identity;
        }

        float smoothFactor = Quaternion.Angle(cameraTransform.rotation, targetRotation);
        cameraTransform.rotation = Quaternion.RotateTowards(cameraTransform.rotation, targetRotation, rotateStep * smoothFactor);
    }

    public void Win()
    {
        cameraRotationSpeed = 0f;
        cameraSpeedMultiplier = 0f;
        Won = true;
    }

    public void Lose()
    {
        if (Won)
            return;

        blackScreeAnimator.SetBool("fade", true);
        soundtrackAnimator.SetBool("slowDown", true);

        StartCoroutine(ShowGameOverScreenCoroutine());
        //deathScreen.SetActive(true);
        //SceneManager.LoadScene("GameOver");
    }

    IEnumerator ShowGameOverScreenCoroutine()
    {
        yield return new WaitForSeconds(timeToShowGameOverScreen);

        deathScreen.SetActive(true);
    }
}
