using UnityEngine;
using UnityEngine.EventSystems;

public class MachineGunBuyer : WeaponBuyer
{
    public void Start()
    {
        textPrice.text = DataManager.instance.machineGunData.price.ToString();
    }

    public override void Buy()
    {
        blockUpgradeHandler.BuyWeapon(GameController.WEAPON.MACHINE_GUN, 0);
    }

    public override void CheckButtonState()
    {
        if (PlayerPrefs.GetInt("Gold") < DataManager.instance.sawData.price) UIHandler.instance.ChangeSpriteWeaponBuyer(UIHandler.Type.NOT_ENOUGH_MONEY, frameButton, frameGold);
        else UIHandler.instance.ChangeSpriteWeaponBuyer(UIHandler.Type.ENOUGH_MONEY, frameButton, frameGold);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (PlayerPrefs.GetInt("Gold") < DataManager.instance.machineGunData.price) return;
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (PlayerPrefs.GetInt("Gold") < DataManager.instance.machineGunData.price) return;
        base.OnPointerUp(eventData);
        Buy();
    }
}
