using Newtonsoft.Json;
using System.IO;
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
    public EnergyData energyData;
    public PlayerData playerData;
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
        TextAsset jsEnergy= Resources.Load<TextAsset>("Datas/EnergyData");

        blockData = JsonConvert.DeserializeObject<BlockData>(jsBlock.text);
        sawData = JsonConvert.DeserializeObject<SawData>(jsSaw.text);
        flameData = JsonConvert.DeserializeObject<FlameData>(jsFlame.text);
        machineGunData = JsonConvert.DeserializeObject<MachineGunData>(jsMachineGun.text);
        energyData = JsonConvert.DeserializeObject<EnergyData>(jsEnergy.text);

        string jsIngame = Path.Combine(Application.persistentDataPath, "IngameData.json");
        if (File.Exists(jsIngame))
        {
            string jsonContent = File.ReadAllText(jsIngame);
            ingameDatas = JsonConvert.DeserializeObject<IngameData[]>(jsonContent);
        }
        else Debug.LogWarning("File not found: " + jsIngame);
        
        string jsPlayer = Path.Combine(Application.persistentDataPath, "PlayerData.json");
        if (File.Exists(jsPlayer))
        {
            string jsonContent = File.ReadAllText(jsPlayer);
            playerData = JsonConvert.DeserializeObject<PlayerData>(jsonContent);
        }
        else
        {
            Debug.LogWarning("File not found: " + jsPlayer);
            playerData = new PlayerData();
        }
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
public class EnergyData
{
    public float[] times;
    public int[] priceUpgrades;
}

[System.Serializable]
public class PlayerData
{
    public int gold;
    public int indexEnergy;
}

[System.Serializable]
public class IngameData
{
    public int blockLevel;
    public int blockGold;
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

