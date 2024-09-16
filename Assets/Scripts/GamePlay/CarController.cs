using System.Collections;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public static CarController instance;

    public Animator carAni;
    public AreaEffector2D effector;
    public Rigidbody2D[] bumpCols;
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

    public IEnumerator Bump(int index, Rigidbody2D rb)
    {
        isBump[index - 1] = true;
        int indexEnemyLine = LayerMask.NameToLayer("Bump_" + index);
        bumpCols[index - 1].gameObject.SetActive(true);
        rb.excludeLayers &= ~(1 << indexEnemyLine);
        yield return new WaitForSeconds(0.55f);
        rb.excludeLayers |= 1 << indexEnemyLine;
        bumpCols[index - 1].gameObject.SetActive(false);
        bumpCols[index - 1].transform.localPosition = Vector2.zero;
        isBump[index - 1] = false;
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
