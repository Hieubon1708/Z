using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance;

    public Sprite[] frameButtonWeaponBuyers;
    public Sprite[] frameGoldWeaponBuyers;

    public Sprite[] frameButtonBlockUpgradees;
    public Sprite[] frameButtonWeaponUpgradees;

    public void Awake()
    {
        instance = this;
        PlayerPrefs.SetInt("Gold", 2222);
    }

    public enum Type
    {
        ENOUGH_MONEY, NOT_ENOUGH_MONEY
    }

    public void ChangeSpriteWeaponBuyer(Type type, Image frameButton, Image frameGold)
    {
        int index = 0;
        if (type == Type.NOT_ENOUGH_MONEY) index = 1;
        frameButton.sprite = frameButtonWeaponBuyers[index];
        frameGold.sprite = frameGoldWeaponBuyers[index];
    }
    
    public void ChangeSpriteWeaponUpgradee(Type type, Image frameButton)
    {
        int index = 0;
        if (type == Type.NOT_ENOUGH_MONEY) index = 1;
        frameButton.sprite = frameButtonWeaponUpgradees[index];
    }

    public void ChangeSpriteWeaponUpgradee(Image frameButton, TextMeshProUGUI textPrice, TextMeshProUGUI textMax)
    {
        textPrice.gameObject.SetActive(false);
        textMax.gameObject.SetActive(true);
        frameButton.sprite = frameButtonWeaponUpgradees[2];
    }

    public void ChangeSpriteBlockUpgradee(Image frameButton, TextMeshProUGUI textPrice, TextMeshProUGUI textMax)
    {
        textPrice.gameObject.SetActive(false);
        textMax.gameObject.SetActive(true);
        frameButton.sprite = frameButtonBlockUpgradees[2];
    }

    public void ChangeSpriteBlockUpgradee(Type type, Image frameButton)
    {
        int index = 0;
        if (type == Type.NOT_ENOUGH_MONEY) index = 1;
        frameButton.sprite = frameButtonBlockUpgradees[index];
    }
}
