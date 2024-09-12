using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    bool isMouseDown;
    public GameObject traectory;
    public Transform gunPivot;
    public Transform gunPivotForAni;
    public Animator playerAni;
    public ParticleSystem bulletPar;

    private void Awake()
    {
        instance = this;
    }

    public void MouseDown()
    {
        //if (!GameManager.instance.isStart) return;
        /*isMouseDown = true;
        traectory.SetActive(true);*/
    }

    public void Update()
    {
        //if (!GameManager.instance.isStart) return;
        /*if (isMouseDown)
        {
            Vector2 mousePos = GameManager.instance.cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = mousePos - (Vector2)gunPivot.position;
            float angle = Mathf.Clamp(Mathf.DeltaAngle(0f, GetAngle(dir) - gunPivotForAni.localEulerAngles.z), -90f, 90f);
            gunPivot.localRotation = Quaternion.Euler(0, 0, angle);
        }*/
    }

    public void MouseUp()
    {
        //if (!GameManager.instance.isStart) return;
        /*if (!isMouseDown) return;
        isMouseDown = false;
        traectory.SetActive(false);*/
    }

    float GetAngle(Vector2 dir)
    {
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    public void AddBookAni()
    {
        playerAni.Play("OnAddBlock");
    }

    public void DeleteBookAni()
    {
        playerAni.Play("OnRemoveGameBlock");
    }

    public void ShotAni()
    {
        playerAni.SetBool("attack", true);
    }
}
