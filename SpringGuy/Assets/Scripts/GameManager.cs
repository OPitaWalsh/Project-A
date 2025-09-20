using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //variables
    public static GameManager instance { get; private set; }
    public int maxHP { get; private set; }
    public int currHP { get; private set; }
    public int coins { get; private set; }
    public int powerUp { get; private set; }
    public bool isPaused { get; private set; }

    //UI components
    [SerializeField]private Image healthbar;
    [SerializeField]private TMP_Text coinCount;


    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        maxHP = currHP = 3;
        coins = 0;
        powerUp = 0;
        isPaused = true;
    }


    public void WinLevel() {}

    public void LoseLevel() {}


    public void HPDown() {
        currHP -= 1;
        healthbar.fillAmount = currHP / (float)maxHP;
    }

    public void CoinUp(int amount) {
        coins += amount;
        coinCount.text = coins.ToString();
    }

    public void CoinDown(int amount) {
        coins -= amount;
        coinCount.text = coins.ToString();
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
