using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private float min_x = -2.4f, max_x = 2.4f;

    private bool canMove;
    private float moveSpeed = 3f;

    private Rigidbody2D boxRigidbody;
    
    //public BoxSpawner boxSpawner;
            
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

        GamePlayController.instance.currentBox = this;     //gets the current box reference
    }


    void Update()
    {
        MoveBox();
        AudioManager.Instance.sfxVol.value = 0.7f;
        AudioManager.Instance.musicVol.value = 0.3f;
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

    void GameOverScreen()
    {
        GamePlayController.instance.GameOverScreen();
    }
    private void OnCollisionEnter2D(Collision2D target)
    {
        if (ignoreCollision)
        {
            return;
        }

        if (target.collider.tag == "Platform") 
        {
            Debug.Log("hitted a platform");
            AudioManager.Instance.PlayEffect(SoundEnum.BoxCollision);

            GamePlayController.instance.moveCount++;
            ScoreController.instance.IncreaseScore(10);
            Invoke("Landed", 1f);
            ignoreCollision = true;
        }
        else if(target.gameObject.tag == "Box")
        {
            Debug.Log("hitted a box");
            AudioManager.Instance.PlayEffect(SoundEnum.BoxCollision);

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
            //Destroy(boxSpawner.boxPrefab);
            HealthBar.instance.chance--;
            if(HealthBar.instance.chance <= 0)
            {
                Debug.Log("chances over ");
                gameOver = true;
                ignoreTrigger = true;

                Invoke("GameOverScreen", 1f);
            }
            else
            {
                Debug.Log("Gameover");                
                ignoreTrigger = true;
                HealthBar.instance.UpdateHealth();
                Invoke("Landed", 1f);
            }
            
           
        }
    }
}
