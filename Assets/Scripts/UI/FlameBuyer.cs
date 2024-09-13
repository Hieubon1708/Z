using UnityEngine;
using UnityEngine.EventSystems;

public class FlameBuyer : WeaponBuyer
{
    public void Start()
    {
        textPrice.text = DataManager.instance.flameData.price.ToString();
    }

    public override void Buy()
    {
        blockUpgradeHandler.BuyWeapon(GameController.WEAPON.FLAME, 0);
    }

    public override void CheckButtonState()
    {
        if (PlayerPrefs.GetInt("Gold") < DataManager.instance.flameData.price) UIHandler.instance.ChangeSpriteWeaponBuyer(UIHandler.Type.NOT_ENOUGH_MONEY, frameButton, frameGold);
        else UIHandler.instance.ChangeSpriteWeaponBuyer(UIHandler.Type.ENOUGH_MONEY, frameButton, frameGold);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (PlayerPrefs.GetInt("Gold") < DataManager.instance.flameData.price) return;
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (PlayerPrefs.GetInt("Gold") < DataManager.instance.flameData.price) return;
        base.OnPointerUp(eventData);
        Buy();
    }
}
