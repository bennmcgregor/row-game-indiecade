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
                _backgroundMusicPlayer.volume = _masterVolume * _backgroundMusicVolume;
            }
        }

        public float SoundFXVolume
        {
            get => _soundFXVolume;
            set
            {
                _soundFXVolume = value;
                _soundFXPlayer.volume = _masterVolume * _soundFXVolume;
            }
        }

        public float MasterVolume
        {
            get => _masterVolume;
            set
            {
                _masterVolume = value;
                _soundFXPlayer.volume = _masterVolume * _soundFXVolume;
                _backgroundMusicPlayer.volume = _masterVolume * _backgroundMusicVolume;
            }
        }

        // Sound assets
        private Dictionary<string, AudioClip> _backgroundMusics;
        private Dictionary<string, AudioClip> _soundFX;

        // Audio players
        private BackgroundMusicPlayer _backgroundMusicPlayer;
        private AudioSource _soundFXPlayer;

        // Save data
        private string _currentBackgroundMusic;

        public string CurrentBackgroundMusic => _currentBackgroundMusic;

        // Start is called before the first frame update
        void Start()
        {
            _backgroundMusics = new Dictionary<string, AudioClip>();
            _backgroundMusicPlayer = gameObject.AddComponent<BackgroundMusicPlayer>();

            _soundFX = new Dictionary<string, AudioClip>();
            _soundFXPlayer = gameObject.AddComponent<AudioSource>();

            LoadSounds(PathnamesScriptableObject.backgroundMusicPath, _backgroundMusics);
            LoadSounds(PathnamesScriptableObject.soundFXPath, _soundFX);
            InitVolumes();
        }

        // Public methods
        public void ChangeBackgroundMusic(string musicKey)
        {
            _currentBackgroundMusic = musicKey;
            _backgroundMusicPlayer.newSoundtrack(_backgroundMusics[musicKey]);
        }

        public void PlaySoundEffect(string soundFXKey)
        {
            /// Play a sound effect with filename "soundFXKey" (without file extension)
            _soundFXPlayer.PlayOneShot(_soundFX[soundFXKey]);
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