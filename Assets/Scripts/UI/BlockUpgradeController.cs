using UnityEngine;

public class BlockUpgradeController : MonoBehaviour
{
    public GameObject recyle;
    public float raycastDistance;
    public LayerMask layerMask;
    public GameObject frame1;
    public GameObject frame2;
    GameObject blockSelected;
    bool isDrag;
    bool isHold;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrag = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
            isHold = false;
            if (blockSelected != null)
            {
                Block scBlock = BlockController.instance.GetScBlock(blockSelected);
                if (Vector2.Distance(blockSelected.transform.position, recyle.transform.position) <= 1f)
                {
                    BlockController.instance.DeleteBlock(blockSelected);
                    scBlock.blockUpgradeHandler.ResetData();
                }
                blockSelected.transform.position = frame1.transform.position;
                scBlock.blockUpgradeHandler.DeSelected();
                SetActiveFrame(false);
                blockSelected = null;
            }
        }
        if (isDrag)
        {
            if(blockSelected != null)
            {
                Vector2 pos = GameManager.instance.cam.ScreenToWorldPoint(Input.mousePosition);
                blockSelected.transform.position = pos;
                frame2.transform.position = pos;
                BlockController.instance.SetPositionNearest(blockSelected, frame1);
            }
            if (!isHold)
            {
                Vector2 raycastPosition = GameManager.instance.cam.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(raycastPosition, Vector2.zero, raycastDistance, layerMask);
                if (hit.collider != null)
                {
                    blockSelected = hit.rigidbody.gameObject;
                    frame1.transform.position = blockSelected.transform.position;
                    frame2.transform.position = blockSelected.transform.position;
                    BlockController.instance.GetScBlock(blockSelected).blockUpgradeHandler.Selected();
                    SetActiveFrame(true);
                    isHold = true;
                }
            }
            else
            {

            }
        }
    }

    void SetActiveFrame(bool isActive)
    {
        frame1.SetActive(isActive);
        frame2.SetActive(isActive);
    }

    public void BuyBlock()
    {
        BlockController.instance.AddBlock();
    }
}
