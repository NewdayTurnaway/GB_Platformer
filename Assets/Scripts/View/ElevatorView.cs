using UnityEngine;

namespace GB_Platformer
{
    internal sealed class ElevatorView : LevelObjectView
    {
        [SerializeField] private SliderJoint2D _sliderJoint2D;

        private JointMotor2D _jointMotor;

        public SliderJoint2D SliderJoint2D => _sliderJoint2D;

        protected override void OnValidate()
        {
            base.OnValidate();
            _sliderJoint2D = _sliderJoint2D != null ? _sliderJoint2D : GetComponent<SliderJoint2D>();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerView _))
            {
                _jointMotor = _sliderJoint2D.motor;
                _jointMotor.motorSpeed *= -1;
                _sliderJoint2D.motor = _jointMotor;
            }
        }
    } 
}
