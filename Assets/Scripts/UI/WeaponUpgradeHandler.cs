using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradeHandler : MonoBehaviour
{
    public int level;
    public int levelUpgrade;
    public TextMeshProUGUI textLv;
    public TextMeshProUGUI textDamage;
    public TextMeshProUGUI textPriceUpgrade;
    public Image frameUpgrade;
    public Image frameLastUpgrade;
    public TextMeshProUGUI textMax;
    public TextMeshProUGUI textLastUpgrade;
    public GameObject lastUpgrade;
    public GameObject[] boxProgress;
    public int[][] priceUpgrades;
    public int[][] damages;

    public void LoadData(int level)
    {
        levelUpgrade = DataManager.instance.playerData.weaponLevelUpgrade;
        this.level = level;
        UpgradeHandle();
    }

    public void OnEnable()
    {
        //
        CheckButtonState();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Upgrade();
        }
    }

    public void Upgrade()
    {
        if (level == priceUpgrades.Length || PlayerPrefs.GetInt("Gold") < priceUpgrades[level][levelUpgrade]) return;
        levelUpgrade++;
        if (lastUpgrade.activeSelf)
        {
            lastUpgrade.SetActive(false);
            levelUpgrade = 0;
        }
        CheckButtonState();
        UpgradeHandle();
    }

    public void UpgradeHandle()
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
            level++;
            if (level != priceUpgrades.Length) ChangeTextUpgrade();
        }
    }

    void ChangeTextUpgrade()
    {
        lastUpgrade.SetActive(true);
        textLastUpgrade.text = "Lv" + level + " > " + "Lv" + (level + 1) + " UPGRADE";
    }

    public void CheckButtonState()
    {
        if (level == priceUpgrades.Length - 1 && levelUpgrade == priceUpgrades[level].Length - 1) UIHandler.instance.ChangeSpriteWeaponUpgradee(frameUpgrade, textPriceUpgrade, textMax);
        else if (PlayerPrefs.GetInt("Gold") < priceUpgrades[level][levelUpgrade])
        {
            if (levelUpgrade == boxProgress.Length) UIHandler.instance.ChangeSpriteWeaponLastUpgradee(UIHandler.Type.NOT_ENOUGH_MONEY, frameLastUpgrade);
            else UIHandler.instance.ChangeSpriteWeaponUpgradee(UIHandler.Type.NOT_ENOUGH_MONEY, frameUpgrade);
        }
        else
        {
            if (levelUpgrade == boxProgress.Length) UIHandler.instance.ChangeSpriteWeaponLastUpgradee(UIHandler.Type.ENOUGH_MONEY, frameLastUpgrade);
            else UIHandler.instance.ChangeSpriteWeaponUpgradee(UIHandler.Type.ENOUGH_MONEY, frameUpgrade);
        }
    }
}
