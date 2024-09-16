using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static GameController;

public class BlockController : MonoBehaviour
{
    public static BlockController instance;

    public List<GameObject> blockPools = new List<GameObject>();
    public List<Block> scBlocks = new List<Block>();
    public List<GameObject> blocks = new List<GameObject>();
    public float startY;
    public float startYPlayer;
    public float distance;
    public Transform container;
    public Transform player;
    public GameObject preBlock;
    public ButtonBuyer blockBuyer;
    public EnergyUpgradeHandler energyUpgradee;
    public int count;

    public void Awake()
    {
        instance = this;
        Generate();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddBlock();
        }
    }

    private void Start()
    {
        LoadData();
        CheckButtonStateAll();
    }

    void LoadData()
    {
        IngameData[] ingameDatas = DataManager.instance.ingameDatas;
        for (int i = 0; i < ingameDatas.Length; i++)
        {
            GameObject block = blockPools[0];
            blockPools.Remove(block);
            block.transform.localPosition = new Vector2(block.transform.localPosition.x, startY + distance * blocks.Count);
            blocks.Add(block);
            block.SetActive(true);
            player.transform.localPosition = new Vector2(player.transform.localPosition.x, startYPlayer + distance * blocks.Count);
            Block scBlock = GetScBlock(block);
            scBlock.gold = ingameDatas[i].blockGold;

            BlockUpgradeHandler blockUpgradeHandler = GetScBlock(block).blockUpgradeHandler;

            int blockLevel = ingameDatas[i].blockLevel;
            WEAPON weaponType = ingameDatas[i].weaponType;
            int weaponLevel = ingameDatas[i].weaponLevel;
            int weaponLevelUpgrade = ingameDatas[i].weaponLevelUpgrade;

            blockUpgradeHandler.LoadData(blockLevel, weaponType, weaponLevel, weaponLevelUpgrade);
        }
        blockBuyer.LoadData();
        energyUpgradee.LoadData();
    }

    public void CheckButtonStateAll()
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            GetScBlock(blocks[i]).blockUpgradeHandler.CheckButtonStateInBlock();
        }
        blockBuyer.CheckButtonState();
        energyUpgradee.CheckButtonState();
    }

    public void AddBlock()
    {
        if (blockPools.Count != 0)
        {
            GameObject block = blockPools[0];
            Block scBlock = GetScBlock(block);
            blockPools.Remove(block);
            block.transform.localPosition = new Vector2(block.transform.localPosition.x, startY + distance * blocks.Count);
            blocks.Add(block);
            block.SetActive(true);
            player.transform.localPosition = new Vector2(player.transform.localPosition.x, startYPlayer + distance * blocks.Count);
            scBlock.blockUpgradeHandler.UpgradeHandle();
            scBlock.AddBlockAni();
            CarController.instance.AddBookAni();
            PlayerController.instance.AddBookAni();
            CheckButtonStateAll();
            scBlock.GoldHandle(DataManager.instance.blockData.price);
        }
    }

    public void SetPositionNearest(GameObject block, GameObject frame)
    {
        int indexNearest = GetIndexNearest(block);
        if (block == blocks[indexNearest]) return;
        Swap(blocks.IndexOf(block), indexNearest);
        for (int i = 0; i < blocks.Count; i++)
        {
            float y = startY + distance * i;
            if (blocks[i] != block) blocks[i].transform.localPosition = new Vector2(blocks[i].transform.localPosition.x, y);
            else frame.transform.position = new Vector2(frame.transform.position.x, y + CarController.instance.transform.localPosition.y);
        }
    }

    void Swap(int index1, int index2)
    {
        GameObject temp = blocks[index1];
        blocks[index1] = blocks[index2];
        blocks[index2] = temp;
    }

    int GetIndexNearest(GameObject block)
    {
        int indexNearest = -1;
        float min = int.MaxValue;
        for (int i = 0; i < blocks.Count; i++)
        {
            float y = startY + distance * i + CarController.instance.transform.localPosition.y;
            float distanceY = Mathf.Abs(block.transform.position.y - y);
            if (distanceY < min)
            {
                min = distanceY;
                indexNearest = i;
            }
        }
        return indexNearest;
    }

    public void DeleteBlock(GameObject block)
    {
        if (blockPools.Count == 0) blockBuyer.gameObject.SetActive(true);
        Block scBlock = GetScBlock(block);
        block.SetActive(false);
        blockPools.Add(block);
        blocks.Remove(block);
        scBlock.DeleteBlockAni();
        CarController.instance.DeleteBookAni();
        PlayerController.instance.DeleteBookAni();
        for (int i = 0; i < blocks.Count; i++)
        {
            blocks[i].transform.localPosition = new Vector2(blocks[i].transform.localPosition.x, startY + distance * i);
        }
        player.transform.localPosition = new Vector2(player.transform.localPosition.x, startYPlayer + distance * blocks.Count);
    }

    public Block GetScBlock(GameObject block)
    {
        for (int i = 0; i < scBlocks.Count; i++)
        {
            if (scBlocks[i].gameObject == block)
            {
                return scBlocks[i];
            }
        }
        return null;
    }

    void Generate()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject blockIns = Instantiate(preBlock, container);
            blockIns.name = i.ToString();
            blockIns.SetActive(false);
            Block scBlock = blockIns.GetComponent<Block>();
            blockPools.Add(blockIns);
            scBlocks.Add(scBlock);
        }
    }

    public void OnApplicationQuit()
    {
        List<IngameData> listData = new List<IngameData>();
        for (int i = 0; i < blocks.Count; i++)
        {
            Block scBlock = blocks[i].GetComponent<Block>();
            int blockLevel = scBlock.level;
            WEAPON weaponType = scBlock.blockUpgradeHandler.weaponUpgradeHandler.weaponType;
            int weaponLevel = scBlock.blockUpgradeHandler.weaponUpgradeHandler.level;
            int weaponUpgradeLevel = scBlock.blockUpgradeHandler.weaponUpgradeHandler.levelUpgrade;

            /*Debug.LogWarning(blockLevel);
            Debug.LogWarning(weaponType);
            Debug.LogWarning(weaponLevel);
            Debug.LogWarning(weaponUpgradeLevel);
            Debug.LogWarning("--------------------");*/

            IngameData ingameData = new IngameData(blockLevel, weaponType, weaponLevel, weaponUpgradeLevel);
            listData.Add(ingameData);
        }

        string jsIngame = JsonConvert.SerializeObject(listData);
        string filePathIngame = Path.Combine(Application.persistentDataPath, "IngameData.json");
        File.WriteAllText(filePathIngame, jsIngame);

        string jsPlayer = JsonConvert.SerializeObject(DataManager.instance.playerData);
        string filePathPlayer = Path.Combine(Application.persistentDataPath, "PLayerData.json");
        File.WriteAllText(filePathPlayer, jsPlayer);


    }
}
