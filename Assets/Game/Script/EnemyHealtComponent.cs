using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace StarterAssets
{

    public class EnemyHealthComponent : MonoBehaviour
    {
        //public Destroy2 TargetSystem;
        public int Health = 100;
        public AudioSource HitSound;
        public AudioSource DamageSound;
        public AudioSource DeadSound;        
        //public ExplodeTarget _ExplodeTarget;
        public EnemySpawnSystem EnemySpawnSystem;
        public GameObject SpawnSystem;
        public Animator EnemyAnimator;
        public EnemyMovement EnemyMovement;
        public Collider EnemyCollider;


        void Start()
        {
            SpawnSystem = GameObject.Find("EnemySpawnSystem");
            EnemySpawnSystem = SpawnSystem.GetComponent<EnemySpawnSystem>();
            EnemyAnimator = GetComponentInChildren<Animator>();
            EnemyMovement = GetComponent<EnemyMovement>();
            EnemyCollider = GetComponent<Collider>();
        }


            public void TakeDamage(int damage)
        {
            //запустить звук урона
            Invoke("HitSoundPlay", 0.3f);
            //HitSound.Play();

            Health = Health - damage;
            Debug.Log("Take Damag! Health = " + Health);
            if (Health <= 0)
            {
                //запустить звук разрушения
                //DeadSound.Play();
                //вызов метода разрушения с задержкой 0,2с
                //TargetSystem.DestroyTarget2();

                Debug.Log("==\\Смерть врага//==");
                EnemyMovement.NavMeshAgent.speed = 0f;
                //EnemyMovement.NavMeshAgent.Stop(); // Устарело!
                EnemyMovement.NavMeshAgent.isStopped = true;
                EnemyMovement._isDead = true;
                DeadSound.Play();
                EnemyAnimator.SetBool("isDead", true);
                EnemySpawnSystem.Spawn();
                EnemyCollider.enabled = false;

                //уничтожение тела через 30 сек
                Invoke("Dead", 30f);

                //_ExplodeTarget.StartExplosion();
                //Destroy(gameObject);
            }
        }

        public void Dead()
        {         
            
            
            
            Destroy(gameObject);
        }



        public void HitSoundPlay()
        {
            DamageSound.Play();
            HitSound.Play();
        }
    }

}
