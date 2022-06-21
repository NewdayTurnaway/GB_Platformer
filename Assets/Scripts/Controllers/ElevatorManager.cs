using System;
using System.Collections.Generic;
using UnityEngine;

namespace GB_Platformer
{
    internal sealed class ElevatorManager : IDisposable
    {
        private readonly LevelObjectView _playerView;
        private readonly List<LevelObjectView> _elevatorsViews;
        private JointMotor2D _jointMotor;

        public ElevatorManager(LevelObjectView playerView, List<LevelObjectView> elevatorsViews)
        {
            _playerView = playerView;
            _elevatorsViews = elevatorsViews;
            _playerView.OnLevelObjectContact += OnLevelObjectContact;
        }
        private void OnLevelObjectContact(LevelObjectView contactView)
        {
            if (_elevatorsViews.Contains(contactView))
            {
                contactView.gameObject.TryGetComponent(out SliderJoint2D sliderJoint2D);
                _jointMotor = sliderJoint2D.motor;
                _jointMotor.motorSpeed *= -1;
                sliderJoint2D.motor = _jointMotor;
            }
        }
        public void Dispose()
        {
            _playerView.OnLevelObjectContact -= OnLevelObjectContact;
        }
    } 
}
