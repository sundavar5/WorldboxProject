using System;

namespace ai.behaviours
{
	// Token: 0x02000961 RID: 2401
	public class BehUFOBeam : BehaviourActionActor
	{
		// Token: 0x0600467C RID: 18044 RVA: 0x001DE55A File Offset: 0x001DC75A
		public BehUFOBeam(bool pEnabled = false)
		{
			this.enabled = pEnabled;
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x001DE56C File Offset: 0x001DC76C
		public override BehResult execute(Actor pActor)
		{
			UFO tUFO = pActor.getActorComponent<UFO>();
			if (!this.enabled)
			{
				tUFO.hideBeam();
				return BehResult.Continue;
			}
			if (!tUFO.beamAnim.isOn)
			{
				tUFO.startBeam();
				return BehResult.RepeatStep;
			}
			if (tUFO.beamAnim.currentFrameIndex == 4)
			{
				for (int yy = 0; yy < 8; yy++)
				{
					for (int xx = 0; xx < 8; xx++)
					{
						WorldTile tTile = BehaviourActionBase<Actor>.world.GetTile(pActor.current_tile.pos.x + xx - 4, pActor.current_tile.pos.y + yy - 4);
						if (tTile != null && Toolbox.Dist(pActor.current_tile.pos.x, pActor.current_tile.pos.y, tTile.pos.x, tTile.pos.y) <= 4f)
						{
							MapAction.damageWorld(tTile, 0, AssetManager.terraform.get("ufo_attack"), pActor);
						}
					}
				}
			}
			if (tUFO.beamAnim.currentFrameIndex == tUFO.beamAnim.frames.Length - 1)
			{
				tUFO.hideBeam();
				return BehResult.Continue;
			}
			return BehResult.RepeatStep;
		}

		// Token: 0x040031F4 RID: 12788
		private bool enabled;
	}
}
