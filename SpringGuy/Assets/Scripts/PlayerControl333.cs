using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl333 : MonoBehaviour
{
    [SerializeField]private float rotSpeed;
    [SerializeField]private float moveSpeed;
    //InputActions
    private InputAction rotAct;
    private InputAction powAct;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotAct = InputSystem.actions.FindAction("Rotate");
        powAct = InputSystem.actions.FindAction("Power");
    }

    // Update is called once per frame
    void Update()
    {
        
        //rotation
        float rotValue = rotAct.ReadValue<float>();
        transform.Rotate(0,0,rotValue * rotSpeed);

        //height power
        //float powValue = powAct.ReadValue<float>();
        //if (true)
        //    body.AddRelativeForceY(powValue);
        
        //clamps
        //transform.rotation = Mathf.Clamp(transform.rotation,-50,50);
    }

}
