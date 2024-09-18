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

    public IEnumerator Bump(int index, GameObject ePushed, string nameOrigin)
    {
        ePushed.name = "Bump";
        Debug.LogWarning("Start");
        isBump[index - 1] = true;
        yield return new WaitForSeconds(0.3f);
        Debug.LogWarning("End");
        isBump[index - 1] = false;
        ePushed.name = nameOrigin;
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
