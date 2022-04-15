using NaughtyAttributes;
using UnityEngine;

public class AudioFXRandom : MonoBehaviour
{
    [SerializeField] AudioSource audioSouce;
    [SerializeField, HorizontalLine] AudioClip[] audioClips;
    [SerializeField, MinMaxSlider(0f, 1f), HorizontalLine] Vector2 minMaxVolume;
    [SerializeField, MinMaxSlider(-3f, 3f)] Vector2 minMaxPitch;

    [HorizontalLine, InfoBox("If not checked, clip will not play if source is already playing.")]
    [SerializeField] bool stopSourceIfPlaying;

    [SerializeField] bool playOneShot;

    [Button]
    public void PlayRandomFX()
    {
        if (audioClips.Length == 0) return;
        SetRandomVolume(minMaxVolume);
        SetRandomPitch(minMaxPitch);
        InitializeFX(audioClips[Random.Range(0, audioClips.Length)]);
    }

    public void PlayFXAtIndex(int index)
    {
        if (audioClips.Length == 0) return;
        if (index < audioClips.Length)
            InitializeFX(audioClips[index]);
    }

    void SetRandomVolume(Vector2 minMax) => audioSouce.volume =
        Mathf.Clamp01(Random.Range(Mathf.Min(minMax.x, minMax.y), Mathf.Max(minMax.x, minMax.y)));

    void SetRandomPitch(Vector2 minMax) => audioSouce.pitch =
        audioSouce.pitch = Mathf.Clamp(Random.Range(Mathf.Min(minMax.x, minMax.y), Mathf.Max(minMax.x, minMax.y)), -3, 3);

    void InitializeFX(AudioClip clip)
    {
        if (audioSouce == null || audioClips.Length == 0) return;
        audioSouce.clip = clip;
        if (playOneShot)
        {
            audioSouce.PlayOneShot(clip);
            return;
        }
        else
        {
            if (stopSourceIfPlaying)
            {
                audioSouce.Stop();
                audioSouce.Play();
            }
            else
            {
                if (!audioSouce.isPlaying)
                    audioSouce.Play();
            }
        }
    }

        public void SetAudioSource(AudioSource newSource) => audioSouce = newSource;
    }