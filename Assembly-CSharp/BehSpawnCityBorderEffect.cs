using System;
using ai.behaviours;

// Token: 0x020003A1 RID: 929
public class BehSpawnCityBorderEffect : BehaviourActionActor
{
	// Token: 0x060021DF RID: 8671 RVA: 0x0011DF0A File Offset: 0x0011C10A
	public BehSpawnCityBorderEffect(int pAmount = 1)
	{
		this._amount = pAmount;
	}

	// Token: 0x060021E0 RID: 8672 RVA: 0x0011DF1C File Offset: 0x0011C11C
	public override BehResult execute(Actor pActor)
	{
		TileZone zone = pActor.current_tile.zone;
		for (int i = 0; i < this._amount; i++)
		{
			WorldTile tRandomTile = pActor.current_tile.neighbours.GetRandom<WorldTile>();
			EffectsLibrary.spawnAt("fx_new_border", tRandomTile.posV, 0.25f);
		}
		return BehResult.Continue;
	}

	// Token: 0x040018E8 RID: 6376
	private int _amount;
}
