using UnityEngine;

public class Destroy2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void DestroyTarget2()
    {
        Debug.Log("”ничтожить мишень полностью");
        Invoke("DestroyTargetFull", 2f);
    }
    public void DestroyTargetFull()
    { 
        GameObject.Destroy(gameObject);
    }

}
