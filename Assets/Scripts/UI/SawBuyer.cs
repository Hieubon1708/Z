using TMPro;
using UnityEngine;

public class SawBuyer : WeaponBuyer
{
    public void Start()
    {
        textPrice.text = DataManager.instance.sawData.price.ToString();
    }

    public void OnEnable()
    {
        //
        CheckButtonState();
    }

    public override void Buy()
    {
        if (PlayerPrefs.GetInt("Gold") < DataManager.instance.sawData.price) return;
        blockUpgradeHandler.BuyWeapon(GameController.WEAPON.SAW, 0);
    }

    public override void CheckButtonState()
    {
        if (PlayerPrefs.GetInt("Gold") < DataManager.instance.sawData.price) UIHandler.instance.ChangeSpriteWeaponBuyer(UIHandler.Type.NOT_ENOUGH_MONEY, frameButton, frameGold);
        else UIHandler.instance.ChangeSpriteWeaponBuyer(UIHandler.Type.ENOUGH_MONEY, frameButton, frameGold);
    }
}
