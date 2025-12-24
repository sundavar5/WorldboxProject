using System;

// Token: 0x02000505 RID: 1285
public class BenchmarkFieldAccess
{
	// Token: 0x06002A8D RID: 10893 RVA: 0x00151984 File Offset: 0x0014FB84
	public static void start()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		int tCountTotal = 100000;
		Bench.bench("field_acess_test", "field_acess_total", false);
		Bench.bench("field_access", "field_acess_test", false);
		int tResult = 0;
		for (int i = 0; i < tCountTotal; i++)
		{
			tResult += World.world.tiles_list.Length;
		}
		Bench.benchEnd("field_access", "field_acess_test", true, (long)tCountTotal, false);
		Bench.bench("temp_var", "field_acess_test", false);
		tResult = 0;
		MapBox tMapBox = World.world;
		for (int j = 0; j < tCountTotal; j++)
		{
			tResult += tMapBox.tiles_list.Length;
		}
		Bench.benchEnd("temp_var", "field_acess_test", true, (long)tCountTotal, false);
		Bench.bench("temp_var_2", "field_acess_test", false);
		tResult = 0;
		WorldTile[] tList = World.world.tiles_list;
		for (int k = 0; k < tCountTotal; k++)
		{
			int tLen = tList.Length;
			tResult += tLen;
		}
		Bench.benchEnd("temp_var_2", "field_acess_test", true, (long)tCountTotal, false);
		Bench.bench("result_len", "field_acess_test", false);
		tResult = 0;
		int tResultLen = World.world.tiles_list.Length;
		for (int l = 0; l < tCountTotal; l++)
		{
			tResult += tResultLen;
		}
		Bench.benchEnd("result_len", "field_acess_test", true, (long)tCountTotal, false);
		Bench.benchEnd("field_acess_test", "field_acess_total", false, 0L, false);
	}
}
