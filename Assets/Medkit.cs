using UnityEngine;

public class Medkit : MonoBehaviour
{
    public int Health = 50;
    public MedkitSpawnSystem MedkitSpawnSystem;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MedkitSpawnSystem = GetComponentInParent<MedkitSpawnSystem>();
    }

    // Update is called once per frame
    public void Destroy()        
    {
        MedkitSpawnSystem.Spawn(); 
        Destroy(gameObject);

    }
}
