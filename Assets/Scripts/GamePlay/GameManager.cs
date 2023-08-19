using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance;
    
    [SerializeField]
    GameObject beatObjectPrefab;
    protected GameObject[] cachedObstacles;


    private void Start()
    {
        if (Instance != null)
            Instance = null;
        Instance = this;
    }
    public GameObject GetObstacle()
    {
         GameObject beatObject = Instantiate(beatObjectPrefab, transform.position, transform.rotation);
        return beatObject;
    }

    // need to implement object pooling

}
