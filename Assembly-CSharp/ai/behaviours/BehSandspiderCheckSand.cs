using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x02000919 RID: 2329
	public class BehSandspiderCheckSand : BehaviourActionActor
	{
		// Token: 0x060045C4 RID: 17860 RVA: 0x001D3988 File Offset: 0x001D1B88
		public override BehResult execute(Actor pActor)
		{
			WorldTile targetTile = pActor.beh_tile_target;
			if (targetTile == null)
			{
				return BehResult.Continue;
			}
			bool changed_direction;
			pActor.data.get("changed_direction", out changed_direction, false);
			if (!changed_direction && targetTile.Type.IsType("sand"))
			{
				int direction_index;
				pActor.data.get("direction", out direction_index, 0);
				int new_direction_index = BehSandspiderCheckSand.getNewDirectionIndex(direction_index);
				pActor.data.set("direction", new_direction_index);
				pActor.data.set("changed_direction", true);
				return BehResult.RestartTask;
			}
			return BehResult.Continue;
		}

		// Token: 0x060045C5 RID: 17861 RVA: 0x001D3A0C File Offset: 0x001D1C0C
		private static int getNewDirectionIndex(int pOldIndex)
		{
			BehSandspiderCheckSand._list_directions.Clear();
			for (int i = 0; i < Toolbox.directions.Length; i++)
			{
				if (i != pOldIndex)
				{
					BehSandspiderCheckSand._list_directions.Add(i);
				}
			}
			return BehSandspiderCheckSand._list_directions.GetRandom<int>();
		}

		// Token: 0x040031C0 RID: 12736
		private static List<int> _list_directions = new List<int>(3);
	}
}
