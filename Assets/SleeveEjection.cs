using Unity.VisualScripting;
using UnityEngine;

public class SleeveEjection : MonoBehaviour
{   
    public GameObject _sleeve;    
    public Vector3 _direction;
    public Vector3 _rotation;
    public GameObject _target;
    public AudioSource _sound;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Eject()
    {
        _target = GameObject.Find("Direction_01");
        _direction =  (_target.transform.position - transform.position)*2f;
        _rotation = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        //_direction = new Vector3(0f, 0f, 0.5f);
        //_direction = _sleeve.transform.rotation.
        //Debug.Log(_direction.ToString());
        //Debug.Log(_rotation.ToString());
        
        _sleeve.GetComponent<Rigidbody>().isKinematic = false;
        _sleeve.GetComponent<Rigidbody>().useGravity = true;

        //_rd.AddForceAtPosition(direction, hit.point, ForceMode.Impulse);
        _sleeve.GetComponent<Rigidbody>().AddForce(_direction, ForceMode.Impulse);
        _sleeve.GetComponent<Rigidbody>().AddTorque(_rotation, ForceMode.Impulse);
        Invoke("PlaySound", 0.5f);
        Invoke("SleeveDead", 300f);
    }
    public void PlaySound()
    {
        _sound.Play();
    }
    
    public void SleeveDead() 
    {
        //Destroy(_sleeve);
        _sleeve.SetActive(false);
    }
}
