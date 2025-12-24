using System;

namespace ai.behaviours
{
	// Token: 0x0200095D RID: 2397
	public class BehFingerGoToCircleTarget : BehFinger
	{
		// Token: 0x06004673 RID: 18035 RVA: 0x001DE0B4 File Offset: 0x001DC2B4
		public BehFingerGoToCircleTarget(int pMinRange = 20, int pMaxRange = 25)
		{
			this._min_range = pMinRange;
			this._max_range = pMaxRange;
		}

		// Token: 0x06004674 RID: 18036 RVA: 0x001DE0CC File Offset: 0x001DC2CC
		public override BehResult execute(Actor pActor)
		{
			WorldTile tTile = pActor.current_tile;
			int tRange = Randy.randomInt(this._min_range, this._max_range);
			BehResult result;
			using (ListPool<WorldTile> tLeft = new ListPool<WorldTile>
			{
				BehaviourActionBase<Actor>.world.GetTile(tTile.x - tRange / 2, tTile.y + tRange / 2),
				BehaviourActionBase<Actor>.world.GetTile(tTile.x - tRange, tTile.y),
				BehaviourActionBase<Actor>.world.GetTile(tTile.x - tRange / 2, tTile.y - tRange / 2)
			})
			{
				using (ListPool<WorldTile> tRight = new ListPool<WorldTile>
				{
					BehaviourActionBase<Actor>.world.GetTile(tTile.x + tRange / 2, tTile.y + tRange / 2),
					BehaviourActionBase<Actor>.world.GetTile(tTile.x + tRange, tTile.y),
					BehaviourActionBase<Actor>.world.GetTile(tTile.x + tRange / 2, tTile.y - tRange / 2)
				})
				{
					using (ListPool<WorldTile> tTop = new ListPool<WorldTile>
					{
						BehaviourActionBase<Actor>.world.GetTile(tTile.x - tRange / 2, tTile.y + tRange / 2),
						BehaviourActionBase<Actor>.world.GetTile(tTile.x, tTile.y + tRange),
						BehaviourActionBase<Actor>.world.GetTile(tTile.x + tRange / 2, tTile.y + tRange / 2)
					})
					{
						using (ListPool<WorldTile> tBottom = new ListPool<WorldTile>
						{
							BehaviourActionBase<Actor>.world.GetTile(tTile.x - tRange / 2, tTile.y - tRange / 2),
							BehaviourActionBase<Actor>.world.GetTile(tTile.x, tTile.y - tRange),
							BehaviourActionBase<Actor>.world.GetTile(tTile.x + tRange / 2, tTile.y - tRange / 2)
						})
						{
							using (ListPool<ListPool<WorldTile>> tTiles = new ListPool<ListPool<WorldTile>>
							{
								tLeft,
								tRight,
								tTop,
								tBottom
							})
							{
								tTiles.RemoveAll((ListPool<WorldTile> tList) => tList.Contains(null));
								if (tTiles.Count == 0)
								{
									result = BehResult.Stop;
								}
								else
								{
									ListPool<WorldTile> tTargetTiles = tTiles.GetRandom<ListPool<WorldTile>>();
									if (ActorMove.goToCurved(pActor, new WorldTile[]
									{
										pActor.current_tile,
										tTargetTiles[0],
										tTargetTiles[1],
										tTargetTiles[2],
										pActor.current_tile
									}) == ExecuteEvent.False)
									{
										result = BehResult.Stop;
									}
									else
									{
										result = BehResult.Continue;
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x040031EF RID: 12783
		private int _min_range;

		// Token: 0x040031F0 RID: 12784
		private int _max_range;
	}
}
