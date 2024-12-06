using UnityEngine;
using System.Collections;

public class PressMovement : MonoBehaviour, IMovementControl
{

    [SerializeField]
    private float interval = 1.0f;
    [SerializeField]
    private float movementTime = 1.0f;
    [SerializeField]
    private Transform destination;
    private Transform initialPosition;

    private bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move() {
        StartCoroutine(SmoothMove());
    }
    
    public void Rewind() {

    }

    private IEnumerator SmoothMove()
    {

        isMoving = true;

        float elapsedTime = 0.0f;

        while (elapsedTime < movementTime)
        {
            transform.position = Vector3.Lerp(initialPosition.position, destination.position, elapsedTime / movementTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = destination.position;
    }
}
