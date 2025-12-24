using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ai.behaviours
{
	// Token: 0x0200088F RID: 2191
	public class BehBurnTumorTiles : BehaviourActionActor
	{
		// Token: 0x06004465 RID: 17509 RVA: 0x001CDD2C File Offset: 0x001CBF2C
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.current_tile.Type.ground)
			{
				return BehResult.Stop;
			}
			WorldTile tTarget = null;
			List<WorldTile> tTiles = BehBurnTumorTiles.tiles;
			this.checkRegion(pActor.current_tile.region, tTiles);
			if (tTiles.Count != 0)
			{
				tTarget = tTiles.GetRandom<WorldTile>();
			}
			else
			{
				List<MapRegion> tNeighbours = pActor.current_tile.region.neighbours;
				for (int i = 0; i < tNeighbours.Count; i++)
				{
					this.checkRegion(tNeighbours[i], tTiles);
					if (tTiles.Count != 0)
					{
						tTarget = tTiles.GetRandom<WorldTile>();
						break;
					}
				}
			}
			tTiles.Clear();
			if (tTarget == null)
			{
				return BehResult.Stop;
			}
			AttackAction action = AssetManager.spells.get("cast_fire").action;
			if (action != null)
			{
				action(pActor, null, tTarget);
			}
			pActor.doCastAnimation();
			return BehResult.Continue;
		}

		// Token: 0x06004466 RID: 17510 RVA: 0x001CDDF0 File Offset: 0x001CBFF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void checkRegion(MapRegion pRegion, List<WorldTile> pTiles)
		{
			List<WorldTile> tTiles = pRegion.tiles;
			for (int i = 0; i < tTiles.Count; i++)
			{
				WorldTile tTile = tTiles[i];
				if (tTile.Type.creep)
				{
					pTiles.Add(tTile);
				}
			}
		}

		// Token: 0x0400316A RID: 12650
		private static List<WorldTile> tiles = new List<WorldTile>();
	}
}
