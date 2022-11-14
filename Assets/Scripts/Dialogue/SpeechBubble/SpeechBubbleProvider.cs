using System;
using System.Collections.Generic;
using UnityEngine;

namespace IndieCade
{
    public class SpeechBubbleProvider : MonoBehaviour
    {
        private Dictionary<string, SpeechBubble> _characterNameSpeechBubbleMap;

        public List<SpeechBubble> All => new List<SpeechBubble>(_characterNameSpeechBubbleMap.Values);

        private void Awake()
        {
            _characterNameSpeechBubbleMap = new Dictionary<string, SpeechBubble>();
        }

        public void Register(SpeechBubble speechBubble)
        {
            _characterNameSpeechBubbleMap[speechBubble.CharacterName] = speechBubble;
        }

        public SpeechBubble GetSpeechBubble(string characterName)
        {
            return _characterNameSpeechBubbleMap[characterName];
        }
    }
}
