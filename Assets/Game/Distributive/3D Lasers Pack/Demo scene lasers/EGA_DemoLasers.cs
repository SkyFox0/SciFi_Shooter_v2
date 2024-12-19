using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
//using System;
using UnityEngine;

public class EGA_DemoLasers : MonoBehaviour
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

    void Start ()
    {
        //ShootLength = 0.2f;
        //LaserEndPoint = new Vector3(0, 0, 0);
        if (Screen.dpi < 1) windowDpi = 1;
        if (Screen.dpi < 200) windowDpi = 1;
        else windowDpi = Screen.dpi / 200f;
        Counter(0);
    }

    public void ShootEnemy(GameObject Prefab, Vector3 Position, Quaternion Rotation)
    {
        //_shootDirection = ShootDirection.transform.rotation;

        //_shootDirection.y = _shootDirection.y + Random.Range ((_bulletSpread * -1), _bulletSpread);
        //_shootDirection.z = _shootDirection.z + Random.Range ((_bulletSpread * -1), _bulletSpread); 

        //Debug.Log(_shootDirection.ToString());
        Destroy(Instance);
        Instance = Instantiate(Prefab, Position, Rotation);
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
        Invoke("DestroyLaser", 0.2f);
        
    }

    public void DestroyLaser()
    {
        LaserScript.DisablePrepare();
        //        Destroy(Instance,1);

    }

    void Update()
    {
        //Enable lazer
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        Destroy(Instance);
        //        Instance = Instantiate(Prefabs[Prefab], FirePoint.transform.position, FirePoint.transform.rotation);
        //        Instance.transform.parent = transform;
        //        LaserScript = Instance.GetComponent<EGA_Laser>();
        //    }

        //Disable lazer prefab
        //    if (Input.GetMouseButtonUp(0))
        //    {
        //        LaserScript.DisablePrepare();
        //        Destroy(Instance,1);
        //    }

        //To change lazers
        //if ((Input.GetKey(KeyCode.R) || Input.GetAxis("Horizontal") < 0) && buttonSaver >= 0.4f)// left button
        if (Input.GetKey(KeyCode.T) && buttonSaver >= 0.4f)// left button
        {
            buttonSaver = 0f;
            Counter(-1);
        }
        //if ((Input.GetKey(KeyCode.T) || Input.GetAxis("Horizontal") > 0) && buttonSaver >= 0.4f)// right button
        if (Input.GetKey(KeyCode.Y) && buttonSaver >= 0.4f)// right button
        {
            buttonSaver = 0f;
            Counter(+1);         
        }
        buttonSaver += Time.deltaTime;
        

        //Current fire point
        if (Cam != null)
        {
            RaycastHit hit; //DELATE THIS IF YOU WANT USE LASERS IN 2D
            var mousePos = Input.mousePosition;
            RayMouse = Cam.ScreenPointToRay(mousePos);
            //ADD THIS IF YOU WANNT TO USE LASERS IN 2D: RaycastHit2D hit = Physics2D.Raycast(RayMouse.origin, RayMouse.direction, MaxLength);
            if (Physics.Raycast(RayMouse.origin, RayMouse.direction, out hit, MaxLength)) //CHANGE THIS IF YOU WANT TO USE LASERRS IN 2D: if (hit.collider != null)
            {
                RotateToMouseDirection(gameObject, hit.point);
                //LaserEndPoint = hit.point;
            }
            else
            {
                var pos = RayMouse.GetPoint(MaxLength);
                RotateToMouseDirection(gameObject, pos);
                //LaserEndPoint = pos;
            }
        }
        //else
        //{
            //Debug.Log("No camera");
        //}
    }

    //GUI Text
    void OnGUI()
    {
        GUI.Label(new Rect(10 * windowDpi, 5 * windowDpi, 400 * windowDpi, 20 * windowDpi), "Use the keyboard buttons T/<- and Y/-> to change lazers!");
        GUI.Label(new Rect(10 * windowDpi, 20 * windowDpi, 400 * windowDpi, 20 * windowDpi), "Use left mouse button for shooting!");
    }

    //To change prefabs (count - prefab number)
    void Counter(int count)
    {
        Prefab += count;
        if (Prefab > Prefabs.Length - 1)
        {
            Prefab = 0;
        }
        else if (Prefab < 0)
        {
            Prefab = Prefabs.Length - 1;
        }
    }
  
    //To rotate fire point
    void RotateToMouseDirection (GameObject obj, Vector3 destination)
    {
        direction = destination - obj.transform.position;
        rotation = Quaternion.LookRotation(direction);     
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }
}
