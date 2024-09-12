using UnityEngine;

public class EnemyTowerController : MonoBehaviour
{
    public static EnemyTowerController instance;

    public GameObject[] towers;
    public EnemyController[] scTowers;
    public float distanceTower;
    public int indexTower = -1;

    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Generate();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            NextTower();
        }
    }

    void Generate()
    {
        scTowers = new EnemyController[towers.Length];
        for (int i = 0; i < towers.Length; i++)
        {
            scTowers[i] = Instantiate(towers[i], new Vector2(CarController.instance.transform.position.x + (distanceTower * (i + 1)), CarController.instance.transform.position.y - 0.15f), Quaternion.identity, transform).GetComponent<EnemyController>();
        }
    }

    public void NextTower()
    {
        indexTower++;
        if(indexTower == towers.Length)
        {
            Debug.Log("Win");
            return;
        }
        CarController.instance.multiplier = 1;
        scTowers[indexTower].EnableEs();
    }
}
