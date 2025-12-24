using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x02000400 RID: 1024
public class Earthquake : BaseMapObject
{
	// Token: 0x06002365 RID: 9061 RVA: 0x00126ECC File Offset: 0x001250CC
	public void Awake()
	{
		Earthquake._instance = this;
	}

	// Token: 0x06002366 RID: 9062 RVA: 0x00126ED4 File Offset: 0x001250D4
	public static void startQuake(WorldTile pTile, EarthquakeType pType = EarthquakeType.RandomPower)
	{
		Earthquake._instance.spawnQuake(pTile, pType);
	}

	// Token: 0x06002367 RID: 9063 RVA: 0x00126EE4 File Offset: 0x001250E4
	private void spawnQuake(WorldTile pTile, EarthquakeType pType)
	{
		if (Earthquake.isQuakeActive())
		{
			return;
		}
		MusicBox.playSound("event:/SFX/NATURE/EarthQuake", pTile, false, false);
		this._type = pType;
		this._quake_active = true;
		List<PrintTemplate> tQuakePrints = PrintLibrary.getQuakes();
		this._current_print_index++;
		if (this._current_print_index >= tQuakePrints.Count)
		{
			tQuakePrints.Shuffle<PrintTemplate>();
			this._current_print_index = 0;
		}
		this._current_print = tQuakePrints[this._current_print_index];
		this._current_print.steps.Shuffle<PrintStep>();
		this._print_tile_origin = pTile;
		this._print_tick = 0;
		if (pType == EarthquakeType.RandomPower)
		{
			if (Randy.randomChance(0.5f))
			{
				this._type = EarthquakeType.BigDecrease;
				return;
			}
			this._type = EarthquakeType.BigIncrease;
		}
	}

	// Token: 0x06002368 RID: 9064 RVA: 0x00126F94 File Offset: 0x00125194
	private void Update()
	{
		if (!Earthquake.isQuakeActive())
		{
			return;
		}
		if (this._timer > 0f)
		{
			this._timer -= World.world.elapsed;
			return;
		}
		this._timer = 0.05f;
		for (int i = 0; i < 300; i++)
		{
			if (this._print_tick >= this._current_print.steps.Length)
			{
				this.endQuake();
				break;
			}
			PrintStep tStep = this._current_print.steps[this._print_tick];
			this._print_tick++;
			WorldTile tTile = World.world.GetTile(this._print_tile_origin.pos.x + tStep.x, this._print_tile_origin.pos.y + tStep.y);
			if (tTile != null)
			{
				this.tileAction(tTile);
				if (this._print_tick >= this._current_print.steps.Length)
				{
					this.endQuake();
					break;
				}
			}
		}
		World.world.startShake(0.3f, 0.01f, 0.23f, true, true);
	}

	// Token: 0x06002369 RID: 9065 RVA: 0x001270B8 File Offset: 0x001252B8
	private void tileAction(WorldTile pTile)
	{
		if (MapAction.checkTileDamageGaiaCovenant(pTile, true))
		{
			switch (this._type)
			{
			case EarthquakeType.BigIncrease:
				MapAction.increaseTile(pTile, true, "earthquake");
				break;
			case EarthquakeType.BigDecrease:
				MapAction.decreaseTile(pTile, true, "earthquake");
				break;
			case EarthquakeType.SmallDisaster:
				MapAction.terraformMain(pTile, pTile.main_type, AssetManager.terraform.get("earthquake_disaster"), false);
				break;
			}
		}
		pTile.removeBurn();
		pTile.doUnits(new Action<Actor>(this.unitAction));
	}

	// Token: 0x0600236A RID: 9066 RVA: 0x0012713B File Offset: 0x0012533B
	private void unitAction(Actor pActor)
	{
		if (Randy.randomBool())
		{
			pActor.makeConfused(-1f, false);
			return;
		}
		pActor.applyRandomForce(1.5f, 2f);
	}

	// Token: 0x0600236B RID: 9067 RVA: 0x00127161 File Offset: 0x00125361
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool isQuakeActive()
	{
		return Earthquake._instance._quake_active;
	}

	// Token: 0x0600236C RID: 9068 RVA: 0x0012716D File Offset: 0x0012536D
	private void endQuake()
	{
		this._quake_active = false;
	}

	// Token: 0x04001998 RID: 6552
	private PrintTemplate _current_print;

	// Token: 0x04001999 RID: 6553
	private int _print_tick;

	// Token: 0x0400199A RID: 6554
	private WorldTile _print_tile_origin;

	// Token: 0x0400199B RID: 6555
	private float _timer;

	// Token: 0x0400199C RID: 6556
	private const float INTERVAL = 0.05f;

	// Token: 0x0400199D RID: 6557
	private bool _quake_active;

	// Token: 0x0400199E RID: 6558
	private EarthquakeType _type;

	// Token: 0x0400199F RID: 6559
	private int _current_print_index;

	// Token: 0x040019A0 RID: 6560
	private static Earthquake _instance;
}
