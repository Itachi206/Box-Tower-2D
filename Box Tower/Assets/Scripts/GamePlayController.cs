using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;

    public BoxSpawner boxSpawner;

    [HideInInspector] public BoxController currentBox;       //to hid the attribute in inspector panel

    public CameraFollow cameraScript;
    public int moveCount;


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
        Invoke("NewBox", 2f);       //invoke the newbox function after 2 sec
    }
    void NewBox()
    {
        boxSpawner.SpawnBox();
    }

    public void MoveCamera()
    {
        if(moveCount == 3)
        {
            moveCount = 0;
            cameraScript.targetPos.y += 2f;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);             //get the current active scene name
    }

}

