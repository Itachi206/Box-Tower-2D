using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;

    public BoxSpawner boxSpawner;
    public GameObject gameOverScreen;

    [HideInInspector] public BoxController currentBox;       //to hid the attribute in inspector panel

    public CameraFollow cameraScript;
    public int moveCount = 0;

    
    public GameObject platformtransform;
    public GameObject gameovertransform;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;            //singleton
        }
    }
   
    void Start()
    {
        boxSpawner.SpawnBox();
    }

    
    void Update()
    {
        DetectInput();
    }

    //detect the mouse input to drop the box
    private void DetectInput() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentBox.DropBox();
        }
    }

    public void SpawnNewBox()
    {
        Invoke("NewBox", 0.5f);       //invoke the newbox function after 2 sec
    }
    void NewBox()
    {
        boxSpawner.SpawnBox();
    }

    public void MoveCamera()
    {
        if(moveCount == 3)
        {
           cameraScript.targetPos.y += 2f;
            moveCount = 0;
        }
       // PlatformBelowGameOver();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);             //get the current active scene name
    }

    internal void GameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    /*public void PlatformBelowGameOver()
    {
        if (platformtransform.transform.position.y < gameovertransform.transform.position.y)
        {
            platformtransform.SetActive(false);
        }
                 
    }*/
    
}

