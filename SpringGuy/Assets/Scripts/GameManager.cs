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
    public bool[] powerUps { get; private set; }

    //UI components
    [SerializeField]private Image healthbar;
    [SerializeField]private TMP_Text coinCount;
    [SerializeField]private GameObject winScreen;
    [SerializeField]private GameObject loseScreen;
    [SerializeField]private GameObject pauseScreen;
    [SerializeField]private GameObject buttonSet;
    [SerializeField]private GameObject shopScreen;


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
        powerUps = new bool[] {false,false,false};
        SyncHUD();

        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        pauseScreen.SetActive(false);
        loseScreen.SetActive(false);
        buttonSet.SetActive(false);
        
        shopScreen.SetActive(true);
        Time.timeScale = 0;
    }


    //this is how the level actually starts
    public void CloseShop() {
        LoadPowerUps();
        shopScreen.SetActive(false);
        Time.timeScale = 1;
        SyncHUD();
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

    public void SetPowerUp(int i, bool selected) {
        powerUps[i] = selected;
    }

    private void LoadPowerUps() {  //NOTE: Powerups not implemented!
        PlayerControl player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        if (player != null) {
            if (powerUps[0]) {
                player.childShield.SetActive(true);
            }
            if (powerUps[1]) {
                Debug.Log("You bounce extra high!");
            }
            if (powerUps[2]) {
                Debug.Log("You can jump in the air!");
            }
        }
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
