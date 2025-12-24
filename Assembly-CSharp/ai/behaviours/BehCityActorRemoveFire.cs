using System;

namespace ai.behaviours
{
	// Token: 0x020008AD RID: 2221
	public class BehCityActorRemoveFire : BehCityActor
	{
		// Token: 0x060044AF RID: 17583 RVA: 0x001CEC45 File Offset: 0x001CCE45
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_tile_target = true;
		}

		// Token: 0x060044B0 RID: 17584 RVA: 0x001CEC54 File Offset: 0x001CCE54
		public override BehResult execute(Actor pActor)
		{
			foreach (WorldTile tTile in pActor.current_tile.getTilesAround(3))
			{
				if (tTile != null)
				{
					this.putOutFireForTile(tTile, false);
				}
			}
			return BehResult.Continue;
		}

		// Token: 0x060044B1 RID: 17585 RVA: 0x001CECAC File Offset: 0x001CCEAC
		private void putOutFireForTile(WorldTile pTile, bool pForceEffect = false)
		{
			bool tRemovedFire = false;
			if (pTile.isOnFire())
			{
				pTile.stopFire();
				tRemovedFire = true;
			}
			if (tRemovedFire || pForceEffect)
			{
				EffectsLibrary.spawnAt("fx_water_splash", pTile.pos, 0.1f);
			}
			if (pTile.hasBuilding())
			{
				pTile.building.stopFire();
			}
		}
	}
}
