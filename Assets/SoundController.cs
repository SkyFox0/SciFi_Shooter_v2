using UnityEngine;


namespace StarterAssets
{

public class SoundController : MonoBehaviour
{
        public AudioSource AudioSource_Dead;
        public AudioSource AudioSource_LF;
        public AudioSource AudioSource_RF;
        public AudioSource AudioSource_Jump;
        public AudioSource AudioSource_Landing;
        public AudioSource AudioSource_Damage;
        public AudioSource AudioSource_Hit;
        public AudioSource AudioSource_Breahedge;
        public AudioSource AudioSource_Heart;
        public AudioSource AudioSource_Reload;
        public AudioClip Step1;
        public AudioClip Step2;
        public AudioClip Jump;
        public AudioClip Landing;
        public AudioClip TakeDamage;
        public AudioClip Hit;
        public AudioClip Dead;

        public void StepSound_1()
        {
            //AudioSource_LF.Stop();
            //AudioSource_LF.clip = Step1;
            AudioSource_LF.Play(); 
        }
        public void StepSound_2()
        {
            //AudioSource_RF.Stop();
            //AudioSource_RF.clip = Step2;
            AudioSource_RF.Play();
        }

        public void JumpSound()
        {
            //AudioSource_Jump.Stop();
            //AudioSource_Jump.clip = Jump;
            AudioSource_Jump.Play();
        }

        public void LandingSound()
        {
            //AudioSource_Landing.Stop();
            //AudioSource_Landing.clip = Landing;
            AudioSource_Landing.Play();
        }

        public void Damage()
        {
            //AudioSource.Stop();
            //AudioSource_Damage.clip = TakeDamage;
            AudioSource_Damage.Play();
            AudioSource_Hit.Play();
        }
        public void DeadSound()
        {
            //AudioSource.Stop();
           //AudioSource.clip = Dead;
            AudioSource_Dead.Play();
            AudioSource_Breahedge.Stop();
            AudioSource_Heart.Play();
        }

        public void Reload()
        {
            AudioSource_Reload.Play();
        }


    }
}

