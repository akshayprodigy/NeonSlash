using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance;
    
    [SerializeField]
    GameObject beatObjectPrefab;
    //protected GameObject[] cachedObstacles;
    [SerializeField]
    Queue<GameObject> cachedObstacles = new();



    private void Start()
    {
        if (Instance != null)
            Instance = null;
        Instance = this;
    }


    public GameObject GetObstacle()
    {
        GameObject beatObject;
        if (cachedObstacles.Count == 0)
        {
            beatObject = Instantiate(beatObjectPrefab, transform.position, transform.rotation);
        }
        else
        {
            beatObject = cachedObstacles.Dequeue();
            beatObject.transform.position = transform.position;
            beatObject.transform.rotation = transform.rotation;

        }
         
        return beatObject;
    }

    // need to implement object pooling

    public void ObstableDestroyed(GameObject obstacle)
    {
        cachedObstacles.Enqueue(obstacle);
    }

}
