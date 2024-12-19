//using DestroyIt;
using StarterAssets;
//using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;
//using static UnityEditor.FilePathAttribute;
//using UnityEngine.UIElements;
//using System.Diagnostics;

public class EnemyMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator EnemyAnimator;
    public Transform Player;
    public Transform Enemy;
    public Vector3 SearchPoint;
    //public Transform SearchPoint;
    public NavMeshAgent NavMeshAgent;
    public EGA_EnemyLasers EGA_EnemyLasers;
    public GameObject Prefab;
    public AudioSource ShootSound;
    public float _moveTime;      // ����� ����� �������� �� ������ ��������
    public float _searchTime;    // ����� �� ����� �����������
    public float _shootTime;     // ����� ������������
    public float _enemyChange;   // ����� �� ���������� ��������� ������������� ������
    public Transform FirePoint;    

    private GameObject Instance;
    public EGA_Laser LaserScript;


    [Header("Move")]
    public float _enemySpeed = 2f;  // C������� �����
    public float _enemyRotationSpeed = 2.0f; // �������� �������� �����
    public bool _isMove; 
    public bool _isRotate;
    public bool _isShoot;
    public bool _isDead;
    public Vector3 _rotateDirection;
    public Quaternion targetRotation;

    [Header("Search")]
    public float _timer;
    //public Transform SearchPoint;
    //public Transform SearchDirection;
    public Vector3 _direction;
    public Quaternion Direction;

    [Header("Fire")]
    public float _fireDistans;
    public float _distance;
    public AnimationCurve _maxBulletSpread;
    public float _bulletSpread;
    public Vector3 _spreadVector;

    public Vector3 _targetDirection;
    public Quaternion _shootDerection;

    void Start()
    {   // � ������ ������ �� ������� ������
        //Player = GameObject.FindGameObjectWithTag("Player").transform;
        NavMeshAgent.speed = _enemySpeed;
        _isMove = true;
        _isDead = false;
        _timer = 0f;
        EGA_EnemyLasers = GetComponent<EGA_EnemyLasers>();
        EnemyAnimator = GetComponentInChildren<Animator>();
        EnemyAnimator.SetFloat("y", 1);
        _enemyChange = 15f;  // ����� �� ����� ������������� �����        
        _fireDistans = 30;  //������������ ��������� ��������
        EnemyChange();
    }

    public void EnemyChange()
    {
        _moveTime = Random.Range(0.5f, 1f);
        _searchTime = Random.Range(0.5f, 1f);
        _shootTime = Random.Range(0.5f, 3f);
        _enemySpeed = Random.Range(2f, 4f);
        _enemyRotationSpeed = Random.Range(1f, 3f);
        // ������� �������� ������ ���� �� 0,5 �� 1,5
        _bulletSpread = 2f / ((_shootTime + 1f) * (_shootTime + 1f));  //����� ����
        _bulletSpread = _maxBulletSpread.Evaluate(_shootTime / 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDead)
        {
            _timer = _timer + Time.deltaTime;
            if ((_timer > _searchTime) && _isMove)
            {
                NavMeshAgent.SetDestination(Player.position);
                Search();
            }
            else
            {

            }
            if (_isShoot)
            {
                SearchPoint = Enemy.transform.position + new Vector3(0f, 1.6f, 0f);
                //Debug.DrawRay(SearchPoint, _direction * 10, Color.red);
                Debug.DrawRay(SearchPoint, _targetDirection * _fireDistans, Color.red);
            }

            if (_isRotate)
            {
                Rotate();
                //Enemy.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }


            SearchPoint = Enemy.transform.position + new Vector3(0f, 1.6f, 0f);
            Debug.DrawRay(SearchPoint, Enemy.transform.forward * 5, Color.green);
            Debug.DrawRay(SearchPoint, _direction * 5, Color.yellow);
            //NavMeshAgent.SetDestination(Player.position);
            //Debug.DrawRay(Enemy.transform.position, Enemy.transform.forward * 30, Color.yellow);
            //Debug.DrawRay(Enemy.transform.position, direction * 30, Color.green);
        }
    }

    public void Search()
    {
        //������� ��������� ���
        _direction = Player.transform.position - Enemy.transform.position;
        _distance = _direction.magnitude;
        //Debug.DrawRay(Enemy.transform.position, Enemy.transform.forward * 30, Color.yellow);
        //Debug.DrawRay(Enemy.transform.position, direction * 30, Color.green);
        if (Physics.Raycast(SearchPoint, _direction, out var hitInfo))
        {
            //Debug.Log("Hit! Object = " + hitInfo.collider.name);
            //Debug.Log(_direction.ToString());
            if (hitInfo.collider.gameObject.name == "Player")  //if (hitInfo.collider.name == "Player")
            {
                Debug.Log("���� ������!");

                if (_distance < _fireDistans)
                {
                    Stop();

                    //Rotate();
                    //Direction = Enemy.transform.rotation;
                    //Direction = _direction.rotation;

                    // ==���� �� ����!==
                    //Direction.eulerAngles = SearchPoint;  

                    _isRotate = true;
                    //Invoke("Rotate", _shootSpeed/2f);

                    _isShoot = true;

                    Invoke("Shoot", _shootTime);
                }
            }
            else
            {
                _timer = 0f;
            }
        }
    }

    public void Stop()
    {
        NavMeshAgent.speed = 0f;
        EnemyAnimator.SetFloat("y", 0);
        _isMove = false;
        //Debug.Log("����������� ��� ��������");
    }

    public void Rotate()
    { 
        //������� � ������� ������
        // ��������� ����������� �� ������
        _rotateDirection = Player.transform.position - Enemy.transform.position;
        _rotateDirection.y = 0; // ���� ������ ������������ ������������ ���

        // ���������, ���� �� ��������� �����������
        if (_rotateDirection.magnitude > 0.01f)
        {
            //Debug.Log("����������� � ������� ������ �� ������  " + _rotateDirection.ToString());
            // ��������� ������� � ����
            targetRotation = Quaternion.LookRotation(_rotateDirection);

            // ������ ������������ ����� � ������� ������
            Enemy.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _enemyRotationSpeed);
              
        }
    }

    public void Shoot()
    {
        
        _isRotate = false;
        //_shoot = true;
        Debug.Log("�������!");
        
        
        _targetDirection = Player.transform.position - Enemy.transform.position;
        // ����� ���������� ������
        // � ������ ��� ��������
        _spreadVector.y = Random.Range((_bulletSpread * -1), _bulletSpread) / 3f;  // ���������� �� ��������� ������ � 3 ����
        _spreadVector.z = Random.Range((_bulletSpread * -1), _bulletSpread);  // ���������� �� �����������

        _targetDirection.y += _spreadVector.y;  // ���������� �� ���������
        _targetDirection.z += _spreadVector.z;  // ���������� �� �����������

        //Direction.y = Direction.y + _spreadVector.y;
        //Direction.z = Direction.z + _spreadVector.z;

        //Direction = FirePoint.transform.rotation;

        _shootDerection = Quaternion.LookRotation(_targetDirection);

        Direction = _shootDerection;//Quaternion.LookRotation(FirePoint.forward);
        ShootSound.Play();
        EnemyAnimator.SetTrigger("Shoot");
        EGA_EnemyLasers.ShootEnemy(Prefab, SearchPoint, Direction);
        //EGA_EnemyLasers.ShootEnemy(Prefab, SearchPoint, Direction);

        //Destroy(Instance);
        //Instance = Instantiate(Prefab, SearchPoint, Direction);
        //Instance.transform.parent = transform;


        

        //if (Physics.Raycast(SearchPoint, _direction, out var hitInfo))
        if (Physics.Raycast(SearchPoint, _targetDirection, out var hitInfo))
        {
            //Debug.DrawRay(SearchPoint, _direction * 15, Color.red);
            if (hitInfo.collider.gameObject.name == "Player")
            {
                // ����� �� ������
                Debug.Log("����� �� ������!");
                if (hitInfo.collider.TryGetComponent(out PlayerHealthComponentNew healthComponent))
                {
                    try
                    {
                        //healthComponent.HitSound.Play();
                        healthComponent.TakeDamage(10);
                    }
                    catch { }
                }

            }
            else 
            {
                // �������� �� ������
                Debug.Log("�������� �� ������!");
            }
        }
        Invoke("Move", _moveTime);
    }

    //public void DestroyLaser()
   // {
    //    LaserScript.DisablePrepare();        
   // }

    public void Move()
    {        
        _isMove = true;
        NavMeshAgent.speed = _enemySpeed;
        EnemyAnimator.SetFloat("y", 1);
        _timer = 0f;
        _isShoot = false;
    }
}