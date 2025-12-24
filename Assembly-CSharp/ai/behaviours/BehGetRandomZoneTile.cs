using System;

namespace ai.behaviours
{
	// Token: 0x020008DA RID: 2266
	public class BehGetRandomZoneTile : BehaviourActionActor
	{
		// Token: 0x0600451C RID: 17692 RVA: 0x001D0CB8 File Offset: 0x001CEEB8
		public override BehResult execute(Actor pActor)
		{
			MapChunk tChunk = Toolbox.getRandomChunkFromTile(pActor.current_tile);
			if (tChunk != null)
			{
				pActor.beh_tile_target = tChunk.tiles.GetRandom<WorldTile>();
			}
			return BehResult.Continue;
		}
	}
}
