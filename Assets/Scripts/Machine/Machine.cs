using UnityEngine;

public class Machine : MonoBehaviour
{

    [SerializeField]
    private IMovementControl movementControl;
    public float interval = 1.0f;

    void Awake() {
        movementControl = GetComponent<IMovementControl>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerMachine() {
        movementControl.Move();
    }
}
