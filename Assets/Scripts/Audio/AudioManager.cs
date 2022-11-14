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
                _soundFXPlayer.SetVolume(_masterVolume * _soundFXVolume);
            }
        }

        public float MasterVolume
        {
            get => _masterVolume;
            set
            {
                _masterVolume = value;
                _soundFXPlayer.SetVolume(_masterVolume * _soundFXVolume);
                _backgroundMusicPlayer.volume = _masterVolume * _backgroundMusicVolume;
            }
        }

        [SerializeField] private AudioClip[] _backgroundMusicClips;
        [SerializeField] private SoundFXPlayer _soundFXPlayer;

        // Sound assets
        private Dictionary<string, AudioClip> _backgroundMusics;

        // Audio players
        private BackgroundMusicPlayer _backgroundMusicPlayer;

        // Save data
        private string _currentBackgroundMusic;

        public string CurrentBackgroundMusic => _currentBackgroundMusic;
        public SoundFXPlayer SoundFXPlayer => _soundFXPlayer;

        // Start is called before the first frame update
        void Start()
        {
            _backgroundMusics = new Dictionary<string, AudioClip>();
            foreach (var clip in _backgroundMusicClips)
            {
                _backgroundMusics[clip.name] = clip;
            }

            _backgroundMusicPlayer = gameObject.AddComponent<BackgroundMusicPlayer>();

            InitVolumes();
        }

        // Public methods
        public void ChangeBackgroundMusic(string musicKey)
        {
            _currentBackgroundMusic = musicKey;
            _backgroundMusicPlayer.newSoundtrack(_backgroundMusics[musicKey]);
        }

        // Private methods

        private void InitVolumes()
        {
            // TODO: Change to be equal to settings in options
            BackgroundMusicVolume = 1f;
            SoundFXVolume = 1f;
            MasterVolume = 1f;
        }
    }
}