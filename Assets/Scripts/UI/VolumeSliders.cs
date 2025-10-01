using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class VolumeSliders : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] AudioSource musicSource;
    //[SerializeField] AudioSource sfxSource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        //sfxSlider = GameObject.Find("SFXSlider").GetComponent<Slider>();
        //musicSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeMusicVolume()
    {
        musicSource.volume = musicSlider.value;
        Save();
    }

    public void ChangeSFXVolume()
    {
        /* sfxSource.volume = sfxSlider.value;
         * Save();
         */
    }

    private void Load()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        //sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
        //PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
    }



}
