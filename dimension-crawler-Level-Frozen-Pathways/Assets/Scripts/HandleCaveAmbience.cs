using UnityEngine;
using UnityEngine.Audio;

public class CaveAmbienceTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip caveAmbienceClip;
    [SerializeField] private AudioMixerGroup mixerGroup; 
    [SerializeField] private AudioMixerSnapshot insideSnapshot;
    [SerializeField] private AudioMixerSnapshot outsideSnapshot;
    private AudioSource audioSource;
    private int triggerCount = 0;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.outputAudioMixerGroup = mixerGroup;

        audioSource.clip = caveAmbienceClip;
    }

    private void Update()
    {
        if (triggerCount > 0 && !audioSource.isPlaying)
        {
            audioSource.Play();
            insideSnapshot.TransitionTo(1.0f);
        }
        else if (triggerCount == 0 && audioSource.isPlaying)
        {
            audioSource.Stop();
            outsideSnapshot.TransitionTo(2.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerCount = Mathf.Max(0, triggerCount - 1);
        }
    }
}