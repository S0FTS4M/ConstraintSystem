using Forge.Helpers;

namespace Forge.Models.Constraint
{
    public delegate void ConstraintHandler(IConstraintable constraintable, bool state);
    public interface IConstraintBase
    {
        bool Satisfied { get; }

        event ConstraintHandler SatisfiedStateChanged;

        void Check();

        void SetContainer(IConstraintContainer constraintContainer);
    }

    public abstract class ConstraintBase : IConstraintBase
    {
        #region Variables

        protected bool satisfied;

        private IConstraintContainer constraintContainer;

        private IConstraintable constraintable;

        protected IGameBase gameBase;

        #endregion

        #region Props

        public bool Satisfied => satisfied;

        #endregion

        #region Events

        public event ConstraintHandler SatisfiedStateChanged;

        #endregion

        #region Constructor

        public ConstraintBase(IConstraintable constraintable)
        {
            this.constraintable = constraintable;

            gameBase = Verfices.Request<IGameBase>();
        }


        #endregion

        #region Methods

        protected void NotifyConstraintStateChanged(bool satisfied)
        {
            SatisfiedStateChanged?.Invoke(constraintable, satisfied);
            constraintContainer?.StateChangedInConstraints();
        }

        public void SetContainer(IConstraintContainer constraintContainer)
        {
            this.constraintContainer = constraintContainer;
        }

        public abstract void Check();

        #endregion
    }
}