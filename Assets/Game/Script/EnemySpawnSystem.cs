using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{
    public GameObject Enemy;
    private GameObject Instance;
    private EnemyMovement EnemyMovement;
    private GameObject Player;
    public GameObject[] SpawnPoints;
    //public GameObject SpawnPoint_1;
    //public GameObject SpawnPoint_2;
    //public GameObject SpawnPoint_3;
    private GameObject SpawnPoint;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        AvtoSpavn();
        
        //
    }

    // Update is called once per frame
    public void AvtoSpavn()
    {
        Destroy(Instance);
        Player = GameObject.Find("Player");

        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            SpawnPoint = SpawnPoints[i];

            Instance = Instantiate(Enemy, SpawnPoint.transform.position, transform.rotation);
            // помещаем клон врага с систему спавна
            Instance.transform.parent = transform;


            EnemyMovement = Instance.GetComponent<EnemyMovement>();
            EnemyMovement.Player = Player.transform;
        }
    }

    public void Spawn()
    {
        //Destroy(Instance);
        SpawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        Instance = Instantiate(Enemy, SpawnPoint.transform.position, transform.rotation);
        // помещаем клон врага с систему спавна
        Instance.transform.parent = transform;

        EnemyMovement = Instance.GetComponent<EnemyMovement>();
        EnemyMovement.Player = Player.transform;
    }
}
