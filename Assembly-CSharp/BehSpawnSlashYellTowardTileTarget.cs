using System;
using ai.behaviours;

// Token: 0x020003BE RID: 958
public class BehSpawnSlashYellTowardTileTarget : BehaviourActionActor
{
	// Token: 0x06002239 RID: 8761 RVA: 0x0012034C File Offset: 0x0011E54C
	protected override void setupErrorChecks()
	{
		base.setupErrorChecks();
		this.null_check_tile_target = true;
	}

	// Token: 0x0600223A RID: 8762 RVA: 0x0012035B File Offset: 0x0011E55B
	public override BehResult execute(Actor pActor)
	{
		pActor.spawnSlashYell(pActor.beh_tile_target.pos);
		return BehResult.Continue;
	}
}
