using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public Player playerInfo;
    public HealthHandler healthHandler;

    public void OnEnable()
    {
        healthHandler.SetTotalHp(playerInfo.hp);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        float hp = playerInfo.SubstractHp(1000);
        healthHandler.SubstractHp(hp);
        if (hp == 0)
        {

        }
    }
}
