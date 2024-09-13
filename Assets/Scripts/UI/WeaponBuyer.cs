using TMPro;
using UnityEngine.UI;

public class WeaponBuyer : ButtonClicker
{
    public TextMeshProUGUI textPrice;
    public Image frameButton;
    public Image frameGold;
    public BlockUpgradeHandler blockUpgradeHandler;

    public virtual void Buy() { }

    public virtual void CheckButtonState() { }
}


