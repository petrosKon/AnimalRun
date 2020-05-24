using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioSource[] audiosInGame;
    public Button[] buttons;

    [Header("Music On")]
    public Sprite soundOnSprite;

    [Header("Music Off")]
    public Sprite soundOffSprite;

    private bool soundPlaying = true;

    private static SoundManager _instance;
    public static SoundManager instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        //analogous to player settings change to sound on or off
        if (PlayerPrefs.GetInt("Sound").Equals(1))
        {
            //this is used in order to change the button sprite on the main menu
            if(audiosInGame.Length != 0)
            {
                foreach (AudioSource audioSource in audiosInGame)
                {
                    audioSource.mute = true;
                    foreach (Button button in buttons)
                    {
                        button.image.sprite = soundOffSprite;
                    }
                }
            }
            else
            {
                foreach (Button button in buttons)
                {
                    button.image.sprite = soundOffSprite;
                }
            }
          
        }
        else
        {
            //this is used in order to change the button sprite on the main menu
            if (audiosInGame.Length != 0)
            {
                foreach (AudioSource audioSource in audiosInGame)
                {
                    audioSource.mute = false;
                    foreach (Button button in buttons)
                    {
                        button.image.sprite = soundOnSprite;
                    }
                }
            }
            else
            {
                foreach (Button button in buttons)
                {
                    button.image.sprite = soundOffSprite;
                }
            }
        }
    }

    public void ManageAudio()
    {
        foreach (AudioSource audioSource in audiosInGame)
        {
            //if player clicks and the audio is muted then unmute and change the icon
            if (audioSource.mute == true)
            {
                audioSource.mute = false;
                soundPlaying = false;
                PlayerPrefs.SetInt("Sound", (soundPlaying ? 1 : 0));
                foreach (Button button in buttons)
                {
                    button.image.sprite = soundOnSprite;
                }
            }
            else
            {
                audioSource.mute = true;
                soundPlaying = true;
                PlayerPrefs.SetInt("Sound", (soundPlaying ? 1 : 0));
                foreach (Button button in buttons)
                {
                    button.image.sprite = soundOffSprite;
                }
            }
        }
    }

    //change preferences from the main menu, according to sprites NOT on the muted sounds.
    public void ManageAudioMainMenu()
    {
        foreach(Button button in buttons)
        {
            if(button.image.sprite == soundOnSprite)
            {
                soundPlaying = true;
                PlayerPrefs.SetInt("Sound", (soundPlaying ? 1 : 0));
                button.image.sprite = soundOffSprite;
            }
            else
            {
                soundPlaying = false;
                PlayerPrefs.SetInt("Sound", (soundPlaying ? 1 : 0));
                button.image.sprite = soundOnSprite;
            }
        }
    }
}

