using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopScript : MonoBehaviour
{
    [SerializeField]private Button startButton;


    //coin display
    public void SyncHUD() {
        int coins = GameManager.instance.coins;

        //if negative, disable "Start" button
        if (coins < 0) {
            startButton.interactable = false;
        }
        else {
            startButton.interactable = true;
        }
    }


    public void CloseShop() {
        GameManager.instance.CloseShop();
    }


    // Shop Items
    public void Shield(bool selected) {
        if (selected)
            GameManager.instance.CoinDown(5);
        else
            GameManager.instance.CoinUp(5);
        SyncHUD();
        GameManager.instance.SetPowerUp(0,selected);
    }

    public void Bounce(bool selected) {
        if (selected)
            GameManager.instance.CoinDown(5);
        else
            GameManager.instance.CoinUp(5);
        SyncHUD();
        GameManager.instance.SetPowerUp(1,selected);
    }

    public void Jump(bool selected) {
        if (selected)
            GameManager.instance.CoinDown(10);
        else
            GameManager.instance.CoinUp(10);
        SyncHUD();
        GameManager.instance.SetPowerUp(2,selected);
    }
}
