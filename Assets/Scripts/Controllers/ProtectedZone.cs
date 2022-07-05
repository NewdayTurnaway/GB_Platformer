using System.Collections.Generic;
using UnityEngine;

namespace GB_Platformer
{
    internal sealed class ProtectedZone : IInitialization, IDeinitialization
    {
        private readonly List<IProtector> _protectors;
        private readonly LevelObjectTrigger _view;

        public ProtectedZone(LevelObjectTrigger view, List<IProtector> protectors)
        {
            _view = view;
            _protectors = protectors;
        }

        public void Initialization()
        {
            _view.TriggerEnter += OnContact;
            _view.TriggerExit += OnExit;
        }

        public void Deinitialization()
        {
            _view.TriggerEnter -= OnContact;
            _view.TriggerExit -= OnExit;
        }

        private void OnContact(GameObject gameObject)
        {
            foreach (IProtector protector in _protectors)
            {
                protector.StartProtection(gameObject);
            }
        }

        private void OnExit(GameObject gameObject)
        {
            foreach (IProtector protector in _protectors)
            {
                protector.FinishProtection(gameObject);
            }
        }
    }
}