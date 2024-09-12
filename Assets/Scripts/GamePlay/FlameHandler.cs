using UnityEngine;

public class FlameHandler : MonoBehaviour
{
    public ParticleSystem par;
    public BoxCollider2D boxCollider;

    public void PLayFlamePar()
    {
        par.Play();
        boxCollider.enabled = true;
    }
    
    public void StopFlamePar()
    {
        par.Stop();
        boxCollider.enabled = false;
    }
}
