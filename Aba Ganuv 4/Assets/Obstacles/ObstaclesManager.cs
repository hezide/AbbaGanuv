using System;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    [SerializeField] float spawnInterval;            // How long between each spawn.
    [SerializeField] GameObject[] obstacles;
    GameObject playerObject;
    [SerializeField] GameObject target;
    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        playerObject = GameObject.Find("fmask");
        InvokeRepeating("Spawn", 8, spawnInterval);

    }


    void Spawn()
    {
        //choose an obstacle from the list of objects randomally
        int obstacleIndex = UnityEngine.Random.Range(0, obstacles.Length);

        float distanceFromPlayer = getDistanceDependingOnTimePassed();
        Vector3 forwardOffsetVector = getForwardOffsetVector();
        Vector3 obstaclePosition = playerObject.transform.position +(playerObject.transform.forward * distanceFromPlayer) + forwardOffsetVector;
        
        Instantiate(obstacles[obstacleIndex], obstaclePosition, playerObject.transform.rotation);
    }

    //create an x offset for the obstacle so it won't be always in fromt of the player
    private Vector3 getForwardOffsetVector()
    {
        int forwardOffset = UnityEngine.Random.Range(-20, 20);
        Vector3 forwardOffsetVector;
        forwardOffsetVector.x = forwardOffset;
        forwardOffsetVector.y = 0;
        forwardOffsetVector.z = 0;

        return forwardOffsetVector;
    }

    //as long as the player gets closer, the obstacles spawn closer to him
    private float getDistanceDependingOnTimePassed()
    {
        float distanceToTarget = Vector3.Distance(playerObject.transform.position, target.transform.position);
        if(distanceToTarget > 4000)//farest
        {
            return 250;
        }
        else if(distanceToTarget < 4000 && distanceToTarget > 3500)
        {
            return 200;
        }
        else if (distanceToTarget < 3500 && distanceToTarget > 2000)
        {
            return 150;
        }
        else if (distanceToTarget < 2500 && distanceToTarget > 1500)
        {
            return 120;
        }
        return 100;
    }
}