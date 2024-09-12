using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public List<GameObject> listEnemies;

    public Transform poolDamages;
    public Transform poolBullets;
    public Transform poolEnemies;

    public float timeSawDamage;
    public float timeFlameDamage;

    public float backgroundSpeed;

    private void Awake()
    {
        instance = this;
        if (!PlayerPrefs.HasKey("Level")) PlayerPrefs.SetInt("Level", 1);
    }

    public enum WEAPON
    {
        NONE, SAW, FLAME, MACHINE_GUN
    }

    public void Start()
    {
        //LoadLevel();
    }

    void LoadLevel()
    {
        Instantiate(Resources.Load(Path.Combine("Levels", PlayerPrefs.GetInt("Level").ToString())), transform);
    }
}
