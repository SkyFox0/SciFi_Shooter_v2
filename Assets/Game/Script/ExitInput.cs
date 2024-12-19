using UnityEngine;
using UnityEngine.InputSystem;

public class ExitInput : MonoBehaviour
{
    //public ShootComponent ShootComponent;
    //public AudioSource ShootSound;
    public void OnExit(InputValue value)
    {
        Debug.Log("Exit");
        Application.Quit();    // закрыть приложение
    }
}
