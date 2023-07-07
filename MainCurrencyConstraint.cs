using Forge.Models.CurrencyManagement;

namespace Forge.Models.Constraint
{
    public class MainCurrencyConstraint : ConstraintBase
    {
        #region Variables

        public const string Type = "MainCurrency";

        private IMainCurrencyDecreaser mainCurrencyDecreaser;

        private ICurrencyBase<double> mainCurrency;

        #endregion

        #region Constructor

        public MainCurrencyConstraint(
            IMainCurrencyDecreaser mainCurrencyDecreaser,
            ICurrencyBase<double> mainCurrency
        ) : base((IConstraintable)mainCurrencyDecreaser)
        {
            this.mainCurrencyDecreaser = mainCurrencyDecreaser;
            this.mainCurrency = mainCurrency;

            satisfied = mainCurrency.CanAfford(this.mainCurrencyDecreaser.DecreaseAmount);
        }

        #endregion

        #region Methods
   
        public override void Check()
        {
            var canAfford = mainCurrency.CanAfford(mainCurrencyDecreaser.DecreaseAmount);

            // If state is not changed then return
            if ((!satisfied && !canAfford) || (satisfied && canAfford))
            {
                return;
            }

            satisfied = canAfford;
            NotifyConstraintStateChanged(satisfied);
        }

        #endregion
    }
}