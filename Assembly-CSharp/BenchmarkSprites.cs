using System;

// Token: 0x0200050A RID: 1290
public class BenchmarkSprites
{
	// Token: 0x06002A9B RID: 10907 RVA: 0x00153380 File Offset: 0x00151580
	public static void start()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		if (!SelectedUnit.isSet())
		{
			return;
		}
		Actor tActor = SelectedUnit.unit;
		if (!tActor.is_visible)
		{
			return;
		}
		int tCountTotal = 100;
		Bench.bench("sprites_old", "sprites_test", false);
		for (int i = 0; i < tCountTotal; i++)
		{
			DynamicSpriteCreator.createNewSpriteUnit(tActor.frame_data, tActor.calculateMainSprite(), tActor.cached_sprite_head, tActor.kingdom.getColor(), tActor.asset, tActor.data.phenotype_index, tActor.data.phenotype_shade, UnitTextureAtlasID.Units);
		}
		Bench.benchEnd("sprites_old", "sprites_test", true, (long)tCountTotal, false);
		Bench.bench("sprites_new", "sprites_test", false);
		for (int j = 0; j < tCountTotal; j++)
		{
			DynamicSpriteCreator.createNewSpriteUnit(tActor.frame_data, tActor.calculateMainSprite(), tActor.cached_sprite_head, tActor.kingdom.getColor(), tActor.asset, tActor.data.phenotype_index, tActor.data.phenotype_shade, UnitTextureAtlasID.Units);
		}
		Bench.benchEnd("sprites_new", "sprites_test", true, (long)tCountTotal, false);
	}
}
