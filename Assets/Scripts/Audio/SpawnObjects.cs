using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject[] initiallaunchPosition;

    private void Start()
    {
        //Debug.Log("beatObject: " + initiallaunchPosition.Length);
    }

    public void SpawnObject()
    {
        //Debug.Log("beatObject: " + initiallaunchPosition.Length);
        GameObject beatObject = GameManager.Instance.GetObstacle();


        beatObject.GetComponent<BeatObject>().MoveBeatObject(transform.position, initiallaunchPosition[Random.Range(0, initiallaunchPosition.Length)].transform.position);

    }
}
