using System;
using UnityEngine;

namespace ai.behaviours
{
	// Token: 0x02000930 RID: 2352
	public class BehBoatFishing : BehaviourActionActor
	{
		// Token: 0x060045FC RID: 17916 RVA: 0x001D42E0 File Offset: 0x001D24E0
		public override BehResult execute(Actor pActor)
		{
			this.spawnFishnet(pActor);
			return BehResult.Continue;
		}

		// Token: 0x060045FD RID: 17917 RVA: 0x001D42EC File Offset: 0x001D24EC
		public void spawnFishnet(Actor pActor)
		{
			if (!MapBox.isRenderGameplay())
			{
				return;
			}
			Vector2 tVec = Randy.randomPointOnCircle(3f, 4f);
			WorldTile tTile = BehaviourActionBase<Actor>.world.GetTile(pActor.current_tile.pos.x + (int)tVec.x, pActor.current_tile.pos.y + (int)tVec.y);
			if (tTile == null)
			{
				return;
			}
			if (!tTile.Type.ocean)
			{
				return;
			}
			EffectsLibrary.spawnAtTile("fx_fishnet", tTile, pActor.asset.base_stats["scale"]);
		}
	}
}
