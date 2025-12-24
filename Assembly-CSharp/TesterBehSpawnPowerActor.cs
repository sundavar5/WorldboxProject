using System;
using ai.behaviours;

// Token: 0x020004E2 RID: 1250
public class TesterBehSpawnPowerActor : TesterBehSpawnPower
{
	// Token: 0x06002A1A RID: 10778 RVA: 0x0014B418 File Offset: 0x00149618
	public TesterBehSpawnPowerActor(string pPower, int pLimit = -1) : base(null)
	{
		this._power = pPower;
		this._limit = pLimit;
	}

	// Token: 0x06002A1B RID: 10779 RVA: 0x0014B430 File Offset: 0x00149630
	public override BehResult execute(AutoTesterBot pObject)
	{
		if (this._limit > -1)
		{
			GodPower tPower = AssetManager.powers.get(this._power);
			if (AssetManager.actor_library.get(tPower.actor_asset_id).units.Count >= this._limit)
			{
				return BehResult.Continue;
			}
		}
		return base.execute(pObject);
	}

	// Token: 0x04001F6A RID: 8042
	private int _limit;
}
