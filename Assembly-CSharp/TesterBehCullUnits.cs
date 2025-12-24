using System;
using ai.behaviours;

// Token: 0x020004BD RID: 1213
public class TesterBehCullUnits : BehaviourActionTester
{
	// Token: 0x060029CD RID: 10701 RVA: 0x00149AB8 File Offset: 0x00147CB8
	public TesterBehCullUnits(string pActorAssetId)
	{
		this._actor_asset_id = pActorAssetId;
	}

	// Token: 0x060029CE RID: 10702 RVA: 0x00149AC8 File Offset: 0x00147CC8
	public override BehResult execute(AutoTesterBot pObject)
	{
		foreach (Actor tUnit in AssetManager.actor_library.get(this._actor_asset_id).units)
		{
			if (!tUnit.isRekt() && !Randy.randomChance(0.1f))
			{
				tUnit.getHit(10000f, false, AttackType.Divine, null, true, false, true);
			}
		}
		return base.execute(pObject);
	}

	// Token: 0x04001F2D RID: 7981
	private string _actor_asset_id;
}
