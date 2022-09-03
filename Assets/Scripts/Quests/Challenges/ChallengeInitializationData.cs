using System;
namespace IndieCade
{
    public class ChallengeInitializationData
    {
        private string _stateName;

        public string StateName => _stateName;

        public ChallengeInitializationData(string stateName)
        {
            _stateName = stateName;
        }

        public string BackgroundMusicFilename;
    }
}
