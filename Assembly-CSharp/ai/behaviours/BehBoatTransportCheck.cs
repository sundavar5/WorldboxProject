using System;

namespace ai.behaviours
{
	// Token: 0x02000934 RID: 2356
	public class BehBoatTransportCheck : BehBoat
	{
		// Token: 0x06004605 RID: 17925 RVA: 0x001D4408 File Offset: 0x001D2608
		public override BehResult execute(Actor pActor)
		{
			base.checkHomeDocks(pActor);
			if (!this.boat.hasPassengers())
			{
				return base.forceTask(pActor, "boat_transport_check_taxi", true, false);
			}
			WorldTile tTarget = null;
			if (this.boat.countPassengers() > 5 && pActor != null)
			{
				City city = pActor.city;
				bool? flag = (city != null) ? new bool?(city.hasAttackZoneOrder()) : null;
				bool flag2 = true;
				if (flag.GetValueOrDefault() == flag2 & flag != null)
				{
					tTarget = pActor.city.target_attack_zone.centerTile;
				}
			}
			if (tTarget == null)
			{
				WorldTile worldTile;
				if (pActor == null)
				{
					worldTile = null;
				}
				else
				{
					City city2 = pActor.city;
					worldTile = ((city2 != null) ? city2.getTile(false) : null);
				}
				tTarget = worldTile;
			}
			if (tTarget != null)
			{
				this.boat.taxi_target = tTarget;
				pActor.beh_tile_target = tTarget;
				return base.forceTask(pActor, "boat_transport_go_unload", false, false);
			}
			return BehResult.Stop;
		}
	}
}
