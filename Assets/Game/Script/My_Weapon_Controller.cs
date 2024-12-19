using UnityEngine;
using UnityEngine.InputSystem;

namespace StarterAssets
{
    public class My_Weapon_Controller : MonoBehaviour
    {
        public Animator Animator;
        private float LastShoot;
        public Camera Camera;

        [Header("Shoot")]
        public ShootComponent ShootComponent;
        public EGA_DemoLasers EGA_DemoLasers;
        public float _shootSpeed;

        [Header("Sounds")]
        public AudioSource PlaySound;
        public AudioClip Shoot;
        public AudioClip Reload;
        public AudioClip Empty;
        public AudioClip Hit;
        


        [Header("Ammo")]
        public int _maxAmmo = 10;
        public int _currentAmmo = 3;
        public TextMesh _ammoText;



        private void Start()
        {
            _ammoText.text = _currentAmmo.ToString();
            LastShoot = Time.time;
            //Animator = GetComponentInChildren<Animator>();
            _shootSpeed = 0.5f;
            Animator.SetBool("isReloading", false);
            if (_currentAmmo < _maxAmmo)
            {
                Animator.SetBool("isNeedToReload", true);
            }
        }


        public void OnShoot(InputValue value)
        {
            if (!Animator.GetBool("isDead"))
            {
                if ((Time.time - LastShoot > _shootSpeed) && (_currentAmmo > 0) && (!Animator.GetBool("isReloading")))  //_shootSpeed = 0.5f;
                {
                    Animator.SetTrigger("Shoot");
                    ShootComponent.Shoot();
                    EGA_DemoLasers.Shoot();

                    PlaySound.clip = Shoot;
                    PlaySound.Play();
                    
                    LastShoot = Time.time;
                    _currentAmmo -= 1;
                    _ammoText.text = _currentAmmo.ToString();
                    Animator.SetBool("isNeedToReload", true);
                    Debug.Log("Shoot!");
                }
                else
                { 
                    if (!Animator.GetBool("isReloading"))
                    {
                        PlaySound.clip = Empty;
                        PlaySound.Play();
                    }
                    
                }
            }
                
        }

        public void OnReload(InputValue value)
        {
            if (Animator.GetBool("isNeedToReload") && !Animator.GetBool("isReloading") && !Animator.GetBool("isMove") && Animator.GetBool("isGround") && (!Animator.GetBool("isDead")))
            {
                Debug.Log("Перезарядка");
                Animator.SetTrigger("Reload");
                PlaySound.clip = Reload;
                PlaySound.Play();
                //Animator.SetBool("isReloading", true);
            }
        }

        public void OnSelectWeapon(InputValue value)
        {
            Debug.Log("Scroll!");
        }



        public void EndReload()
        {
            _currentAmmo = _maxAmmo;
            _ammoText.text = _currentAmmo.ToString();
        }

        public void OnSight(InputValue value)
        {
            //if (!Animator.GetBool("isReloading") && Animator.GetBool("isGround") && (!Animator.GetBool("isDead"))) 
            //      Animator.SetBool("isSight", true);
            //Camera.fieldOfView = 40;

        }
    }
}
