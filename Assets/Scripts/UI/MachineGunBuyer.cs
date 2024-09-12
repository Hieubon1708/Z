using TMPro;
using UnityEngine;

public class MachineGunBuyer : WeaponBuyer
{
    public void Start()
    {
        textPrice.text = DataManager.instance.machineGunData.price.ToString();
    }

    public void OnEnable()
    {
        //
        CheckButtonState();
    }

    public override void Buy()
    {
        if (PlayerPrefs.GetInt("Gold") < DataManager.instance.machineGunData.price) return;
        blockUpgradeHandler.BuyWeapon(GameController.WEAPON.MACHINE_GUN, 0);
    }

    public override void CheckButtonState()
    {
        if (PlayerPrefs.GetInt("Gold") < DataManager.instance.sawData.price) UIHandler.instance.ChangeSpriteWeaponBuyer(UIHandler.Type.NOT_ENOUGH_MONEY, frameButton, frameGold);
        else UIHandler.instance.ChangeSpriteWeaponBuyer(UIHandler.Type.ENOUGH_MONEY, frameButton, frameGold);
    }
}
