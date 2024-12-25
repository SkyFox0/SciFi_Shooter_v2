using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UIElements;

public class OldSpeakerSoundController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //public AudioClip[] clips;
    public AudioSource[] Source;
    public AudioSource Main1;
    public AudioSource Main2;
    public AudioSource Hit1;
    public AudioSource Hit2;
    public AudioSource Error1;
    public AudioSource Error2;
    public AudioSource DestroySound;

    public int last;
    public int current;
    public int next;
    public float lenght;
    public bool isPlaying;
    public float _timer;
    public int _main;

    void Start()
    {
        _main = 1;

        last = -1;
        _timer = 0f;
        current = 0;
        next = 1;
        //while ((next == current) | (next == last))
        //{
        //    next = Random.Range(0, Source.Length);
        //}

        //[Random.Range(0, SpawnPoints.Length)];
        //Main1.clip = clips[current];
        Source[current].Play();
        isPlaying = true;
        //lenght = Main.clip.length + 0.5f;
        //Invoke("PlayNext", length);
    }

    // Update is called once per frame
    public void LoadNext()
    {
        isPlaying = false;
        if (_main == 1)
        {
            last = current;
            current = next;
            Main1.Stop();
                //Main2.clip = clips[current];
            next = Random.Range(0, Source.Length);
            while ((next == current) | (next == last))
            {
                next = Random.Range(0, Source.Length);
            }
            _main = 2;
            PlayNext_2();
            //Invoke("PlayNext_2", 1f);
        }
        else
        {
            last = current;
            current = next;
            Main2.Stop();
                //Main1.clip = clips[current];
            next = Random.Range(0, Source.Length);
            while ((next == current) | (next == last))
            {
                next = Random.Range(0, Source.Length);
            }
            _main = 1;
            PlayNext_1();
            //Invoke("PlayNext_1", 1f);
        }        
    }
    public void PlayNext()
    {
        //Source[current].Stop();
        last = current;
        current = next;
        next += 5;
        if (next >= Source.Length) 
        { 
            next -= 14; 
        }
        Source[current].Play();
        isPlaying = true;
        Source[last].Stop();

    }


    public void PlayNext_1()
    {
        Main1.Play();
        //Main1.PlayOneShot(clips[current]);
        isPlaying = true;
    }

    public void PlayNext_2()
    {
        Main2.Play();
        //Main2.PlayOneShot(clips[current]);
        isPlaying = true;
    }
    

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 3f)
        {
            _timer = 0f;    
            if (!Source[current].isPlaying && isPlaying)
            {
                isPlaying = false;
                PlayNext();
            }
        }         
    }
}
