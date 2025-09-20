using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl222 : MonoBehaviour
{
    [SerializeField]private float rotSpeed;
    [SerializeField]private float moveSpeed;
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
        /*
        print(body.IsTouchingLayers(6));
        if (body.IsTouchingLayers(LayerMask.NameToLayer("Terrain"))) {
            grounded = true;
            //print("true");
        } else {
            grounded = false;
            //print("false");
        }
        */
        //RaycastHit hit;
        //print(Physics.Raycast(transform.position, new Vector2(0,-1), out hit, 2.0f));

        //rotation
        float rotValue = rotAct.ReadValue<float>();
        body.angularVelocity -= rotValue * rotSpeed;

        //height power
        float powValue = powAct.ReadValue<float>();
        if (true)
            body.AddRelativeForceY(powValue);
        
        //clamps
        body.rotation = Mathf.Clamp(body.rotation,-50,50);
    }


    /*
    //manage grounded
    private void OnCollisionEnter(Collision coll) {
        countColliders++;
        print("enter");
    } 
    private void OnCollisionExit(Collision coll) {
        countColliders--;
        print("exit");
    } 
    private bool IsGrounded() {
        return (countColliders > 0);
    }
    */
}
