using DG.Tweening;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public DataManager dataManager;
    public CarController carController;

    public List<GameObject> listEnemies;
    public GameObject[] mapLevels;

    public Dictionary<GameObject, int> listDamages = new Dictionary<GameObject, int>();

    public Transform poolDamages;
    public Transform poolBullets;
    public Transform poolEnemies;

    public float timeSawDamage;
    public float timeFlameDamage;

    public float backgroundSpeed;

    public bool isStart;
    public Camera cam;

    private void Awake()
    {
        instance = this;
        DOTween.SetTweensCapacity(200, 1000);
        Resize();
        MapGenerate(dataManager.playerData.gameLevel);
    }

    void MapGenerate(int index)
    {
        Instantiate(mapLevels[index], transform);
    }

    public void Start()
    {
        ChangeBlockSprites(dataManager.playerData.gameLevel);
        BlockController.instance.LoadData();
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            DataManager.instance.playerData.gameLevel = Mathf.Clamp(++DataManager.instance.playerData.gameLevel, 0, 5);
            ChangeBlockSprites(DataManager.instance.playerData.gameLevel);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DataManager.instance.playerData.gameLevel = Mathf.Clamp(--DataManager.instance.playerData.gameLevel, 0, 5);
            ChangeBlockSprites(DataManager.instance.playerData.gameLevel);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            StartGame();
        }
    }

    public void ChangeBlockSprites(int level)
    {
        dataManager.SetBlockSprites(level);
        BlockController.instance.ResetBlockSprites();
    }

    void Resize()
    {
        float defaultSize = cam.orthographicSize;
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = 10.8f / 19.2f;
        if (screenRatio < targetRatio)
        {
            float changeSize = targetRatio / screenRatio;
            cam.orthographicSize = defaultSize * changeSize;
        }
    }

    public void AddKeyDamage(GameObject obj, int damage)
    {
        if(!listDamages.ContainsKey(obj))
        {
            listDamages.Add(obj, damage);
        }
        else
        {
            listDamages[obj] = damage;
        }
    }

    public void StartGame()
    {
        BlockController.instance.SetActiveUI(false);
        EnemyTowerController.instance.NextTower();
        CarController.instance.multiplier = 1;
        isStart = true;
    }

    public void RemoveKeyDamage(GameObject obj)
    {
        listDamages.Remove(obj);
    }

    public enum WEAPON
    {
        NONE, SAW, FLAME, MACHINE_GUN
    }

    public void OnDestroy()
    {
        List<IngameData> listData = new List<IngameData>();
        for (int i = 0; i < BlockController.instance.blocks.Count; i++)
        {
            Block scBlock = BlockController.instance.blocks[i].GetComponent<Block>();
            int blockLevel = scBlock.level;
            int blockGold = scBlock.gold;
            WEAPON weaponType = scBlock.blockUpgradeHandler.weaponUpgradeHandler.weaponType;
            int weaponLevel = scBlock.blockUpgradeHandler.weaponUpgradeHandler.level;
            int weaponUpgradeLevel = scBlock.blockUpgradeHandler.weaponUpgradeHandler.levelUpgrade;

            /*Debug.LogWarning(blockLevel);
            Debug.LogWarning(weaponType);
            Debug.LogWarning(weaponLevel);
            Debug.LogWarning(weaponUpgradeLevel);
            Debug.LogWarning("--------------------");*/

            IngameData ingameData = new IngameData(blockLevel, blockGold, weaponType, weaponLevel, weaponUpgradeLevel);
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
