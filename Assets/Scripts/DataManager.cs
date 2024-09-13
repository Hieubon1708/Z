using Newtonsoft.Json;
using UnityEngine;
using static GameController;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public Sprite[] blockSprites;
    public BlockData blockData;
    public SawData sawData;
    public FlameData flameData;
    public MachineGunData machineGunData;
    public IngameData[] ingameDatas;

    public void Awake()
    {
        instance = this;
        DataReader();
    }

    void DataReader()
    {
        TextAsset jsBlock = Resources.Load<TextAsset>("Datas/BlockData");
        TextAsset jsSaw = Resources.Load<TextAsset>("Datas/SawData");
        TextAsset jsFlame = Resources.Load<TextAsset>("Datas/FlameData");
        TextAsset jsMachineGun = Resources.Load<TextAsset>("Datas/MachineGunData");
        if (jsBlock != null)
        {
            blockData = JsonConvert.DeserializeObject<BlockData>(jsBlock.text);
            if (blockData != null)
            {
                for (int i = 0; i < blockData.hps.Length; i++)
                {
                    //Debug.LogWarning(blockData.hps[i]);
                }
            }
            else
            {
                Debug.Log("Failed to parse JSON file.");
            }
        }
        else
        {
            Debug.Log("JSON file not found.");
        }
        sawData = JsonConvert.DeserializeObject<SawData>(jsSaw.text);
        flameData = JsonConvert.DeserializeObject<FlameData>(jsFlame.text);
        machineGunData = JsonConvert.DeserializeObject<MachineGunData>(jsMachineGun.text);
    }
}

[System.Serializable]
public class BlockData
{
    public int price;
    public int[] hps;
    public int[] priceUpgrades;
}

[System.Serializable]
public class SawData
{
    public int price;
    public int[][] priceUpgrades;
    public int[][] damages;
}

[System.Serializable]
public class FlameData
{
    public int price;
    public int[][] priceUpgrades;
    public int[][] damages;
}

[System.Serializable]
public class MachineGunData
{
    public int price;
    public int[][] priceUpgrades;
    public int[][] damages;
}

[System.Serializable]
public class IngameData
{
    public int blockLevel;
    public WEAPON weaponType;
    public int weaponLevel;
    public int weaponLevelUpgrade;

    public IngameData(int blockLevel, WEAPON weaponType, int weaponLevel, int weaponLevelUpgrade)
    {
        this.blockLevel = blockLevel;
        this.weaponType = weaponType;
        this.weaponLevel = weaponLevel;
        this.weaponLevelUpgrade = weaponLevelUpgrade;
    }
}

