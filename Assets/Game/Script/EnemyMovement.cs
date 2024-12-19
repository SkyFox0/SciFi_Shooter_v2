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
    public SoundController SoundController;
    public Transform Player;
    public Transform Enemy;
    public Vector3 SearchPoint;
    //public Transform SearchPoint;
    public NavMeshAgent NavMeshAgent;
    public EGA_EnemyLasers EGA_EnemyLasers;
    public GameObject Prefab;
    public AudioSource ShootSound;
    public float _moveTime;      // врем€ после выстрела до начала движени€
    public float _searchTime;    // врем€ до смены направлени€
    public float _shootTime;     // врем€ прицеливани€
    public float _enemyChange;   // врем€ до рандомного изменени€ характеристик игрока
    public Transform FirePoint;    

    private GameObject Instance;
    public EGA_Laser LaserScript;


    [Header("Move")]
    public float _enemySpeed = 2f;  // Cкорость врага
    public float _enemyRotationSpeed = 2.0f; // —корость поворота врага
    public bool _isMove; 
    public bool _isRotate;
    public bool _isShoot;
    public bool _isReloading;
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

    [Header("Weapon")]
    public int _ammo = 3;
    public int _maxAmmo = 5;

    void Start()
    {   // ќ игроке узнаем из системы спавна
        //Player = GameObject.FindGameObjectWithTag("Player").transform;
        NavMeshAgent.speed = _enemySpeed;
        _isMove = true;
        _isDead = false;
        _isReloading = false;
        _timer = 0f;
        EGA_EnemyLasers = GetComponent<EGA_EnemyLasers>();
        EnemyAnimator = GetComponentInChildren<Animator>();
        SoundController = GetComponentInChildren<SoundController>();    
        EnemyAnimator.SetFloat("y", 1);
        _enemyChange = 15f;  // врем€ до смены характеристик врага        
        _fireDistans = 30;  //максимальна€ дистанци€ стрельбы
        _ammo = Random.Range(1, _maxAmmo);
        EnemyChange();
    }

    public void EnemyChange()
    {
        _moveTime = Random.Range(0.5f, 1f);
        _searchTime = Random.Range(0.5f, 1f);
        _shootTime = Random.Range(0.5f, 3f);
        _enemySpeed = Random.Range(2f, 4f);
        _enemyRotationSpeed = Random.Range(1f, 3f);
        // –азброс стрельбы должен быть от 0,5 до 1,5
        _bulletSpread = 2f / ((_shootTime + 1f) * (_shootTime + 1f));  //разлЄт пуль
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
        //бросаем поисковый луч
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
                Debug.Log("¬ижу игрока!");

                if (_distance < _fireDistans)
                {
                    Stop();

                    //Rotate();
                    //Direction = Enemy.transform.rotation;
                    //Direction = _direction.rotation;

                    // ==ќдно из двух!==
                    //Direction.eulerAngles = SearchPoint;  

                    _isRotate = true;
                    //Invoke("Rotate", _shootSpeed/2f);

                    _isShoot = true;

                    if (_ammo > 0)
                    {
                        Invoke("Shoot", _shootTime);
                    }
                    else
                    {   
                        Reloading();
                    }                    
                }
            }
            else
            {
                _timer = 0f;
            }
        }
    }

    public void Reloading()
    {
        _isMove = false;
        _isReloading = true;
        SoundController.Reload();

        EnemyAnimator.SetBool("isMove", false);        
        EnemyAnimator.SetTrigger("Reload");
        EnemyAnimator.SetBool("isReloading", true);
        _ammo = _maxAmmo;
        Invoke("Move", _moveTime);
        
        
    }

        public void Stop()
    {
        NavMeshAgent.speed = 0f;
        EnemyAnimator.SetFloat("y", 0);
        _isMove = false;
        //Debug.Log("ќстановилс€ дл€ стрельбы");
    }

    public void Rotate()
    { 
        //ѕоворот в сторону игрока
        // ¬ычисл€ем направление на игрока
        _rotateDirection = Player.transform.position - Enemy.transform.position;
        _rotateDirection.y = 0; // ≈сли хотите игнорировать вертикальную ось

        // ѕровер€ем, есть ли ненулевое направление
        if (_rotateDirection.magnitude > 0.01f)
        {
            //Debug.Log("поворачиваю в сторону игрока на вектор  " + _rotateDirection.ToString());
            // ¬ычисл€ем поворот к цели
            targetRotation = Quaternion.LookRotation(_rotateDirection);

            // ѕлавно поворачиваем врага в сторону игрока
            Enemy.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _enemyRotationSpeed);
              
        }
    }

    public void Shoot()
    {        
        _isRotate = false;
        //_shoot = true;
        Debug.Log("¬ыстрел!");
        _ammo -= 1;
        _targetDirection = Player.transform.position - Enemy.transform.position;

        // задаЄм отклонение вызова
        // и кидаем луч повторно
        _spreadVector.y = Random.Range((_bulletSpread * -1), _bulletSpread) / 3f;  // отклонение по вертикали меньше в 3 раза
        _spreadVector.z = Random.Range((_bulletSpread * -1), _bulletSpread);  // отклонение по горизонтали

        _targetDirection.y += _spreadVector.y;  // отклонение по вертикали
        _targetDirection.z += _spreadVector.z;  // отклонение по горизонтали               

        // проверка не сдох ли враг :)
        if (_isDead)
        {            
            // стрельба по направлению ствола оружи€
            _targetDirection = FirePoint.forward;
        }

        // преобразование Vector 3 в Quaternion!!!
        _shootDerection = Quaternion.LookRotation(_targetDirection);
        //Direction = _shootDerection;   //Quaternion.LookRotation(FirePoint.forward);        
        EnemyAnimator.SetTrigger("Shoot");
        EGA_EnemyLasers.ShootEnemy(Prefab, SearchPoint, _shootDerection);
        ShootSound.Play();
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
                // попал по игроку
                Debug.Log("ѕопал по игроку!");
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
                // промазал по игроку
                Debug.Log("ѕромазал по игроку!");
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
        _isReloading = false;
        _isMove = true;

        EnemyAnimator.SetBool("isReloading", false);        
        EnemyAnimator.SetBool("isMove", true);
        
        NavMeshAgent.speed = _enemySpeed;
        EnemyAnimator.SetFloat("y", 1);
        _timer = 0f;
        _isShoot = false;

    }
}
