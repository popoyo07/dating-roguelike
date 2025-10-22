using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] private float masterVolume;
    [SerializeField] private float musicVolume;
    [SerializeField] private float sfxVolume;
    [SerializeField] public CharacterClass c;

    private void Start()
    {
        Load();
    }

    #region Save and Load

    public void LoadData(GameData data)
    {
        this.masterVolume = data.masterVolume;
        this.musicVolume = data.musicVolume;
        this.sfxVolume = data.sfxVolume;
        this.c = data.playerClass;
        
    }

    public void SaveData(ref GameData data)
    {
        data.masterVolume = this.masterVolume;
        data.musicVolume = this.musicVolume;
        data.sfxVolume = this.sfxVolume;
        data.playerClass = this.c;

    }

    #endregion

    private void Load()
    {
        SetMaster(masterVolume);
        SetMusic(musicVolume);
        SetSFX(sfxVolume);
        masterSlider.value = masterVolume;
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;
    }

    public void SetMaster(float level)
    {
        masterVolume = level;
        audioMixer.SetFloat("Master", level);
    }

    public void SetMusic(float level)
    {
        musicVolume = level;
        audioMixer.SetFloat("Music", level);
    }

    public void SetSFX(float level)
    {
        sfxVolume = level;
        audioMixer.SetFloat("SFX", level);
    }

}
