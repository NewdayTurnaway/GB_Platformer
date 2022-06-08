using System.Collections.Generic;

namespace GB_Platformer
{
    internal sealed class Controllers : IInitialization, IExecute, ILateExecute, IFixedExecute
    {
        private readonly List<IInitialization> _initializations;
        private readonly List<IExecute> _executes;
        private readonly List<ILateExecute> _lateExecutes;
        private readonly List<IFixedExecute> _fixedExecutes;

        internal Controllers()
        {
            _initializations = new();
            _executes = new();
            _lateExecutes = new();
            _fixedExecutes = new();
        }

        internal Controllers Add(IController controller)
        {
            if(controller is IInitialization initialization)
            {
                _initializations.Add(initialization);
            }
            if(controller is IExecute execute)
            {
                _executes.Add(execute);
            }
            if (controller is ILateExecute lateExecute)
            {
                _lateExecutes.Add(lateExecute);
            }
            if (controller is IFixedExecute fixedExecute)
            {
                _fixedExecutes.Add(fixedExecute);
            }

            return this;
        }

        public void Initialization()
        {
            for (int i = 0; i < _initializations.Count; i++)
            {
                _initializations[i].Initialization();
            }
        }

        public void Execute()
        {
            for (int i = 0; i < _executes.Count; i++)
            {
                _executes[i].Execute();
            }
        }

        public void LateExecute()
        {
            for (int i = 0; i < _lateExecutes.Count; i++)
            {
                _lateExecutes[i].LateExecute();
            }
        }

        public void FixedExecute()
        {
            for (int i = 0; i < _fixedExecutes.Count; i++)
            {
                _fixedExecutes[i].FixedExecute();
            }
        }
    }
}