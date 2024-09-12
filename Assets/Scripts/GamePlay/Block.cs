using UnityEngine;

public class Block : MonoBehaviour
{
    public int level;
    public float hp;
    public Animation ani;
    public BlockUpgradeHandler blockUpgradeHandler;

    AnimationClip[] animationClips = new AnimationClip[3];
    public ParticleSystem addBlock;

    public void Awake()
    {
        animationClips[0] = ani.GetClip("addBlock");
        animationClips[1] = ani.GetClip("destroyBlock");
        animationClips[2] = ani.GetClip("upgradeBlock");
    }

    public float SubstractHp(float hp)
    {
        this.hp -= hp;
        if(this.hp < 0) this.hp = 0;
        return this.hp;
    }

    public void AddBlockAni()
    {
        ani.clip = animationClips[0];
        ani.Play("addBlock");
    }

    public void DeleteBlockAni()
    {
        ani.clip = animationClips[1];
        ani.Play("destroyBlock");
    }

    public void UpgradeBlockAni()
    {
        ani.clip = animationClips[2];
        ani.Play("upgradeBlock");
    }

    public void AddBlockParEvent()
    {
        addBlock.Play();
    }
}
