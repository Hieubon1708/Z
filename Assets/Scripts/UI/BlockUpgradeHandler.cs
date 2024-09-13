using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static GameController;

public class BlockUpgradeHandler : MonoBehaviour
{
    public Block blockInfo;
    public GameObject[] weaponBuyers;
    public WeaponBuyer[] scWeaponBuyers;
    public SpriteRenderer spriteRenderer;
    public SortingGroup sortingGroup;
    public TextMeshProUGUI textLv;
    public TextMeshProUGUI textHp;
    public TextMeshProUGUI textPrice;
    public TextMeshProUGUI textMax;
    public Image frameButton;
    public GameObject[] saws;
    public GameObject[] flames;
    public GameObject[] machineGuns;
    public GameObject weaponUpgrade;
    public GameObject canvas;
    public WeaponUpgradeHandler weaponUpgradeHandler;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CheckButtonStateInBlock();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Upgrade();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (PlayerPrefs.GetInt("Gold") < DataManager.instance.sawData.price) return;
            BuyWeapon(WEAPON.SAW, 0);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (PlayerPrefs.GetInt("Gold") < DataManager.instance.flameData.price) return;
            BuyWeapon(WEAPON.FLAME, 0);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (PlayerPrefs.GetInt("Gold") < DataManager.instance.machineGunData.price) return;
            BuyWeapon(WEAPON.MACHINE_GUN, 0);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetData();
        }
    }

    public void LoadData(int blockLevel, WEAPON weaponType, int weaponLevel, int levelUpgrade)
    {
        blockInfo.level = blockLevel;
        UpgradeHandle();

        if (weaponType == WEAPON.NONE) return;
        BuyWeapon(weaponType, weaponLevel);
        weaponUpgradeHandler.LoadData(weaponLevel, levelUpgrade);
    }

    public void BuyWeapon(WEAPON weaponType, int weaponLevel)
    {
        GameObject[] weapons = null;
        weaponUpgradeHandler.weaponType = weaponType;
        if (weaponType == WEAPON.SAW)
        {
            weapons = saws;
            weaponUpgradeHandler.priceUpgrades = DataManager.instance.sawData.priceUpgrades;
            weaponUpgradeHandler.damages = DataManager.instance.sawData.damages;
        }
        if (weaponType == WEAPON.FLAME)
        {
            weapons = flames;
            weaponUpgradeHandler.priceUpgrades = DataManager.instance.flameData.priceUpgrades;
            weaponUpgradeHandler.damages = DataManager.instance.flameData.damages;
        }
        if (weaponType == WEAPON.MACHINE_GUN)
        {
            weapons = machineGuns;
            weaponUpgradeHandler.priceUpgrades = DataManager.instance.machineGunData.priceUpgrades;
            weaponUpgradeHandler.damages = DataManager.instance.machineGunData.damages;
        }
        SetActiveWeaponBuyer(false);
        weaponUpgrade.SetActive(true);
        if(weaponLevel > 0) weapons[weaponLevel -1].SetActive(false);
        weapons[weaponLevel].SetActive(true);
        weaponUpgradeHandler.weapon = weapons[weaponLevel];
        weaponUpgradeHandler.UpgradeHandle();
    }

    public void SetActiveWeaponBuyer(bool isActive)
    {
        for (int i = 0; i < weaponBuyers.Length; i++)
        {
            weaponBuyers[i].SetActive(isActive);
        }
    }

    public void Upgrade()
    {
        blockInfo.level++;
        UpgradeHandle();
    }

    public void CheckButtonStateInBlock()
    {
        for(int i = 0; i < scWeaponBuyers.Length; i++)
        {
            scWeaponBuyers[i].CheckButtonState();
        }
        CheckButtonState();
        weaponUpgradeHandler.CheckButtonState();
    }

    public void CheckButtonState()
    {
        if (blockInfo.level == DataManager.instance.blockData.hps.Length - 1) UIHandler.instance.ChangeSpriteBlockUpgradee(frameButton, textPrice, textMax);
        else if (PlayerPrefs.GetInt("Gold") < DataManager.instance.blockData.priceUpgrades[blockInfo.level]) UIHandler.instance.ChangeSpriteBlockUpgradee(UIHandler.Type.NOT_ENOUGH_MONEY, frameButton);
        else UIHandler.instance.ChangeSpriteBlockUpgradee(UIHandler.Type.ENOUGH_MONEY, frameButton);
    }

    public void UpgradeHandle()
    {
        textLv.text = "Lv" + (blockInfo.level + 1);
        spriteRenderer.sprite = DataManager.instance.blockSprites[blockInfo.level];
        int hp = DataManager.instance.blockData.hps[blockInfo.level];
        textHp.text = hp >= 1000 ? Mathf.Floor(hp / 100) / 10 + "K" : hp.ToString();
        textPrice.text = DataManager.instance.blockData.priceUpgrades[blockInfo.level].ToString();
    }

    public void ResetData()
    {
        blockInfo.level = 0;
        SetActiveWeaponBuyer(true);
        weaponUpgrade.SetActive(false);
        UpgradeHandle();
        CheckButtonState();
        textPrice.gameObject.SetActive(true);
        textMax.gameObject.SetActive(false);

        weaponUpgradeHandler.ResetData();
    }

    public void Selected()
    {
        canvas.SetActive(false);
        transform.localScale = Vector3.one * 1.55f;
        sortingGroup.sortingLayerName = "UI";
    }

    public void DeSelected()
    {
        canvas.SetActive(true);
        transform.localScale = Vector3.one;
        sortingGroup.sortingLayerName = "Default";
    }
}
