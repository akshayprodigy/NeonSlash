using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{

    public Vector3 direction { get; private set; }

    private Camera mainCamera;
    private float CameraZDistance;

    private Collider sliceCollider;
    private TrailRenderer sliceTrail;

    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;

    private bool slicing;

    private void Awake()
    {
        mainCamera = Camera.main;
        sliceCollider = GetComponent<Collider>();
        sliceTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void Start()
    {
        CameraZDistance = mainCamera.WorldToScreenPoint(transform.position).z; //z axis of the game object for scre
    }

    private void OnEnable()
    {
        StopSlice();
    }

    private void OnDisable()
    {
        StopSlice();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlice();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlice();
        }
        else if (slicing)
        {
            ContinueSlice();
        }
    }



    //Vector3 initmousePosition;

    private void StartSlice()
    {
        Debug.Log("StartSlice: mousePosition: " + Input.mousePosition);
        // Vector3 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 mousePosition = Input.mousePosition;
        //mousePosition.z = mainCamera.nearClipPlane; // Set z position to near clip plane

        Vector3 ScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, CameraZDistance); //z axis added to screen point 
        Vector3 NewWorldPosition = mainCamera.ScreenToWorldPoint(ScreenPosition); //Screen point converted to world point

        transform.position = NewWorldPosition;

        //Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        //Debug.Log("StartSlice: position: " + worldPosition);
        // worldPosition.z = -2.0f;// 0f;
        //transform.position = worldPosition;

        slicing = true;
        sliceCollider.enabled = true;
        sliceTrail.enabled = true;
        sliceTrail.Clear();
    }

    private void StopSlice()
    {
        slicing = false;
        sliceCollider.enabled = false;
        sliceTrail.enabled = false;
    }

    private void ContinueSlice()
    {
        // Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        // Debug.Log("newPosition: " + newPosition);
        //Vector3 mousePosition = Input.mousePosition;
        //mousePosition.z = mainCamera.nearClipPlane; // Set z position to near clip plane

        //Vector3 newPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        //// Debug.Log("StartSlice: position: " + worldPosition);
        // newPosition.z = -2.0f;//0f;
        // newPosition.z = 0f;

        Vector3 ScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, CameraZDistance); //z axis added to screen point 
        Vector3 NewWorldPosition = mainCamera.ScreenToWorldPoint(ScreenPosition); //Screen point converted to world point
        direction = NewWorldPosition - transform.position;
        

        //direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        // Debug.Log("ContinueSlice " + (velocity > minSliceVelocity) + " velocity: "+ velocity);
        sliceCollider.enabled = velocity > minSliceVelocity;

        //transform.position = newPosition;
        transform.position = NewWorldPosition;
    }
}
