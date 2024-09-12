using System.Collections.Generic;
using UnityEngine;

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
    }

    void LoadData()
    {
        //for
        GameObject block = blockPools[0];
        blockPools.Remove(block);
        block.transform.localPosition = new Vector2(block.transform.localPosition.x, startY + distance * blocks.Count);
        blocks.Add(block);
        block.SetActive(true);
        player.transform.localPosition = new Vector2(player.transform.localPosition.x, startYPlayer + distance * blocks.Count);

        BlockUpgradeHandler blockUpgradeHandler = block.GetComponent<BlockUpgradeHandler>();
        int blockLevel = DataManager.instance.playerData.blockLevel;
        blockUpgradeHandler.LoadData(blockLevel);
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

            scBlock.AddBlockAni();
            CarController.instance.AddBookAni();
            PlayerController.instance.AddBookAni();
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
}
