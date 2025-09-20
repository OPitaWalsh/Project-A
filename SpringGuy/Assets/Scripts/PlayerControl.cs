using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]private float rotSpeed;
    [SerializeField]private float linMaxSpeed;
    [SerializeField]private float vBaseSpeed;
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
        body.angularVelocity -= rotValue * rotSpeed;

        //bounce recovery and deminish
        float powValue = powAct.ReadValue<float>();
        if ((powValue >= 0.5) && (body.linearVelocityY < vBaseSpeed) && (body.linearVelocityY > 0)) //good enough
            body.linearVelocityY = vBaseSpeed+1;
        else if ((powValue <= -0.5) && (body.linearVelocityY > vBaseSpeed))
            body.linearVelocityY = vBaseSpeed;
        
        //clamps
        body.rotation = Mathf.Clamp(body.rotation,-45,45);
        body.linearVelocityX = Mathf.Clamp(body.linearVelocityX,-linMaxSpeed,linMaxSpeed);
        body.linearVelocityY = Mathf.Clamp(body.linearVelocityY,-linMaxSpeed,linMaxSpeed);
    }


    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "Enemy") {
            GameManager.instance.HPDown();
        }
        else if (coll.gameObject.tag == "Coin") {
            Destroy(coll.gameObject);
            GameManager.instance.CoinUp(1);
        }
    }
}
