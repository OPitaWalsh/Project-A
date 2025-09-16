using UnityEngine;

public class GameManager : MonoBehaviour
{
    //variables
    public static GameManager instance { get; private set; }
    public int hp { get; private set; }
    public int powerUp { get; private set; }
    public bool isPaused { get; private set; }


    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        hp = 3;
        powerUp = 0;
        isPaused = true;
    }


    public void WinLevel() {}

    public void LoseLevel() {}


    public void HPDown() {
        hp -= 1;
    }

    public void SetPowerUp(int index) {
        powerUp = index;
    }

    public void Pause() {
        isPaused = false;
    }

    public void Play() {
        isPaused = true;
    }
}
