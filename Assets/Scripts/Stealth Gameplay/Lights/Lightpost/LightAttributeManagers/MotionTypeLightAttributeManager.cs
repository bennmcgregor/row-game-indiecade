using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieCade
{
    public class MotionTypeLightAttributeManager : LightAttributeManager
    {
        [SerializeField] List<LightMotionTypeManager> _motionTypeManagers;

        private Dictionary<LightMotionType, LightMotionTypeManager> _motionTypeManagersMap;
        private LightMotionTypeManager _currentManager = null;

        private void Awake()
        {
            _motionTypeManagersMap = new Dictionary<LightMotionType, LightMotionTypeManager>();

            foreach (var manager in _motionTypeManagers)
            {
                _motionTypeManagersMap[manager.Type] = manager;
            }
        }

        protected override void InitializeWithDataInternal(LightStateData data)
        {
            foreach (var manager in _motionTypeManagersMap.Values)
            {
                manager.InitializeWithData(_lightInstance);
            }
        }

        protected override void UpdateStateInternal(LightStateData data)
        {
            if (_currentManager != null)
            {
                _currentManager.Pause();
            }

            _currentManager = _motionTypeManagersMap[data.MotionType.Type];
            _currentManager.Resume(data.MotionType);
        }
    }
}
