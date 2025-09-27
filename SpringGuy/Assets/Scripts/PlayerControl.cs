using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]private float rotSpeed;
    [SerializeField]private float linMaxSpeed;
    [SerializeField]private float heightPow;
    private bool grounded;
    private Rigidbody2D body;
    private AudioSource sfx;
    public GameObject childShield;
    private Timer iframeTimer;
    //InputActions
    private InputAction rotAct;
    private InputAction powAct;
    private InputAction recoverAct;
    private InputAction pausePlayAct;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grounded = false;
        body = GetComponent<Rigidbody2D>();
        sfx = GetComponent<AudioSource>();
        childShield = transform.Find("Shield").gameObject;
        childShield.SetActive(false);
        iframeTimer = transform.Find("iFrames").GetComponent<Timer>();

        rotAct = InputSystem.actions.FindAction("Rotate");
        powAct = InputSystem.actions.FindAction("Power");
        recoverAct = InputSystem.actions.FindAction("Recover");
        pausePlayAct = InputSystem.actions.FindAction("Pause");
    }

    // Update is called once per frame
    void Update()
    {
        //rotation
        float rotValue = rotAct.ReadValue<float>();
        body.angularVelocity -= rotValue * rotSpeed;

        //height power
        float powValue = powAct.ReadValue<float>();
        if (grounded) {
            if (powValue > 0)
                body.AddForceY(powValue * heightPow);
            else
                body.AddForceY(powValue * heightPow * 2); //negative needs more power
        }

        //recovery
        if ((recoverAct.ReadValue<float>() > 0.5) && (grounded)) {
            body.rotation = 0;
            body.angularVelocity = 0;
            body.AddForceY(heightPow);
        }
        
        //clamps
        body.linearVelocityX = Mathf.Clamp(body.linearVelocityX,-linMaxSpeed,linMaxSpeed);
        body.linearVelocityY = Mathf.Clamp(body.linearVelocityY,-linMaxSpeed,linMaxSpeed);

        //pause button
        //bool pausePressed = pausePlayAct.ReadValue<float>() > 0.5 ? true : false;
        if (pausePlayAct.ReadValue<float>() > 0.5) {
            GameManager.instance.PausePlay();
        }
    }


    // Triggers and Collisions

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "Enemy") {
            if (!iframeTimer.isRunning) {
                iframeTimer.isRunning = true;
                if (GameManager.instance.powerUps[0]) {
                    GameManager.instance.SetPowerUp(0, false);
                    childShield.SetActive(false);
                } else {
                    GameManager.instance.HPDown();
                }
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


    private void OnCollisionEnter2D(Collision2D coll) {
        //if (coll.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        grounded = true;
    }

    private void OnCollisionExit2D(Collision2D coll) {
        sfx.Play();
        
        //if (coll.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        grounded = false;
    }
}
