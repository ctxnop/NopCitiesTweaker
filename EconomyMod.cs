#region References
using ICities;
#endregion

namespace NopCitiesTweaker
{
	public class EconomyMod
		: EconomyExtensionBase
	{
		public override int OnGetConstructionCost(int originalConstructionCost, Service service, SubService subService, Level level)
		{
			return (int)(base.OnGetConstructionCost(originalConstructionCost, service, subService, level) * ConfigLoader.Config.ConstructionCostMultiplier);
		}

		public override int OnGetMaintenanceCost(int originalMaintenanceCost, Service service, SubService subService, Level level)
		{
			return (int)(base.OnGetMaintenanceCost(originalMaintenanceCost, service, subService, level) * ConfigLoader.Config.MaintenanceCostMultiplier);
		}

		public override int OnGetRelocationCost(int constructionCost, int relocationCost, Service service, SubService subService, Level level)
		{
			return (int)(base.OnGetRelocationCost(constructionCost, relocationCost, service, subService, level) * ConfigLoader.Config.RelocationCostMultiplier);
		}

		public override int OnGetRefundAmount(int constructionCost, int refundAmount, Service service, SubService subService, Level level)
		{
			return (int)(base.OnGetRefundAmount(constructionCost, refundAmount, service, subService, level) * ConfigLoader.Config.RefundMultiplier);
		}
	}
}
