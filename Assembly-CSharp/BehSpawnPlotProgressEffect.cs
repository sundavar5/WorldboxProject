using System;
using ai.behaviours;
using UnityEngine;

// Token: 0x020003A3 RID: 931
public class BehSpawnPlotProgressEffect : BehaviourActionActor
{
	// Token: 0x060021E3 RID: 8675 RVA: 0x0011DFD3 File Offset: 0x0011C1D3
	public BehSpawnPlotProgressEffect(int pAmount = 1)
	{
		this._amount = pAmount;
	}

	// Token: 0x060021E4 RID: 8676 RVA: 0x0011DFE4 File Offset: 0x0011C1E4
	public override BehResult execute(Actor pActor)
	{
		TileZone zone = pActor.current_tile.zone;
		for (int i = 0; i < this._amount; i++)
		{
			Vector3 tPos = pActor.current_position;
			tPos.y += 5f * pActor.actor_scale;
			tPos.y += Randy.randomFloat(-pActor.actor_scale * 3f, pActor.actor_scale * 3f);
			tPos.x += Randy.randomFloat(-pActor.actor_scale * 2f, pActor.actor_scale * 2f);
			EffectsLibrary.spawnAt("fx_plot_progress", tPos, pActor.actor_scale * 0.8f) == null;
		}
		return BehResult.Continue;
	}

	// Token: 0x040018EA RID: 6378
	private int _amount;
}
