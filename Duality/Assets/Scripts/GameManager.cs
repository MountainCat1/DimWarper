﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public bool Lost { get; private set; } = false;
    public bool Paused { get; set; } = false;
    public Action GameLostEvent;
    public bool GameOver => Lost || Won;
    
    public LevelGenerator ActiveLevelGenerator { get => LevelGeneratorManager.Instance.GetActiveLevelGenerator(ExpectedHeight); }
    public Dictionary<int, Floor> instantinatedFloors = new Dictionary<int, Floor>();
    public int TopFloor { get; set; } = 0;
    public int BottomFloor { get; set; } = 1;

    // Editor 
    public float towerWidth = 8;
    public float renderRangeUp = 10f;
    public float renderRangeDown = 20f;
    public float deathDistance = 30f;
    public float dangerDistance = 25f;

    public float timeScale = 2f;

    // Camera, Height, Time
    public Transform cameraTransform;
    public float cameraRotationSpeed = 1f;
    public float cameraSpeedMultiplier = 1f;
    public float cameraCatchUpSpeed = 6f;
    public float breakHeight = 2f;
    public float cameraSpeedChangeSpeed = 0.1f;
    private float cameraSpeed = 0.6f;
    public float ExpectedHeight { get; set; } = 0f;
    public float Timer { get; set; }
    
    // Misc? Dont really know how to call those x3
    public AudioSource soundtrackSource;
    public Animator blackScreeAnimator;
    public Animator soundtrackAnimator;
    public MenuWindow deathScreen;
    public float timeToShowGameOverScreen = 1f;

    
    // Game settings
    public float maxEnergy = 100f;
    public float energyRegen = 0.2f;
    public float actionEnergyCost = 25f;
    public float gameEndHeight = 0f;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singleton duplicated!");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        
        Time.timeScale = timeScale;
        Application.targetFrameRate = 999;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Energy = 100f;
    }

    public void PauseGame()
    {
        Paused = true;
        Time.timeScale = 0f;
        
        // Pause music, it's required to disable an animator bcs otherwise it will just reset pitch to 1
        soundtrackSource.pitch = 0f;
        soundtrackAnimator.enabled = false;
    }

    public void UnpauseGame()
    {
        Paused = false;
        Time.timeScale = timeScale;
        
        // Start music
        soundtrackSource.pitch = 1f;
        soundtrackAnimator.enabled = true;
    }
    
    private void Start()
    {
        LevelGeneratorManager.Instance.GetActiveLevelGenerator(ExpectedHeight).GenerateNextFloor(this);
    }

    private void FixedUpdate()
    {
        if(!GameManager.Instance.Won)
            LevelGeneratorManager.Instance.GetActiveLevelGenerator(ExpectedHeight).Generate(GameManager.Instance);
    }

    private void Update()
    {
        if (!Won && !Lost && !Paused)
        {
            Timer += Time.deltaTime / Time.timeScale;
        }
        
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

        if (!Lost)
        {
            // If character is to high, speed up camera to catch up
            if (playerHeight - breakHeight > ExpectedHeight)
                ExpectedHeight += Time.deltaTime * cameraCatchUpSpeed;

            // Update speed of camera
            if (ActiveLevelGenerator.cameraSpeed > cameraSpeed)
                cameraSpeed += cameraSpeedChangeSpeed * Time.deltaTime;
            else if (ActiveLevelGenerator.cameraSpeed < cameraSpeed)
                cameraSpeed -= cameraSpeedChangeSpeed * Time.deltaTime;
            if (Mathf.Abs(cameraSpeed - ActiveLevelGenerator.cameraSpeed) < cameraSpeedChangeSpeed * Time.deltaTime)
                cameraSpeed = ActiveLevelGenerator.cameraSpeed;

            // Calculate expected height
            ExpectedHeight += Time.deltaTime * cameraSpeed * cameraSpeedMultiplier;

            // Move camera
            float step = Time.deltaTime * cameraCatchUpSpeed;
            cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, new Vector3(0, ExpectedHeight, -10), step);

            // Kill player if needed
            if (playerHeight + deathDistance < ExpectedHeight)
            {
                PlayerController.Instance.Kill();
            }
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

        var rotation = cameraTransform.rotation;
        float smoothFactor = Quaternion.Angle(rotation, targetRotation);
        rotation = Quaternion.RotateTowards(rotation, targetRotation, rotateStep * smoothFactor);
        cameraTransform.rotation = rotation;
    }

    public void Win()
    {
        soundtrackAnimator.SetBool("fade", true);
        Won = true;
    }

    public void Lose()
    {
        if (Won)
            return;

        blackScreeAnimator.SetBool("fade", true);
        soundtrackAnimator.SetBool("slowDown", true);

        StartCoroutine(ShowGameOverScreenCoroutine());

        Lost = true;
        GameLostEvent?.Invoke();
    }

    IEnumerator ShowGameOverScreenCoroutine()
    {
        yield return new WaitForSeconds(timeToShowGameOverScreen);

        deathScreen.Show();
    }
}
