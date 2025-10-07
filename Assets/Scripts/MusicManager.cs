using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    public void SetMaster(float level)
    {
        audioMixer.SetFloat("Master", level);
    }

    public void SetMusic(float level)
    {
        audioMixer.SetFloat("Music", level);
    }

    public void SetSFX(float level)
    {
        audioMixer.SetFloat("SFX", level);
    }
}
