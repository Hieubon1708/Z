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
    public Image frameButton;
    public TextMeshProUGUI textMax;
    public TextMeshProUGUI textUpgrade;
    public GameObject buttontUpgrade;
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
        //if (level == priceUpgrades.Length - 1 && levelUpgrade == priceUpgrades[level].Length - 1 || PlayerPrefs.GetInt("Gold") < priceUpgrades[level][levelUpgrade]) return;
        levelUpgrade++;
        UpgradeHandle();
    }

    public void UpgradeHandle()
    {
        if(levelUpgrade == 0) buttontUpgrade.SetActive(false);
        if (levelUpgrade < boxProgress.Length)
        {
            textLv.text = level + 1.ToString();
            textDamage.text = DataManager.instance.sawData.damages[level][levelUpgrade].ToString();
        }
        textPriceUpgrade.text = DataManager.instance.sawData.priceUpgrades[level][levelUpgrade].ToString();
        for (int i = 0; i < boxProgress.Length; i++)
        {
            if (i <= levelUpgrade - 1)
            {
                boxProgress[i].SetActive(true);
                if (levelUpgrade == boxProgress.Length)
                {
                    level++;
                    ChangeTextUpgrade();
                    levelUpgrade = -1;
                }
            }
            else boxProgress[i].SetActive(false);
        }
    }

    void ChangeTextUpgrade()
    {
        buttontUpgrade.SetActive(true);
        textUpgrade.text = "Lv" + level + " > " + "Lv" + (level + 1) + " UPGRADE";
    }

    public void CheckButtonState()
    {
        if (level == priceUpgrades.Length - 1 && levelUpgrade == priceUpgrades[level].Length - 1) UIHandler.instance.ChangeSpriteWeaponUpgradee(frameButton, textPriceUpgrade, textMax);
        else if (PlayerPrefs.GetInt("Gold") < priceUpgrades[level][levelUpgrade]) UIHandler.instance.ChangeSpriteWeaponUpgradee(UIHandler.Type.NOT_ENOUGH_MONEY, frameButton);
        else UIHandler.instance.ChangeSpriteWeaponUpgradee(UIHandler.Type.ENOUGH_MONEY, frameButton);
    }
}
