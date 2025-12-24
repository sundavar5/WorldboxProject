using System;

// Token: 0x0200032F RID: 815
public class AntimatterBombEffect : BaseEffect
{
	// Token: 0x06001FA7 RID: 8103 RVA: 0x001119D0 File Offset: 0x0010FBD0
	private void Update()
	{
		World.world.startShake(0.03f, 0.01f, 0.3f, false, true);
		if (this.sprite_animation.currentFrameIndex >= 6 && !this.used)
		{
			this.used = true;
			World.world.applyForceOnTile(this.tile, 10, 0f, true, 1000, null, null, null, false);
			World.world.loopWithBrush(this.tile, Brush.get(11, "circ_"), new PowerActionWithID(this.tileAntimatter), null);
		}
	}

	// Token: 0x06001FA8 RID: 8104 RVA: 0x00111A60 File Offset: 0x0010FC60
	public bool tileAntimatter(WorldTile pTile, string pPowerID)
	{
		TileType tToChange = TileLibrary.pit_deep_ocean;
		bool tSkipTerraform = false;
		if (!MapAction.checkTileDamageGaiaCovenant(pTile, true))
		{
			tToChange = null;
			tSkipTerraform = true;
		}
		MapAction.terraformMain(pTile, tToChange, TerraformLibrary.destroy_no_flash, tSkipTerraform);
		return true;
	}

	// Token: 0x06001FA9 RID: 8105 RVA: 0x00111A90 File Offset: 0x0010FC90
	internal override void spawnOnTile(WorldTile pTile)
	{
		this.tile = pTile;
		this.used = false;
		this.prepare(pTile, 0.5f);
		base.resetAnim();
	}

	// Token: 0x0400170A RID: 5898
	private bool used;
}
