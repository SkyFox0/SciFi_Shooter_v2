using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
public class WeaponController : MonoBehaviour 
{
	public Transform Firepoint;
    public Camera _camera;
    public Animator CameraAnimator;
    public Animator WeaponAnimator;
    public float startTime;
    public float fireRate = 0.2f;

    [Header("Damage")]
    [SerializeField, Min(0f)] private float _bulletForse = 100f;
    [SerializeField, Min(0f)] private int _damage = 10;

    [Header("Particle System")]
    [SerializeField] private ParticleSystem _muzzleEffect;
    [SerializeField] private Light _muzzleLight;
    [SerializeField] private float _lightForse = 5f;

    [Header("Выбрас гильзы")]
    [SerializeField] private GameObject _sleeve;
    [SerializeField] private Transform _sleevePosition;
    public Sleeve_Manager _sleeve_Manager;
    public SleeveEjection _sleeveEjection;
    public GameObject SLEEVE_POOL_MANAGER;

    //[SerializeField] private ParticleSystem _hitEffectPrefab;
    //[SerializeField, Min(0f)] private float _hitEffectDestroyDelay = 2f;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;



    // Update is called once per frame
    void Start()
    {
        startTime = Time.time;
        _camera.fieldOfView = 60f;
    }

    void Update () 
	{
        CameraSightOn();
        CameraSightOff();
    //	if(Input.GetMouseButtonDown(0))
    //	{
    //       if (Time.time - startTime > fireRate)
    //        {

        //            Shoot();
        //            startTime = Time.time;
        //        }            
        //    }
    }

    public void OnShoot(InputValue value)
    {
        if (Time.time - startTime > fireRate)
        {
            Shoot();
            //SleeveEjectionOld();
            SleeveEjection();
            startTime = Time.time;
        }
    }

    public void SleeveEjectionOld()
    {
        GameObject Gm = Instantiate(_sleeve, _sleevePosition.position, _sleevePosition.rotation);
        _sleeveEjection = Gm.GetComponent<SleeveEjection>();
        _sleeveEjection.Eject();
    }

    public void SleeveEjection()
    {
        int _currentSleeve = _sleeve_Manager._currentSleeve;
        _sleeveEjection = _sleeve_Manager.m_Sleeve[_currentSleeve].GetComponent<SleeveEjection>();
        _sleeve_Manager.SleeveEject();
        GameObject Gm = _sleeve_Manager.m_Sleeve[_currentSleeve];  //Instantiate(_sleeve, _sleevePosition.position, _sleevePosition.rotation);
        Debug.Log(Gm.ToString());
        Gm.gameObject.SetActive(true);
        //_sleeveEjection = Gm.GetComponent<SleeveEjection>();
        Gm.transform.position = _sleevePosition.position;

        _sleeveEjection.Eject();
    }

    public void OnMove(InputValue value)
    {
        Debug.Log("Движение");
        if (!CameraAnimator.GetBool("Walk"))
        {
            CameraAnimator.SetBool("Walk", true);
        }
    }

    public void OnStop(InputValue value)
    {
        Debug.Log("Stop");
        CameraAnimator.SetBool("Walk", false);

    }

    public void Shoot()
    {
        DoTrace(Camera.main.transform);
        //DoTrace(Firepoint);
        if (!WeaponAnimator.GetBool("Shoot"))
        {
            PerformAnimation();
            PerformEffects();
        }
        
    }

    public void OnSight(InputValue value)
    {
        WeaponAnimator.SetBool("SightOn", true);
        //_camera.fieldOfView = 40f;
        //Debug.Log("Включить прицеливание");
    }

    public void CameraSightOn()
    {
        if (WeaponAnimator.GetBool("SightOn"))
        {
            if (_camera.fieldOfView > 40)
            {
                _camera.fieldOfView = _camera.fieldOfView - 0.2f;
            }
        }
    }

    public void CameraSightOff()
    {
        if (!WeaponAnimator.GetBool("SightOn"))
        {
            if (_camera.fieldOfView < 60)
            {
                _camera.fieldOfView = _camera.fieldOfView + 0.5f;
            }
        }

    }

    public void OnSightOff(InputValue value)
    {
        WeaponAnimator.SetBool("SightOn", false);
        //_camera.fieldOfView = 60f;
        //Debug.Log("Выключить прицеливание");
    }

    public void PerformAnimation()
        {
            WeaponAnimator.SetBool("Shoot", true);
            CameraAnimator.SetBool("Shoot", true);
            Invoke("ShootAnimationOff", fireRate);
        }

    public void PerformEffects()
    {
        if (_muzzleEffect != null)
        {
            _muzzleEffect.Play();
            _muzzleLightOn();            
            Invoke("_muzzleLightOff", 0.1f);
        }

        if (_audioSource != null && _audioClip != null)
        {
            _audioSource.PlayOneShot(_audioClip);
        }
    }

    public void _muzzleLightOn()
    {
        _muzzleLight.intensity = _lightForse;
    }
    public void _muzzleLightOff()
    {
        _muzzleLight.intensity = 0f;
    }

    public void ShootAnimationOff()
    {
        WeaponAnimator.SetBool("Shoot", false);
        CameraAnimator.SetBool("Shoot", false);
    }

    void DoTrace(Transform fireFrom)
	{
		Vector3 direction = fireFrom.forward;

		Ray ray = new Ray(fireFrom.position, direction);

		RaycastHit hit;

		if (!Physics.Raycast (ray, out hit,1000f,~(1<<LayerMask.NameToLayer("Char"))))
			return;

        if (WPN_Decal_Manager.Instance != null )
        {
            WPN_Decal_Manager.Instance.SpawnBulletHitEffects(hit.point, hit.normal, hit.collider.material, hit.collider.gameObject);
        }
        if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForceAtPosition(direction * _bulletForse, hit.point, ForceMode.Impulse);
                                   //Vector3 force, ForceMode.Impulse)
        }

        if (hit.transform.GetComponent<PlayerHealthComponentOLD>())
        {
            Debug.Log("Обьект: " + hit.collider.gameObject.name.ToString()); 
            Debug.Log("обнаружен компонент здоровье!");
            hit.transform.GetComponent<PlayerHealthComponentOLD>().TakeDamage(_damage);
        }

    }
}
