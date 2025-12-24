using System;

namespace ai.behaviours
{
	// Token: 0x02000894 RID: 2196
	public class BehCheckCure : BehaviourActionActor
	{
		// Token: 0x06004471 RID: 17521 RVA: 0x001CDF64 File Offset: 0x001CC164
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.current_tile.Type.ground)
			{
				return BehResult.Stop;
			}
			Actor tTarget = null;
			foreach (Actor tActor in Finder.getUnitsFromChunk(pActor.current_tile, 1, 0f, false))
			{
				if (ActorTool.canBeCuredFromTraitsOrStatus(tActor))
				{
					tTarget = tActor;
					break;
				}
			}
			if (tTarget == null)
			{
				return BehResult.Stop;
			}
			AttackAction action = AssetManager.spells.get("cast_cure").action;
			if (action != null)
			{
				action(pActor, tTarget, tTarget.current_tile);
			}
			pActor.doCastAnimation();
			return BehResult.Continue;
		}
	}
}
