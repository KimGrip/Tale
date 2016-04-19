using UnityEngine;
using System.Collections;

public class scr_AudioManager : MonoBehaviour 
{
    public AudioSource efxSource;
    public AudioSource musicSource;
    public static scr_AudioManager instance = null;

    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;

    public AudioClip moveSound_1;
    public AudioClip moveSound_2;

    public AudioClip DrawBowSound_1;
    public AudioClip ShoowBowSound_1;
    




    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }
    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }
    void Update()
    {

    }

}
