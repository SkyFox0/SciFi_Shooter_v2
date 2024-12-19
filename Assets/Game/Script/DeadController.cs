using StarterAssets;
using UnityEngine;
using UnityEngine.UIElements;

public class DeadController : MonoBehaviour
{
    public Camera MainCamera;
    public Camera DeadCamera;
    public Animator DeadCameraAnimator;
    public Canvas ImageX;
    public Canvas Dead;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayerIsDead()
    {
        DeadCamera.enabled = true;
        MainCamera.enabled = false;        
        DeadCameraAnimator.SetBool("isDead", true);
        ImageX.enabled = false;
        Dead.enabled = true;
    }
}
