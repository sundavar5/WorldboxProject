using System;
using UnityEngine;

namespace ai.behaviours
{
	// Token: 0x0200094B RID: 2379
	public class BehDragonFindRandomTile : BehaviourActionActor
	{
		// Token: 0x06004648 RID: 17992 RVA: 0x001DCBE4 File Offset: 0x001DADE4
		public unsafe override BehResult execute(Actor pActor)
		{
			if (pActor.beh_tile_target != null)
			{
				return BehResult.Continue;
			}
			WorldTile tTile = Toolbox.getRandomTileWithinDistance(pActor.current_tile, 100);
			if (!BehaviourActionBase<Actor>.world.islands_calculator.hasGround())
			{
				pActor.beh_tile_target = tTile;
				return BehResult.Continue;
			}
			int tTries = 5;
			while (!tTile.Type.ground && !tTile.Type.lava && tTries > 0)
			{
				tTile = Toolbox.getRandomTileWithinDistance(pActor.current_tile, 100);
				tTries--;
			}
			if (!tTile.Type.ground && !tTile.Type.lava && BehaviourActionBase<Actor>.world.islands_calculator.getRandomIslandGround(true) != null)
			{
				Span<Vector2Int> tTiles = new Span<Vector2Int>(stackalloc byte[checked(unchecked((UIntPtr)8) * (UIntPtr)sizeof(Vector2Int))], 8);
				for (int i = 0; i < 8; i++)
				{
					*tTiles[i] = BehaviourActionBase<Actor>.world.islands_calculator.tryGetRandomGround().pos;
				}
				Vector2Int tTilePos = Toolbox.getClosestTile(tTiles, pActor.current_tile);
				tTile = BehaviourActionBase<Actor>.world.GetTileSimple(tTilePos.x, tTilePos.y);
			}
			pActor.beh_tile_target = tTile;
			return BehResult.Continue;
		}
	}
}
