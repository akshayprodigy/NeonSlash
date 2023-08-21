using UnityEngine;

public class BeatObject : MonoBehaviour
{
    public float initialSpeed = 25f;
    public float targetSpeed = 15f;
    public float smoothTime = 0.5f;
    public float targetZ = 12f;
    public Vector3 targetB;

    private Vector3 velocity = Vector3.zero;
    private bool initialMovementComplete = false;
    private bool startMoving = false;
    private bool isMovingToB = false;

    private void Update()
    {
        if (startMoving)
        {
            if (isMovingToB)
            {
                // Move the object from A to B
                float step = initialSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetB, step);

                // Check if the object has reached position B
                if (transform.position == targetB)
                {
                    isMovingToB = false;
                }
            }
            else
            {
                // Move the object along the Z-axis
                float zStep = targetSpeed * Time.deltaTime;
                transform.Translate(Vector3.back * zStep);
            }
            //Debug.Log("transform.position.z : " + transform.position.z);
            if (transform.position.z <= -10)
            {
                GameManager.Instance.ObstableDestroyed(this.gameObject);
                startMoving = false;
            }
        }
        
    }

    public void MoveBeatObject(Vector3 from, Vector3 To)
    {
        transform.position = from;
        startMoving = true;
        isMovingToB = true;
        targetB = To;
    }


}
