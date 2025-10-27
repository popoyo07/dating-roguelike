using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private AudioMixer audioMixer; // Reference to the AudioMixer controlling audio levels
    [SerializeField] Slider masterSlider;          // UI slider for master volume
    [SerializeField] Slider musicSlider;           // UI slider for music volume
    [SerializeField] Slider sfxSlider;             // UI slider for SFX volume
    [SerializeField] private float masterVolume;    // Stores the current master volume value
    [SerializeField] private float musicVolume;     // Stores the current music volume value
    [SerializeField] private float sfxVolume;       // Stores the current SFX volume value
    [SerializeField] public CharacterClass c;       // Reference to the player's selected class

    private void Start()
    {
        Load(); // Apply saved volume levels and update sliders at start
    }

    #region Save and Load

    // Loads volume and player class data from the saved GameData
    public void LoadData(GameData data)
    {
        this.masterVolume = data.masterVolume;
        this.musicVolume = data.musicVolume;
        this.sfxVolume = data.sfxVolume;
        this.c = data.playerClass;
    }

    // Saves current volume and player class data to GameData
    public void SaveData(ref GameData data)
    {
        data.masterVolume = this.masterVolume;
        data.musicVolume = this.musicVolume;
        data.sfxVolume = this.sfxVolume;
        data.playerClass = this.c;
    }

    #endregion

    // Applies current volume levels to AudioMixer and updates sliders
    private void Load()
    {
        SetMaster(masterVolume); // Apply master volume
        SetMusic(musicVolume);   // Apply music volume
        SetSFX(sfxVolume);       // Apply SFX volume

        // Update slider values to reflect current volume levels
        masterSlider.value = masterVolume;
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;
    }

    // Sets master volume and updates the AudioMixer
    public void SetMaster(float level)
    {
        masterVolume = level;                // Update stored value
        audioMixer.SetFloat("Master", level); // Apply to AudioMixer
    }

    // Sets music volume and updates the AudioMixer
    public void SetMusic(float level)
    {
        musicVolume = level;                 // Update stored value
        audioMixer.SetFloat("Music", level);  // Apply to AudioMixer
    }

    // Sets SFX volume and updates the AudioMixer
    public void SetSFX(float level)
    {
        sfxVolume = level;                   // Update stored value
        audioMixer.SetFloat("SFX", level);    // Apply to AudioMixer
    }
}