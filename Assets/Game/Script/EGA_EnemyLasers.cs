using UnityEngine;

public class EGA_EnemyLasers : MonoBehaviour
{
    //public GameObject FirePoint;
    public Transform FirePoint;
    public Transform ShootDirection;
    public Quaternion _shootDirection;
    public float _bulletSpread;

    private Camera Cam;

    public float MaxLength;
    //public float ShootLength;
    public GameObject[] Prefabs;

    private Ray RayMouse;
    private Vector3 direction;
    private Quaternion rotation;


    [Header("GUI")]
    private float windowDpi;

    private int Prefab;
    private GameObject Instance;
    private EGA_Laser LaserScript;

    //Double-click protection
    private float buttonSaver = 0f;

    //void Start()
    //{
        //ShootLength = 0.2f;
        //LaserEndPoint = new Vector3(0, 0, 0);
    //    if (Screen.dpi < 1) windowDpi = 1;
    //    if (Screen.dpi < 200) windowDpi = 1;
    //    else windowDpi = Screen.dpi / 200f;
    //    Counter(0);
    //}

    public void ShootEnemy(GameObject Prefab, Vector3 Position, Quaternion Rotation)
    {
        //_shootDirection = ShootDirection.transform.rotation;

        //_shootDirection.y = _shootDirection.y + Random.Range ((_bulletSpread * -1), _bulletSpread);
        //_shootDirection.z = _shootDirection.z + Random.Range ((_bulletSpread * -1), _bulletSpread); 

        //Debug.Log(_shootDirection.ToString());
        Destroy(Instance);
        Instance = Instantiate(Prefab, FirePoint.position, Rotation);
        //Instance = Instantiate(Prefabs[Prefab], FirePoint.transform.position, _shootDirection);
        //Instance = Instantiate(Prefabs[Prefab], FirePoint.transform.position, FirePoint.transform.rotation);
        //Instance = Instantiate(Prefabs[Prefab], FirePoint.transform.position, ShootDirection.transform.rotation);
        //Instance = Instantiate(Prefabs[Prefab], FirePoint.transform.position, FirePoint.transform.rotation);
        Instance.transform.parent = transform;
        LaserScript = Instance.GetComponent<EGA_Laser>();
        Invoke("DestroyLaser", 0.2f);

    }

    public void Shoot()
    {
        _shootDirection = ShootDirection.transform.rotation;

        //_shootDirection.y = _shootDirection.y + Random.Range ((_bulletSpread * -1), _bulletSpread);
        //_shootDirection.z = _shootDirection.z + Random.Range ((_bulletSpread * -1), _bulletSpread); 

        //Debug.Log(_shootDirection.ToString());
        Destroy(Instance);
        Instance = Instantiate(Prefabs[Prefab], FirePoint.transform.position, _shootDirection);
        //Instance = Instantiate(Prefabs[Prefab], FirePoint.transform.position, FirePoint.transform.rotation);
        //Instance = Instantiate(Prefabs[Prefab], FirePoint.transform.position, ShootDirection.transform.rotation);
        //Instance = Instantiate(Prefabs[Prefab], FirePoint.transform.position, FirePoint.transform.rotation);
        Instance.transform.parent = transform;
        LaserScript = Instance.GetComponent<EGA_Laser>();
        Invoke("DestroyLaser", 1f);

    }

    public void DestroyLaser()
    {
        LaserScript.DisablePrepare();
        //Debug.Log("Луч уничтожен");
        //        Destroy(Instance,1);

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    // Update is called once per frame

}
