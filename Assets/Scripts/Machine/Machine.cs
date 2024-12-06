using UnityEngine;

public class Machine : MonoBehaviour
{

    IMovementControl movementControl;

    void Awake() {
        movementControl = GetComponent<IMovementControl>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementControl.Move();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerMachine() {

    }
}
