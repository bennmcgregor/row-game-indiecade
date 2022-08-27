using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace IndieCade
{
    public class AudioManager : MonoBehaviour
    {

        // Volume settings
        private float _backgroundMusicVolume = 1f;
        private float _soundFXVolume = 1f;
        private float _masterVolume = 1f;

        public float BackgroundMusicVolume
        {
            get => _backgroundMusicVolume;
            set
            {
                _backgroundMusicVolume = value;
                backgroundMusicPlayer.volume = _masterVolume * _backgroundMusicVolume;
            }
        }

        public float SoundFXVolume
        {
            get => _soundFXVolume;
            set
            {
                _soundFXVolume = value;
                soundFXPlayer.volume = _masterVolume * _soundFXVolume;
            }
        }

        public float MasterVolume
        {
            get => _masterVolume;
            set
            {
                _masterVolume = value;
                soundFXPlayer.volume = _masterVolume * _soundFXVolume;
                backgroundMusicPlayer.volume = _masterVolume * _backgroundMusicVolume;
            }
        }

        // Sound assets
        public Dictionary<string, AudioClip> backgroundMusics;
        public Dictionary<string, AudioClip> soundFX;


        // Audio players
        public BackgroundMusicPlayer backgroundMusicPlayer;
        public AudioSource soundFXPlayer;

        // Start is called before the first frame update
        void Start()
        {
            backgroundMusics = new Dictionary<string, AudioClip>();
            backgroundMusicPlayer = gameObject.AddComponent<BackgroundMusicPlayer>();

            soundFX = new Dictionary<string, AudioClip>();
            soundFXPlayer = gameObject.AddComponent<AudioSource>();

            LoadSounds(PathnamesScriptableObject.backgroundMusicPath, backgroundMusics);
            LoadSounds(PathnamesScriptableObject.soundFXPath, soundFX);
            InitVolumes();
        }

        // Public methods
        public void ChangeBackgroundMusic(string musicKey)
        {
            /// Change the background music to the background music with filename "musicKey" (without file extension)
            backgroundMusicPlayer.newSoundtrack(backgroundMusics[musicKey]);
        }

        public void PlaySoundEffect(string soundFXKey)
        {
            /// Play a sound effect with filename "soundFXKey" (without file extension)
            soundFXPlayer.PlayOneShot(soundFX[soundFXKey]);
        }

        // Private methods

        private void LoadSounds(string fpath, Dictionary<string, AudioClip> soundDict)
        {
            DirectoryInfo dir = new DirectoryInfo(PathnamesScriptableObject.resourcesPath + fpath);
            FileInfo[] info = dir.GetFiles("*.mp3");
            foreach (FileInfo f in info)
            {
                string baseName = f.Name.Split(".")[0];
                soundDict.Add(baseName, Resources.Load<AudioClip>(fpath + baseName));
            }
        }

        private void InitVolumes()
        {
            // TODO: Change to be equal to settings in options
            BackgroundMusicVolume = 1f;
            SoundFXVolume = 1f;
            MasterVolume = 1f;
        }
    }
}