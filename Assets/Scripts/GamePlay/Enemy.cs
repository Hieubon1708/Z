using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp;

    public float SubstractHp(float hp)
    {
        this.hp -= hp;
        if (this.hp < 0) this.hp = 0;
        return this.hp;
    }
}
