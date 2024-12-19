using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
//using static WPN_Decal_Manager;

public class Sleeve_Manager : MonoBehaviour
{
    [SerializeField] private GameObject _sleeve;
    public GameObject MY_SLEEVE_POOL;
    public int _sleevePoolSize = 300;
    public List<GameObject> m_Sleeve = new List<GameObject>();
    public SleeveEjection _sleeveEjection;
    //public List<AudioSource> m = new List<AudioSource>();

    public Vector3 _position;
    //public AudioSource _sound;
    public int i;
    public int _currentSleeve;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //m_Sleeve[].Length = 310;
        _position = transform.position;
        //Debug.Log(_position.ToString());

        for (i = 0; i< _sleevePoolSize; i +=1)  
        {
            Invoke("_sleeveSpawn", i*0.1f);
        }
        _currentSleeve = 0;
    }
    public void _sleeveSpawn()
    {        
        GameObject Gm = Instantiate(_sleeve, _position, transform.rotation);
        m_Sleeve.Add(Gm);        
        int y = m_Sleeve.Count - 1;
        m_Sleeve[y].gameObject.SetActive(false);
        m_Sleeve[y].transform.SetParent(MY_SLEEVE_POOL.transform);

        // Если надо проиграть звук (доступ к скрипту):
        //int y = m_Sleeve.Count - 1;
        //_sleeveEjection = m_Sleeve[y].GetComponent<SleeveEjection>();
        //_sleeveEjection.Invoke("PlaySound", 0.5f); 


        //m_Sleeve[y].GetComponent<Rigidbody>().isKinematic = false;
        //m_Sleeve[y].GetComponent<Rigidbody>().useGravity = true;
    }

    public void SleeveEject()
    {
        // передать данные о гильзе в скрипт стрельбы 


        // Изменение счетчика новой гильзы
        if (_currentSleeve < _sleevePoolSize-1)
        {
            _currentSleeve += 1;
        }
        else
        {
            _currentSleeve = 0;
        }
        
    }
}
