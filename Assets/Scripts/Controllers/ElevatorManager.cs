using System;
using UnityEngine;

namespace GB_Platformer
{
    internal sealed class ElevatorManager : IDisposable
    {
        private readonly LevelObjectView _playerView;

        public ElevatorManager(LevelObjectView playerView)
        {
            _playerView = playerView;
            _playerView.OnLevelObjectContact += OnLevelObjectContact;
        }

        private void OnLevelObjectContact(LevelObjectView contactView)
        {
            if (contactView is ElevatorView elevatorView)
            {
                SliderJoint2D sliderJoint2D = elevatorView.SliderJoint2D;
                JointMotor2D jointMotor = sliderJoint2D.motor;
                jointMotor.motorSpeed *= -1;
                sliderJoint2D.motor = jointMotor;
            }
        }

        public void Dispose()
        {
            _playerView.OnLevelObjectContact -= OnLevelObjectContact;
        }
    } 
}
