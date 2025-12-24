using System;
using ai.behaviours;

// Token: 0x020003A2 RID: 930
public class BehSpawnHmmEffect : BehaviourActionActor
{
	// Token: 0x060021E1 RID: 8673 RVA: 0x0011DF6E File Offset: 0x0011C16E
	public BehSpawnHmmEffect(int pAmount = 1)
	{
		this._amount = pAmount;
	}

	// Token: 0x060021E2 RID: 8674 RVA: 0x0011DF80 File Offset: 0x0011C180
	public override BehResult execute(Actor pActor)
	{
		TileZone zone = pActor.current_tile.zone;
		for (int i = 0; i < this._amount; i++)
		{
			WorldTile tRandomTile = pActor.current_tile.neighbours.GetRandom<WorldTile>();
			EffectsLibrary.spawnAt("fx_hmm", tRandomTile.posV, pActor.actor_scale);
		}
		return BehResult.Continue;
	}

	// Token: 0x040018E9 RID: 6377
	private int _amount;
}
