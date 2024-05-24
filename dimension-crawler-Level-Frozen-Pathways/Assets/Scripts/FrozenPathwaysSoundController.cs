using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class FrozenPathwaysSoundController : MonoBehaviour
{
    public AudioClip[] snowFootstepSounds;
    public AudioClip[] snowRunningSounds;
    public AudioClip[] iceFootstepSounds;
    public AudioClip[] iceRunningSounds;
    public AudioClip snowLandSound;
    public AudioClip iceLandSound;

    [SerializeField] private PhysicMaterial icePhysicsMaterial;
    [SerializeField] private AudioSource footstepsAudioSource;
    [SerializeField] private AudioSource landingAudioSource;

    private FirstPersonControllerFrozenPathways playerController;

    public float footstepDelay = 0.5f;
    private float footstepTimer = 0f;

    private int lastSnowFootstepIndex = -1;
    private int lastIceFootstepIndex = -1;

    private int lastSnowRunningIndex = -1;
    private int lastIceRunningIndex = -1;

    private List<AudioClip> shuffledFootstepSounds = new List<AudioClip>();
    private int currentFootstepIndex = 0;

    private void Start()
    {
        playerController = GetComponent<FirstPersonControllerFrozenPathways>();
    }

    private void Update()
    {
        footstepTimer += Time.deltaTime;

        if (playerController.isWalking && playerController.isGrounded && footstepTimer >= footstepDelay)
        {
            if (IsOnIce())
                PlayFootstepSound(iceFootstepSounds);
            else
                PlayFootstepSound(snowFootstepSounds);

            footstepTimer = 0f;
        }

        if (playerController.isSprinting && playerController.isGrounded && playerController.isWalking && footstepTimer >= footstepDelay / 1.35f)
        {
            if (IsOnIce())
                PlayRunningSound(iceRunningSounds, ref lastIceRunningIndex);
            else
                PlayRunningSound(snowRunningSounds, ref lastSnowRunningIndex);

            footstepTimer = 0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("OutsideGround") || collision.gameObject.layer == LayerMask.NameToLayer("LowerPuzzleLevel"))
        {
            float verticalVelocity = Mathf.Clamp01(-playerController.GetComponent<Rigidbody>().velocity.normalized.y);

            if (IsOnIce())
                PlayLandSound(iceLandSound, verticalVelocity);
            else
                PlayLandSound(snowLandSound, verticalVelocity);
        }
    }

    private bool IsOnIce()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.collider.sharedMaterial == icePhysicsMaterial)
            {
                return true;
            }
        }
        return false;
    }

    private void PlayFootstepSound(AudioClip[] footstepSounds)
    {
        // Shuffle the array if it's the first footstep or if we've reached the end of the shuffled array
        if (currentFootstepIndex == 0 || currentFootstepIndex >= shuffledFootstepSounds.Count)
        {
            shuffledFootstepSounds.Clear();
            shuffledFootstepSounds.AddRange(footstepSounds.OrderBy(x => Random.value));
            currentFootstepIndex = 0;
        }

        // Play the next footstep sound
        PlaySound(shuffledFootstepSounds[currentFootstepIndex]);
        currentFootstepIndex++;
    }

    private void PlayRunningSound(AudioClip[] runningSounds, ref int lastIndex)
    {
        int randomIndex = GetRandomIndex(lastIndex, runningSounds.Length);
        if (randomIndex != lastIndex)
        {
            lastIndex = randomIndex;
            PlaySound(runningSounds[randomIndex]);
        }
    }


    private void PlayLandSound(AudioClip landSound, float volume)
    {
        PlaySoundAtVolume(landSound, volume, landingAudioSource);
    }

    private void PlaySound(AudioClip clip)
    {
        footstepsAudioSource.clip = clip;
        footstepsAudioSource.Play();
    }

    private void PlaySoundAtVolume(AudioClip clip, float volume, AudioSource audioSource)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }

    private int GetRandomIndex(int lastIndex, int arrayLength)
    {
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, arrayLength);
        } while (randomIndex == lastIndex);
        return randomIndex;
    }
}
