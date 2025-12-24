using System;

namespace ai.behaviours
{
	// Token: 0x02000958 RID: 2392
	public class BehFingerDrawAction : BehFinger
	{
		// Token: 0x06004662 RID: 18018 RVA: 0x001DD2B5 File Offset: 0x001DB4B5
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			if (this.check_target_tile_in_target_tiles)
			{
				this.null_check_tile_target = true;
			}
		}

		// Token: 0x06004663 RID: 18019 RVA: 0x001DD2CC File Offset: 0x001DB4CC
		public override bool errorsFound(Actor pActor)
		{
			if (base.errorsFound(pActor))
			{
				return true;
			}
			this.finger = (pActor.children_special[0] as GodFinger);
			if (this.check_has_target_tiles && this.finger.target_tiles.Count == 0)
			{
				return true;
			}
			if (this.check_current_tile_in_target_tiles)
			{
				pActor.findCurrentTile(false);
				if (!this.finger.target_tiles.Contains(pActor.current_tile))
				{
					bool tAnyClose = false;
					if (pActor.beh_tile_target != null && Toolbox.DistTile(pActor.current_tile, pActor.beh_tile_target) < 6f)
					{
						tAnyClose = true;
					}
					else
					{
						foreach (WorldTile tNeighbour in pActor.current_tile.neighboursAll)
						{
							if (this.finger.target_tiles.Contains(tNeighbour))
							{
								tAnyClose = true;
								break;
							}
						}
					}
					if (!tAnyClose)
					{
						return true;
					}
				}
			}
			return this.check_target_tile_in_target_tiles && !this.finger.target_tiles.Contains(pActor.beh_tile_target);
		}

		// Token: 0x040031E6 RID: 12774
		public bool check_has_target_tiles = true;

		// Token: 0x040031E7 RID: 12775
		public bool check_current_tile_in_target_tiles = true;

		// Token: 0x040031E8 RID: 12776
		public bool check_target_tile_in_target_tiles = true;
	}
}
