using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem;
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

    //UI components
    [SerializeField]private Image healthbar;
    [SerializeField]private TMP_Text coinCount;
    [SerializeField]private GameObject winScreen;
    [SerializeField]private GameObject loseScreen;
    [SerializeField]private GameObject pauseScreen;
    [SerializeField]private GameObject buttonSet;


    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        coins = 0; // coins do not get reset with other variables
    }

    private void Start()
    {
        maxHP = currHP = 5;
        powerUp = 0;
        SyncHUD();

        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        pauseScreen.SetActive(false);
        buttonSet.SetActive(false);
        Time.timeScale = 1;
    }


    //variable display
    public void SyncHUD() {
        healthbar.fillAmount = currHP / (float)maxHP;
        coinCount.text = coins.ToString();
    }


    //variable control
    public void HPDown() {
        currHP -= 1;
        SyncHUD();
        if (currHP < 1)
            LoseLevel();
    }

    public void CoinUp(int amount) {
        coins += amount;
        SyncHUD();
    }

    public void CoinDown(int amount) {
        coins -= amount;
        SyncHUD();
    }

    public void SetPowerUp(int index) {
        powerUp = index;
    }


    //pause/play
    public void PausePlay() {
        bool status;
        if (pauseScreen.activeInHierarchy) {
            Time.timeScale = 1;
            status = false;
        }
        else {
            Time.timeScale = 0;
            status = true;
        }
        pauseScreen.SetActive(status);
        buttonSet.SetActive(status);
    }


    //win/lose
    public void WinLevel() {
        PausePlay();
        winScreen.SetActive(true);
        buttonSet.SetActive(true);
    }

    public void LoseLevel() {
        PausePlay();
        loseScreen.SetActive(true);
        buttonSet.SetActive(true);
    }


    //buttons
    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Start();
    }

    public void QuitToMainMenu() {
        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject);
    }
}
