using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSlicer : MonoBehaviour
{
    private bool isSlicing = false;
    private Vector2 startTouchPos;
    private Vector2 endTouchPos;

    public GameObject cube; // Reference to the cube GameObject
    public GameObject slicingEffectPrefab;

    void Update()
    {
        // Handle touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isSlicing = true;
                startTouchPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                endTouchPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isSlicing = false;
                SliceCube();
            }
        }
    }

    void SliceCube()
    {
        if (isSlicing)
        {
            // Calculate the slicing direction
            Vector2 slicingDirection = (endTouchPos - startTouchPos).normalized;

            // Perform the slicing action
            PerformSlice(slicingDirection);
        }
    }

    void PerformSlice(Vector2 slicingDirection)
    {
        // Calculate the plane for slicing based on the direction
        Plane slicePlane = new Plane(transform.forward, transform.position);

        // Create a new mesh to store the sliced part
        Mesh slicedMesh = new Mesh();

        // Perform the actual slicing using the slicing algorithm
        // This part would involve more complex mesh manipulation
        // We're just assuming here for the sake of example
        // slicedMesh = YourSlicingAlgorithm(cube.GetComponent<MeshFilter>().mesh, slicePlane, slicingDirection);

        // Create a new GameObject for the sliced part
        GameObject slicedPart = new GameObject("SlicedPart");
        slicedPart.AddComponent<MeshFilter>().mesh = slicedMesh;
        slicedPart.AddComponent<MeshRenderer>().material = cube.GetComponent<MeshRenderer>().material;

        // Adjust positions and rotations of the sliced part and the original cube
        // You'll need to calculate these based on your slicing algorithm
        // slicedPart.transform.position = ...
        // slicedPart.transform.rotation = ...
        // cube.transform.position = ...
        // cube.transform.rotation = ...
    }

}