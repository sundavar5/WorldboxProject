using System;

namespace ai.behaviours
{
	// Token: 0x02000906 RID: 2310
	public class BehTeleportHome : BehaviourActionActor
	{
		// Token: 0x06004588 RID: 17800 RVA: 0x001D2898 File Offset: 0x001D0A98
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.hasCity())
			{
				return BehResult.Stop;
			}
			City tCity = pActor.getCity();
			if (!tCity.hasZones())
			{
				return BehResult.Stop;
			}
			TileZone tZone = tCity.zones.GetRandom<TileZone>();
			if (tZone == null)
			{
				return BehResult.Stop;
			}
			WorldTile tRandomTile = tZone.tiles.GetRandom<WorldTile>();
			ActionLibrary.teleportEffect(pActor, tRandomTile);
			pActor.cancelAllBeh();
			pActor.spawnOn(tRandomTile, 0f);
			pActor.doCastAnimation();
			return BehResult.Continue;
		}
	}
}
