using System;

namespace ai.behaviours
{
	// Token: 0x020008EF RID: 2287
	public class BehRandomSwim : BehaviourActionActor
	{
		// Token: 0x06004556 RID: 17750 RVA: 0x001D1F18 File Offset: 0x001D0118
		public override BehResult execute(Actor pActor)
		{
			BehaviourActionActor.possible_moves.Clear();
			foreach (WorldTile tTile in pActor.current_tile.neighboursAll)
			{
				if (tTile.Type.liquid)
				{
					BehaviourActionActor.possible_moves.Add(tTile);
				}
			}
			if (BehaviourActionActor.possible_moves.Count > 0)
			{
				WorldTile tTile2 = BehaviourActionActor.possible_moves.GetRandom<WorldTile>();
				BehaviourActionActor.possible_moves.Clear();
				pActor.moveTo(tTile2);
				pActor.setTileTarget(tTile2);
			}
			return BehResult.Continue;
		}
	}
}
