using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponButton : ButtonClicker
{
    public WeaponUpgradeHandler weaponUpgradeHandler;

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (weaponUpgradeHandler.textMax.gameObject.activeSelf || PlayerPrefs.GetInt("Gold") < weaponUpgradeHandler.priceUpgrades[weaponUpgradeHandler.level][weaponUpgradeHandler.levelUpgrade]) return;
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (weaponUpgradeHandler.textMax.gameObject.activeSelf || PlayerPrefs.GetInt("Gold") < weaponUpgradeHandler.priceUpgrades[weaponUpgradeHandler.level][weaponUpgradeHandler.levelUpgrade]) return;
        base.OnPointerUp(eventData);
        weaponUpgradeHandler.Upgrade();
    }
}
