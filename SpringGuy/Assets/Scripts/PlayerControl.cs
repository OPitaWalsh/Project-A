using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]private float rotSpeed;
    [SerializeField]private float linMaxSpeed;
    [SerializeField]private float vBaseSpeed;
    private Rigidbody2D body;
    public GameObject childShield;
    //InputActions
    private InputAction rotAct;
    private InputAction powAct;
    private InputAction recoverAct;
    private InputAction pausePlayAct;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        rotAct = InputSystem.actions.FindAction("Rotate");
        powAct = InputSystem.actions.FindAction("Power");
        recoverAct = InputSystem.actions.FindAction("Recover");
        pausePlayAct = InputSystem.actions.FindAction("Pause");
        childShield = transform.Find("Shield").gameObject;
        childShield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //rotation
        float rotValue = rotAct.ReadValue<float>();
        body.angularVelocity -= rotValue * rotSpeed;

        //bounce recovery and deminish
        //bool recoverPressed = recoverAct.ReadValue<float>() > 0.5 ? true : false;
        if (recoverAct.ReadValue<float>() > 0.5) {
            body.rotation = 0;
            body.angularVelocity = 0;
            body.AddForceY(4);
        }
        /*
        float powValue = powAct.ReadValue<float>();
        if ((powValue >= 0.5) && (body.linearVelocityY < vBaseSpeed) && (body.linearVelocityY > 0)) //good enough
            body.linearVelocityY = vBaseSpeed+1;
        else if ((powValue <= -0.5) && (body.linearVelocityY > vBaseSpeed))
            body.linearVelocityY = vBaseSpeed;
        */
        
        //clamps
        body.linearVelocityX = Mathf.Clamp(body.linearVelocityX,-linMaxSpeed,linMaxSpeed);
        body.linearVelocityY = Mathf.Clamp(body.linearVelocityY,-linMaxSpeed,linMaxSpeed);

        //pause button
        //bool pausePressed = pausePlayAct.ReadValue<float>() > 0.5 ? true : false;
        if (pausePlayAct.ReadValue<float>() > 0.5) {
            GameManager.instance.PausePlay();
        }
    }


    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "Enemy") {
            if (GameManager.instance.powerUps[0]) {
                GameManager.instance.SetPowerUp(0, false);
                childShield.SetActive(false);
            } else {
                GameManager.instance.HPDown();
            }
        }
        else if (coll.gameObject.tag == "Coin") {
            Destroy(coll.gameObject);
            GameManager.instance.CoinUp(1);
        }
        else if (coll.gameObject.tag == "Finish") {
            GameManager.instance.WinLevel();
        }
    }
}
