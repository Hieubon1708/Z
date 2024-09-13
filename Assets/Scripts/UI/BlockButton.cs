using UnityEngine;
using UnityEngine.EventSystems;

public class BlockButton : ButtonClicker
{
    public BlockUpgradeHandler blockUpgradeHandler;

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (blockUpgradeHandler.textMax.gameObject.activeSelf || PlayerPrefs.GetInt("Gold") < DataManager.instance.blockData.priceUpgrades[blockUpgradeHandler.blockInfo.level]) return;
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (blockUpgradeHandler.textMax.gameObject.activeSelf || PlayerPrefs.GetInt("Gold") < DataManager.instance.blockData.priceUpgrades[blockUpgradeHandler.blockInfo.level]) return;
        base.OnPointerUp(eventData);
        blockUpgradeHandler.Upgrade();
    }
}
