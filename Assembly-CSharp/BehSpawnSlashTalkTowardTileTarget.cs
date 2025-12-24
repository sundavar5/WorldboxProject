using System;
using ai.behaviours;

// Token: 0x020003BD RID: 957
public class BehSpawnSlashTalkTowardTileTarget : BehaviourActionActor
{
	// Token: 0x06002236 RID: 8758 RVA: 0x0012031C File Offset: 0x0011E51C
	protected override void setupErrorChecks()
	{
		base.setupErrorChecks();
		this.null_check_tile_target = true;
	}

	// Token: 0x06002237 RID: 8759 RVA: 0x0012032B File Offset: 0x0011E52B
	public override BehResult execute(Actor pActor)
	{
		pActor.spawnSlashTalk(pActor.beh_tile_target.pos);
		return BehResult.Continue;
	}
}
