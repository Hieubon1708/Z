using System.Collections;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public static CarController instance;

    public Animator carAni;
    public AreaEffector2D effector;
    public int[] layerLineBumps;
    public Transform[] lineBumps;
    public bool[] isBump;
    public float backgroundSpeed;
    public int amoutCollison = 3;
    public Transform[] spawnY;
    public bool isStop;
    public int multiplier;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        backgroundSpeed = GameController.instance.backgroundSpeed;
    }

    public void AddBookAni()
    {
        carAni.SetTrigger("addBlock");
    }

    public void DeleteMenuBookAni()
    {
        carAni.SetTrigger("removeMenuBlock");
    }
    
    public void DeleteGameBookAni()
    {
        carAni.SetTrigger("removeGameBlock");
    }

    public IEnumerator Bump(int index, GameObject ePushed, string nameOrigin)
    {
        /*ePushed.name = "Bump";
        isBump[index - 1] = true;
        int indexEnemyLine = LayerMask.NameToLayer("Bump_" + index);
        yield return new WaitForSeconds(0.3f);
        isBump[index - 1] = false;
        ePushed.name = nameOrigin;*/

        ePushed.name = "Bump";
        isBump[index] = true;
        Debug.LogWarning("Start");
        ePushed.transform.SetParent(CarController.instance.lineBumps[index]);
        effector.colliderMask |= 1 << layerLineBumps[index];
        yield return new WaitForSeconds(0.3f);
        effector.colliderMask &= ~(1 << layerLineBumps[index]);
        lineBumps[index].transform.localPosition = new Vector2(0, lineBumps[index].transform.localPosition.y);
        isBump[index] = false;
        ePushed.transform.SetParent(EnemyTowerController.instance.scTowers[EnemyTowerController.instance.indexTower].enemyPools[index]);
        ePushed.name = nameOrigin;
        Debug.LogWarning("End");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy")) amoutCollison--;
    }

    private void FixedUpdate()
    {
        GameController.instance.backgroundSpeed = Mathf.Lerp(GameController.instance.backgroundSpeed, multiplier * backgroundSpeed / 3 * Mathf.Clamp(amoutCollison, 0, 3), 0.1f);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) amoutCollison++;
    }
}
