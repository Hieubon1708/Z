using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameController;

public class WeaponUpgradeHandler : ButtonUpgradee
{
    public int level;
    public int levelUpgrade;
    public TextMeshProUGUI textLv;
    public TextMeshProUGUI textDamage;
    public Image frameLastUpgrade;
    public TextMeshProUGUI textMax;
    public TextMeshProUGUI textLastUpgrade;
    public GameObject lastUpgrade;
    public GameObject[] boxProgress;
    public BlockUpgradeHandler blockUpgradeHandler;
    public WEAPON weaponType;
    public GameObject weapon;
    public int[][] priceUpgrades;
    public int[][] damages;

    public void LoadData(int level, int levelUpgrade)
    {
        this.levelUpgrade = levelUpgrade;
        this.level = level;
        UpgradeHandle();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Upgrade();
        }
    }

    public override void Upgrade()
    {
        levelUpgrade++;
        if (lastUpgrade.activeSelf)
        {
            blockUpgradeHandler.BuyWeapon(weaponType, level);
            lastUpgrade.SetActive(false);
            levelUpgrade = 0;
            level++;
        }
        UpgradeHandle();
    }

    public override void UpgradeHandle()
    {
        if (levelUpgrade < boxProgress.Length)
        {
            textLv.text = "Lv" + (level + 1);
            textDamage.text = damages[level][levelUpgrade].ToString();
        }
        textPriceUpgrade.text = priceUpgrades[level][levelUpgrade].ToString();
        for (int i = 0; i < boxProgress.Length; i++)
        {

            if (i > levelUpgrade - 1 || levelUpgrade == boxProgress.Length) boxProgress[i].SetActive(false);
            else boxProgress[i].SetActive(true);
        }
        if (levelUpgrade == boxProgress.Length)
        {
            if (level != priceUpgrades.Length - 1) ChangeTextUpgrade();
            levelUpgrade = 0;
        }
    }

    void ChangeTextUpgrade()
    {
        lastUpgrade.SetActive(true);
        textLastUpgrade.text = "Lv" + (level + 1) + " > " + "Lv" + (level + 2) + " UPGRADE";
    }

    public override void CheckButtonState()
    {
        if (weapon == null) return;
        if (level == priceUpgrades.Length - 1 && levelUpgrade == priceUpgrades[level].Length - 1) UIHandler.instance.ChangeSpriteWeaponUpgradee(frame, textPriceUpgrade, textMax);
        else if (DataManager.instance.playerData.gold < priceUpgrades[level][levelUpgrade])
        {
            if (levelUpgrade == boxProgress.Length) UIHandler.instance.ChangeSpriteWeaponLastUpgradee(UIHandler.Type.NOT_ENOUGH_MONEY, frameLastUpgrade);
            else UIHandler.instance.ChangeSpriteWeaponUpgradee(UIHandler.Type.NOT_ENOUGH_MONEY, frame);
        }
        else
        {
            if (levelUpgrade == boxProgress.Length) UIHandler.instance.ChangeSpriteWeaponLastUpgradee(UIHandler.Type.ENOUGH_MONEY, frameLastUpgrade);
            else UIHandler.instance.ChangeSpriteWeaponUpgradee(UIHandler.Type.ENOUGH_MONEY, frame);
        }
    }

    public void ResetData()
    {
        if (weapon == null) return;
        weapon.SetActive(false);
        level = 0;
        levelUpgrade = 0;
        lastUpgrade.SetActive(false);
        UpgradeHandle();
    }
}
