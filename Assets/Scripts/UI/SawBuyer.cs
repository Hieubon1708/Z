using UnityEngine;
using UnityEngine.EventSystems;

public class SawBuyer : WeaponBuyer
{
    public void Start()
    {
        textPrice.text = DataManager.instance.sawData.price.ToString();
    }

    public override void Buy()
    {
        blockUpgradeHandler.BuyWeapon(GameController.WEAPON.SAW, 0);
    }

    public override void CheckButtonState()
    {
        if (PlayerPrefs.GetInt("Gold") < DataManager.instance.sawData.price) UIHandler.instance.ChangeSpriteWeaponBuyer(UIHandler.Type.NOT_ENOUGH_MONEY, frameButton, frameGold);
        else UIHandler.instance.ChangeSpriteWeaponBuyer(UIHandler.Type.ENOUGH_MONEY, frameButton, frameGold);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (PlayerPrefs.GetInt("Gold") < DataManager.instance.sawData.price) return;
        base.OnPointerUp(eventData);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (PlayerPrefs.GetInt("Gold") < DataManager.instance.sawData.price) return;
        base.OnPointerDown(eventData);
        Buy();
    }
}
