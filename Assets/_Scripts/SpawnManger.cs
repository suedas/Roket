using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManger : MonoBehaviour
{
    #region Singleton
    public static SpawnManger instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    #endregion

    public GameObject collectible;
    public GameObject obstacle;
    private GameObject eskiCollectible;
    private GameObject eskiObstacle;
    public void spawn()
    {
        float yColelctible = Random.Range(2f,10f);
        float yObstacle = Random.Range(7f, 15f);
        //int randomindex = Random.Range(0, collectible.Length);
        for (int i = 0; i < 50; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-1.30f, 1.30f), yColelctible, 0);
            eskiCollectible = Instantiate(collectible, spawnPosition, Quaternion.identity);
            yColelctible = eskiCollectible.transform.position.y;
            yColelctible += Random.Range(7f,12f);
        }
        for (int j= 0; j < 20; j++)
        {
            Vector3 obsatclePosition = new Vector3(Random.Range(-1.30f, 1.30f), yObstacle, 0);
            eskiObstacle = Instantiate(obstacle, obsatclePosition, Quaternion.identity);
            yObstacle = eskiObstacle.transform.position.y;
            yObstacle += Random.RandomRange(7f, 15f);

        }
   
    }
}
