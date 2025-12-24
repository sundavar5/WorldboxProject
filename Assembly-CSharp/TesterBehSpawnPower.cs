using System;
using ai.behaviours;
using UnityEngine;

// Token: 0x020004E1 RID: 1249
public class TesterBehSpawnPower : BehaviourActionTester
{
	// Token: 0x06002A18 RID: 10776 RVA: 0x0014B366 File Offset: 0x00149566
	public TesterBehSpawnPower(string pPower = null)
	{
		this._power = pPower;
	}

	// Token: 0x06002A19 RID: 10777 RVA: 0x0014B378 File Offset: 0x00149578
	public override BehResult execute(AutoTesterBot pObject)
	{
		string tPowerId = this._power;
		int tX = Randy.randomInt(0, MapBox.width);
		int tY = Randy.randomInt(0, MapBox.height);
		if (!AssetManager.powers.dict.ContainsKey(tPowerId))
		{
			Debug.LogError("TESTER ERROR... " + tPowerId);
			return BehResult.Continue;
		}
		GodPower tPower = AssetManager.powers.get(tPowerId);
		string current_brush = Config.current_brush;
		Config.current_brush = Brush.getRandom();
		pObject.debugString = "rand_power_" + tPowerId;
		BehaviourActionBase<AutoTesterBot>.world.player_control.clickedFinal(new Vector2Int(tX, tY), tPower, true);
		Config.current_brush = current_brush;
		return base.execute(pObject);
	}

	// Token: 0x04001F69 RID: 8041
	protected string _power;
}
