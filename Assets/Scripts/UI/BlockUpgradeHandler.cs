using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BlockUpgradeHandler : MonoBehaviour
{
    public Block blockInfo;
    public GameObject[] weaponBuyers;
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
    public WeaponUpgradeHandler weaponUpgradeHandler;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Upgrade();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (PlayerPrefs.GetInt("Gold") < DataManager.instance.sawData.price) return;
            BuyWeapon(GameController.WEAPON.SAW, 0);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (PlayerPrefs.GetInt("Gold") < DataManager.instance.flameData.price) return;
            BuyWeapon(GameController.WEAPON.FLAME, 0);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (PlayerPrefs.GetInt("Gold") < DataManager.instance.machineGunData.price) return;
            BuyWeapon(GameController.WEAPON.MACHINE_GUN, 0);
        }
    }

    public void OnEnable()
    {
        //
        CheckButtonState();
    }

    public void LoadData(int blockLevel)
    {
        GameController.WEAPON weaponType = DataManager.instance.playerData.weaponType;
        if (weaponType == GameController.WEAPON.NONE) return;
        int weaponLevel = DataManager.instance.playerData.weaponLevel;

        blockInfo.level = blockLevel;
        UpgradeHandle();
        BuyWeapon(weaponType, weaponLevel);

        weaponUpgradeHandler.LoadData(weaponLevel);
    }

    public void BuyWeapon(GameController.WEAPON weaponType, int weaponLevel)
    {
        GameObject[] weapons = null;
        if (weaponType == GameController.WEAPON.SAW)
        {
            weapons = saws;
            weaponUpgradeHandler.priceUpgrades = DataManager.instance.sawData.priceUpgrades;
            weaponUpgradeHandler.damages = DataManager.instance.sawData.damages;
        }
        if (weaponType == GameController.WEAPON.FLAME)
        {
            weapons = flames;
            weaponUpgradeHandler.priceUpgrades = DataManager.instance.flameData.priceUpgrades;
            weaponUpgradeHandler.damages = DataManager.instance.flameData.damages;
        }
        if (weaponType == GameController.WEAPON.MACHINE_GUN)
        {
            weapons = machineGuns;
            weaponUpgradeHandler.priceUpgrades = DataManager.instance.machineGunData.priceUpgrades;
            weaponUpgradeHandler.damages = DataManager.instance.machineGunData.damages;
        }
        SetActiveWeaponBuyer(false);
        weaponUpgrade.SetActive(true);
        weapons[weaponLevel].SetActive(true);
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
        if (blockInfo.level == DataManager.instance.blockData.hps.Length - 1 || PlayerPrefs.GetInt("Gold") < DataManager.instance.blockData.prices[blockInfo.level]) return;
        blockInfo.level++;
        UpgradeHandle();
        CheckButtonState();
    }

    public void CheckButtonState()
    {
        if (blockInfo.level == DataManager.instance.blockData.hps.Length - 1) UIHandler.instance.ChangeSpriteBlockUpgradee(frameButton, textPrice, textMax);
        else if (PlayerPrefs.GetInt("Gold") < DataManager.instance.blockData.prices[blockInfo.level]) UIHandler.instance.ChangeSpriteBlockUpgradee(UIHandler.Type.NOT_ENOUGH_MONEY, frameButton);
        else UIHandler.instance.ChangeSpriteBlockUpgradee(UIHandler.Type.ENOUGH_MONEY, frameButton);
    }

    public void UpgradeHandle()
    {
        textLv.text = "Lv" + (blockInfo.level + 1);
        spriteRenderer.sprite = DataManager.instance.blockSprites[blockInfo.level];
        int hp = DataManager.instance.blockData.hps[blockInfo.level];
        textHp.text = hp >= 1000 ? Mathf.Floor(hp / 100) / 10 + "K" : hp.ToString();
        textPrice.text = DataManager.instance.blockData.prices[blockInfo.level].ToString();
    }

    public void Selected()
    {
        transform.localScale = Vector3.one * 1.55f;
        sortingGroup.sortingLayerName = "UI";
    }

    public void DeSelected()
    {
        transform.localScale = Vector3.one;
        sortingGroup.sortingLayerName = "Default";
    }
}
