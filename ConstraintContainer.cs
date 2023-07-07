using System;
using System.Collections.Generic;

namespace Forge.Models.Constraint
{
    public interface IConstraintContainer
    {
        bool ConstraintsSatisfied { get; }

        event Action<IConstraintable, bool> StateChanged;

        void AddConstraint(string type, IConstraintBase constraint);
        bool CheckConstraints();
        IConstraintBase GetConstraint(string type);
        void StateChangedInConstraints();
    }

    public class ConstraintContainer : IConstraintContainer
    {
        #region Variables

        private bool state = true;

        private IConstraintable constraintable;

        Dictionary<string, IConstraintBase> constraintsDict;

        #endregion

        #region Props
        public bool ConstraintsSatisfied { get => state; }

        #endregion

        #region Events

        public event Action<IConstraintable, bool> StateChanged;

        #endregion

        #region Constructor

        public ConstraintContainer(IConstraintable constraintable)
        {
            this.constraintable = constraintable;
            constraintsDict = new Dictionary<string, IConstraintBase>();
        }

        #endregion

        #region Methods
        public bool CheckConstraints()
        {
            var result = true;
            var constraints = constraintsDict.Values;
            foreach (var constraint in constraints)
            {
                result &= constraint.Satisfied;
            }
            return result;
        }

        public void StateChangedInConstraints()
        {
            var newState = CheckConstraints();

            // state is not changed so return
            if (state == newState) return;

            state = newState;
            StateChanged?.Invoke(constraintable, state);
        }

        public void AddConstraint(string type, IConstraintBase constraint)
        {
            if (constraintsDict.ContainsKey(type))
            {
                UnityEngine.Debug.LogError("TYPE: " + type + " is already added.");
                return;
            }

            constraintsDict.Add(type, constraint);

            constraint.SetContainer(this);

            //when we start adding these values we can setup state value
            state &= constraint.Satisfied;
        }

        public IConstraintBase GetConstraint(string type)
        {
            bool hasValue = constraintsDict.TryGetValue(type, out IConstraintBase constraint);

            if (hasValue == false)
            {
                UnityEngine.Debug.LogError("Constraint not found in container. TYPE:" + type);
                return null;
            }

            return constraint;
        }
        #endregion
    }
}