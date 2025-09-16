using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]private float speed;
    private Rigidbody2D body;
    //InputActions
    private InputAction rotAct;
    private InputAction powAct;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        rotAct = InputSystem.actions.FindAction("Rotate");
        powAct = InputSystem.actions.FindAction("Power");
    }

    // Update is called once per frame
    void Update()
    {
        //rotation
        float rotValue = rotAct.ReadValue<float>();
        body.angularVelocity -= rotValue * speed;

        //height power
        // NOT WORKING RIGHT YET
        float powValue = powAct.ReadValue<float>();
        if (powValue != 0) {
            powValue = powValue * speed * body.linearVelocity.y;
        } else {
            powValue = body.linearVelocity.y;
        }
        body.linearVelocity = new Vector2(body.linearVelocity.x, powValue);

    }
}
