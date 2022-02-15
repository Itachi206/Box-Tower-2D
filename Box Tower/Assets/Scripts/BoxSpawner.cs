using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject boxPrefab;

    private void Start()
    {
       
    }
    public void SpawnBox()
    {
        //initialize the box prefab 
        GameObject box = Instantiate(boxPrefab);
        //spawn the box at the box spawner position 
        box.transform.position = transform.position;
    }
}
