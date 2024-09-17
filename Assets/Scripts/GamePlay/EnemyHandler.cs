using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public Enemy enemyInfo;
    public GameObject healthBar;
    public HealthHandler healthHandler;
    public Damage damage;
    public Rigidbody2D rb;
    public GameObject col;
    bool isTriggerSaw;
    bool isTriggerFlame;
    public float forceJump;
    public float multiplier;
    public float speed;
    public float timeStunned;
    public float timeJump;
    public Animator animator;
    public bool isCollisionWithCar;
    public bool isCollisionWithGround;
    public bool isCollisionWithBump;
    public bool isJump;
    public bool isWalk;
    public bool isStunned;
    public int amoutCollision;
    public int lineIndex;
    public GameObject content;
    public GameObject enemyCollisionToJump;
    public List<GameObject> listCollisions = new List<GameObject>();
    public List<Vector2> listNormals = new List<Vector2>();
    public ContactPoint2D[] listContacts = new ContactPoint2D[10];
    Coroutine stunnedDelay;
    Coroutine jump;
    Coroutine bump;
    public GameObject frontalCollision;

    public void Start()
    {
        lineIndex = EUtils.GetIndexLine(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && a)
        {
            StartCoroutine(CarController.instance.Bump(lineIndex, rb, gameObject));


        }
    }

    protected virtual void OnEnable()
    {
        healthHandler.SetTotalHp(enemyInfo.hp);
    }

    protected virtual IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        int subtractHp;
        if (collision.CompareTag("Bullet"))
        {
            subtractHp = 70;
            collision.gameObject.SetActive(false);
            SubtractHp(subtractHp);
        }
        if (collision.CompareTag("MachineGun"))
        {
            subtractHp = GameController.instance.listDamages[collision.attachedRigidbody.gameObject];
            collision.gameObject.SetActive(false);
            SubtractHp(subtractHp);
        }
        if (collision.CompareTag("Saw"))
        {
            subtractHp = GameController.instance.listDamages[collision.attachedRigidbody.gameObject];
            isTriggerSaw = true;
            while (isTriggerSaw && enemyInfo.hp > 0)
            {
                SubtractHp(subtractHp);
                yield return new WaitForSeconds(GameController.instance.timeSawDamage);
            }
        }
        if (collision.CompareTag("Flame"))
        {
            subtractHp = GameController.instance.listDamages[collision.attachedRigidbody.gameObject];
            isTriggerFlame = true;
            while (isTriggerFlame && enemyInfo.hp > 0)
            {
                SubtractHp(subtractHp);
                yield return new WaitForSeconds(GameController.instance.timeFlameDamage);
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Saw")) isTriggerSaw = false;
        if (collision.CompareTag("Flame")) isTriggerFlame = false;
        if (collision.CompareTag("Car")) animator.SetBool("attack", false);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Car")) isCollisionWithCar = true;
        if (collision.gameObject.CompareTag("Ground")) isCollisionWithGround = true;
        if (collision.gameObject.CompareTag("Bump")) isCollisionWithBump = true;

        if (collision.gameObject.CompareTag("Enemy") && collision.contacts[0].normal.x >= 0.99f) frontalCollision = collision.gameObject;

        animator.SetFloat("velocityY", 0);
        if (collision.contacts[0].normal.y >= 0.85f)
        {
            isJump = false;
            MassChange(1);
        }
    }

    public GameObject d;

    IEnumerator SetFalseIsStunned(float time)
    {
        yield return new WaitForSeconds(time);
        isStunned = false;
    }

    public bool a;

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Bump") && isCollisionWithGround) name = "Bump";

        if (collision.contacts[0].normal.y < -0.05f && collision.gameObject != enemyCollisionToJump && collision.gameObject.CompareTag("Enemy"))
        {
            animator.SetFloat("velocityY", 0);
            isStunned = true;
            timeStunned = Random.Range(0.75f, 1.15f);

            if (jump != null)
            {
                StopCoroutine(jump);
                isJump = false;
            }

            if (stunnedDelay != null) StopCoroutine(stunnedDelay);
            stunnedDelay = StartCoroutine(SetFalseIsStunned(timeStunned));

            rb.velocity = Vector2.zero;
            MassChange(1);
        }

        amoutCollision = 0;
        int length = rb.GetContacts(listContacts);
        listCollisions.Clear();
        listNormals.Clear();

        for (int i = 0; i < length; i++)
        {
            if ((listContacts[i].normal.x >= 0.99f || listContacts[i].normal.x <= -0.99f || listContacts[i].normal.y < 0)
                && !listCollisions.Contains(listContacts[i].rigidbody.gameObject)
                && listContacts[i].collider.gameObject.CompareTag("Enemy"))
            {
                listCollisions.Add(listContacts[i].rigidbody.gameObject);
                listNormals.Add(listContacts[i].normal);
                amoutCollision++;
            }
        }

        /*if (!isJump
            && amoutCollision == 1
            && listNormals[0].x > 0.85f
            && Mathf.Abs(listCollisions[0].transform.position.y - transform.position.y) <= 0.01f
            && !isStunned
            && !isCollisionWithCar
            && !isCollisionWithBump)
        {
            animator.SetFloat("velocityY", 3);
            enemyCollisionToJump = collision.gameObject;
            jump = StartCoroutine(Jump());
        }*/

        if (isCollisionWithCar
            && isCollisionWithGround
            && collision.contacts[0].normal.y <= -0.85f)
        {
            if (!CarController.instance.isBump[lineIndex - 1])
            {
                StartCoroutine(CarController.instance.Bump(lineIndex, rb, collision.gameObject));
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Car")) isCollisionWithCar = false;
        if (collision.gameObject.CompareTag("Ground")) isCollisionWithGround = false;
        if (collision.gameObject.CompareTag("Bump")) isCollisionWithBump = false;
        if (collision.gameObject.name.Contains("Bump") || collision.gameObject.CompareTag("Ground")) name = "E";
    }

    protected virtual void FixedUpdate()
    {
        if (frontalCollision != null)
        {
            if (frontalCollision.name == "Bump" && isCollisionWithGround) name = "Bump";
            else name = "E";
        }

        if (name.Contains("Bump"))
        {
            rb.velocity = new Vector2(2, rb.velocity.y);
        }
        else
        {
            if (isWalk)
            {
                rb.velocity = new Vector2(speed * multiplier, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }

    protected IEnumerator Jump()
    {
        if (!a) yield break;
        isJump = true;
        MassChange(0.1f);
        rb.velocity = new Vector2(rb.velocity.x, forceJump);
        yield return new WaitForSeconds(timeJump);
        animator.SetFloat("velocityY", 0);
        rb.velocity = new Vector2(rb.velocity.x, 0);
    }

    public void MassChange(float mass)
    {
        rb.mass = mass;
    }

    void SubtractHp(float subtractHp)
    {
        if (!healthBar.activeSelf) healthBar.SetActive(true);
        float hp = enemyInfo.SubtractHp(subtractHp);
        healthHandler.SubtractHp(hp);
        damage.ShowDamage(subtractHp.ToString());

        if (hp == 0) DeathHandle();
    }

    protected virtual void DeathHandle()
    {
        //content.SetActive(false);
        healthHandler.SetDefaultInfo(enemyInfo);
        enemyInfo.gameObject.SetActive(false);
        healthBar.SetActive(false);
        EnemyTowerController.instance.scTowers[EnemyTowerController.instance.indexTower].ERevival(enemyInfo.gameObject);
    }

    public void OnDestroy()
    {
        isCollisionWithBump = false;
        isCollisionWithCar = false;
        isCollisionWithGround = false;
        isStunned = false;
        isTriggerFlame = false;
        isTriggerSaw = false;
        isWalk = false;
    }
}
