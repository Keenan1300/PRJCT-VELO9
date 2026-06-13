using UnityEngine;

public class PlayerSFX : MonoBehaviour
{

    public AudioSource audioSource;


    public AudioClip slowBreathing;
    public AudioClip regularBreathing;
    public AudioClip fastBreathing;

    public BreathingDetector breathingDetector;

    private AudioClip currentClip;






    // Update is called once per frame
    void Update()
    {
        if (breathingDetector.HeavyAir)
        {
            Debug.Log("played fast breathing");
            PlayBreathing(fastBreathing);
        } 
        else if (breathingDetector.MediumAir)
        {
            Debug.Log("played regular breathing");

            PlayBreathing(regularBreathing);

        }
        else if (breathingDetector.SlowAir)
        {
            Debug.Log("played slow breathing");

            PlayBreathing(slowBreathing);
        }
     }





        void PlayBreathing (AudioClip clip)
        {
            if (currentClip == clip) return;

            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();

            currentClip = clip;



        }

    }

