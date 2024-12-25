using UnityEngine;
using UnityEngine.InputSystem;

namespace StarterAssets
{

    public class PlayerAnimatorController : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public Animator Animator;
        public FirstPersonController FirstPersonController;
        public My_Weapon_Controller My_Weapon_Controller;
        public SoundController SoundController;
        public StarterAssetsInputs StarterAssetsInputs;

        private void Start()
        {
            //My_Weapon_Controller = GetComponent<My_Weapon_Controller>();
            //FirstPersonController = GetComponent<FirstPersonController>();
            //Animator = GetComponent<Animator>();
        }

        //public void ActivateMoveAnimation()
        //{
        //    Animator.SetBool("isMove", true);
        //}


        //public void DeactivateMoveAnimation()
        //{
        //    Animator.SetBool("isMove", false);
        //}


        public void JumpBegin(string s)
        {
            Debug.Log("Подготовка к прыжку закончена!");
            Animator.SetBool("isJump", true);
            Animator.SetBool("isGround", false);
            Animator.SetBool("isSigth", false);
            FirstPersonController.Jump();
            SoundController.JumpSound();
        }
        public void JumpEnd(string s)
        {
            Debug.Log("Прыжок закончен!");
            Animator.SetBool("isJump", false);
            Animator.SetBool("isGround", true);
            SoundController.LandingSound();
        }



        public void StartReload(string s)
        {
            Animator.SetBool("isReloading", true);
            Animator.SetBool("isSight", false);
            FirstPersonController.isReloading = true;

            Debug.Log("Перезарядка начата!");
        }


        public void EndReload(string s)
        {
            My_Weapon_Controller.EndReload();
            Animator.SetBool("isReloading", false);
            Animator.SetBool("isNeedToReload", false);
            FirstPersonController.isReloading = false;
            Debug.Log("Перезарядка закончена!");
            if (StarterAssetsInputs.sight) 
            {
                Animator.SetBool("isSight", true);
            }
        }

        public void Step_1(string s)
        {
            SoundController.StepSound_1();
        }

        public void Step_2(string s)
        {
            SoundController.StepSound_2();
        }
    }
}
