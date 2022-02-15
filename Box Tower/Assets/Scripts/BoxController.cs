using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private float min_x = -2.4f, max_x = 2.4f;

    private bool canMove;
    private float moveSpeed = 2f;

    private Rigidbody2D boxRigidbody;
        
    private bool gameOver;
    private bool ignoreCollision;
    private bool ignoreTrigger;

    private void Awake()
    {
        //get the reference of the rigidbody 
        boxRigidbody = GetComponent<Rigidbody2D>();

    }

    void Start()
    {
        canMove = true;
        if (Random.Range(0, 2) > 0)
        {
            moveSpeed *= -1f; //
        }

        GamePlayController.instance.currentBox = this;      //gets the current box reference

    }


    void Update()
    {
        MoveBox();
    }

    private void MoveBox()
    {
        if (canMove)
        {
            Vector3 tempMovement = transform.position;

            tempMovement.x += moveSpeed * Time.deltaTime;

            if (tempMovement.x > max_x) //if the box collide with the right side then it will change the direction
            {
                moveSpeed *= -1f;
            }
            else if (tempMovement.x < min_x)  //if the box collide with the left side then it will change the direction
            {
                moveSpeed *= -1f;
            }

            transform.position = tempMovement;
        }
    }

    public void DropBox()
    {
        canMove = false;
        boxRigidbody.gravityScale = Random.Range(2, 4);
    }

    private void Landed()
    {
        if (gameOver)
        {
            return;
        }
        ignoreCollision = true;
        ignoreTrigger = true;

        GamePlayController.instance.SpawnNewBox();
        GamePlayController.instance.MoveCamera();
    }

    void RestartGame()
    {
        GamePlayController.instance.RestartGame();
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (ignoreCollision)
        {
            return;
        }
        
        if(target.gameObject.tag == "Platform" || target.gameObject.tag == "Box")
        {
            GamePlayController.instance.moveCount++;
            ScoreController.instance.IncreaseScore(10);
            Invoke("Landed", 1f);
            ignoreCollision = true;
           
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (ignoreTrigger)
        {
            return;
        }

        if (target.gameObject.tag == "GameOver" )
        {
            HealthBar.instance.chance--;
            if(HealthBar.instance.chance <= 0)
            {
                CancelInvoke("Landed");
                gameOver = true;
                ignoreTrigger = true;

                Invoke("RestartGame", 1f);
            }
            else
            {
                Invoke("Landed", 1f);
                ignoreCollision = true;
                HealthBar.instance.UpdateHealth();
            }
            
           
        }
    }
}
