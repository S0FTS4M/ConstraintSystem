using Forge.Models.Constraint;
using Forge.Models.Upgradables;

namespace Forge.Models
{
    public class UpgradableLevelMaxedConstraint : ConstraintBase
    {
        #region Variables

        private const string Type = "UpgradableLevelMaxed";

        private IUpgradable upgradable;

        #endregion

        #region Constructor

        public UpgradableLevelMaxedConstraint(IUpgradable upgradable) : base((IConstraintable) upgradable)
        {
            this.upgradable = upgradable;

            // NOTE: Here we define satisfied as level maxed, in other words, if level maxed,
            // then satisfied = true
            satisfied = upgradable.Level == upgradable.MaxLevel;
        }

        #endregion

        #region Methods
        public override void Check()
        {
            var levelMaxedState = upgradable.Level == upgradable.MaxLevel;
            if ((!satisfied && !levelMaxedState) || (satisfied && levelMaxedState))
            {
               return;
            }
            satisfied = levelMaxedState;
            NotifyConstraintStateChanged(satisfied);
        }

        #endregion
    }
}