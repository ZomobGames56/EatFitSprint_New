using UnityEngine;
using UnityEngine.UI;

public class HY_AudioManager : MonoBehaviour
{
    public static HY_AudioManager instance;
    
   public AudioSource bgAudioSource,forOnShotPlayAudioSource;
    [SerializeField]
    AudioClip[] bgClips;
     public Slider musicSlider,sfxSlider;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        int rndIndex=Random.Range(0, bgClips.Length);
        StartBackgroundMusic(bgClips[rndIndex]);
        //musicSlider.value = 0.1f;
       // sfxSlider.value = 1f;
    }

   public void StartBackgroundMusic(AudioClip BackgroundClip)
    {
        bgAudioSource.clip = BackgroundClip;
        bgAudioSource.Play();
        
    }
    private void Update()
    {
        //bgAudioSource.volume = musicSlider.value;
        //forOnShotPlayAudioSource.volume = sfxSlider.value;
    }
    public void PlayAudioEffectOnce(AudioClip effectClip)
    {
        forOnShotPlayAudioSource.PlayOneShot(effectClip);
    }

    //void OnApplicationFocus(bool hasFocus)
    //{
    //    if (!hasFocus && bgAudioSource.isPlaying)
    //    {
    //        bgAudioSource.Pause();

    //    }
    //    else if (hasFocus)
    //    {
    //        bgAudioSource.UnPause();
    //    }

    //}

    //void OnApplicationPause(bool isPaused)
    //{
    //    if (isPaused && bgAudioSource.isPlaying)
    //    {
    //        bgAudioSource.Pause();
    //    }
    //    else if (!isPaused)
    //    {
    //        bgAudioSource.UnPause();
    //    }
    //}

}
