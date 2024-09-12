using TMPro;
using UnityEngine;

public class FlameBuyer : WeaponBuyer
{
    public void Start()
    {
        textPrice.text = DataManager.instance.flameData.price.ToString();
    }

    public void OnEnable()
    {
        //
        CheckButtonState();
    }

    public override void Buy()
    {
        if (PlayerPrefs.GetInt("Gold") < DataManager.instance.flameData.price) return;
        blockUpgradeHandler.BuyWeapon(GameController.WEAPON.FLAME, 0);
    }

    public override void CheckButtonState()
    {
        if (PlayerPrefs.GetInt("Gold") < DataManager.instance.flameData.price) UIHandler.instance.ChangeSpriteWeaponBuyer(UIHandler.Type.NOT_ENOUGH_MONEY, frameButton, frameGold);
        else UIHandler.instance.ChangeSpriteWeaponBuyer(UIHandler.Type.ENOUGH_MONEY, frameButton, frameGold);
    }
}
