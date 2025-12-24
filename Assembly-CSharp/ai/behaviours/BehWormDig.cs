using System;

namespace ai.behaviours
{
	// Token: 0x0200090E RID: 2318
	public class BehWormDig : BehaviourActionActor
	{
		// Token: 0x0600459A RID: 17818 RVA: 0x001D2D74 File Offset: 0x001D0F74
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_tile_target = true;
		}

		// Token: 0x0600459B RID: 17819 RVA: 0x001D2D84 File Offset: 0x001D0F84
		public override BehResult execute(Actor pActor)
		{
			int size;
			pActor.data.get("size", out size, 0);
			if (size > 0 && pActor.beh_tile_target.Height < 220)
			{
				BehaviourActionBase<Actor>.world.loopWithBrush(pActor.beh_tile_target, Brush.get(size, "circ_"), new PowerActionWithID(BehWormDig.tileDrawWorm), null);
			}
			else
			{
				BehaviourActionBase<Actor>.world.loopWithBrush(pActor.beh_tile_target, Brush.get(size, "circ_"), new PowerActionWithID(BehWormDig.tileFlashWorm), null);
			}
			return BehResult.RestartTask;
		}

		// Token: 0x0600459C RID: 17820 RVA: 0x001D2E0D File Offset: 0x001D100D
		public static bool tileFlashWorm(WorldTile pTile, string pPowerID)
		{
			BehaviourActionBase<Actor>.world.flash_effects.flashPixel(pTile, 20, ColorType.White);
			return true;
		}

		// Token: 0x0600459D RID: 17821 RVA: 0x001D2E23 File Offset: 0x001D1023
		public static bool tileDrawWorm(WorldTile pTile, string pPowerID)
		{
			BehWormDig.wormTile(pTile);
			return true;
		}

		// Token: 0x0600459E RID: 17822 RVA: 0x001D2E2C File Offset: 0x001D102C
		public static void wormTile(WorldTile pTile)
		{
			BehaviourActionBase<Actor>.world.flash_effects.flashPixel(pTile, 20, ColorType.White);
			if (pTile.top_type != null)
			{
				MapAction.decreaseTile(pTile, false, "flash");
				return;
			}
			if (pTile.Type.increase_to != null && !pTile.Type.road)
			{
				bool flag = pTile.Type.increase_to.id.StartsWith("mountain", StringComparison.Ordinal);
				bool tIncHill = pTile.Type.increase_to.id.StartsWith("hill", StringComparison.Ordinal);
				if (!flag && !tIncHill && (pTile.Type.decrease_to == null || Randy.randomBool()))
				{
					MapAction.increaseTile(pTile, false, "destroy");
					return;
				}
				if (pTile.Type.decrease_to != null)
				{
					MapAction.decreaseTile(pTile, false, "destroy");
				}
			}
		}
	}
}
