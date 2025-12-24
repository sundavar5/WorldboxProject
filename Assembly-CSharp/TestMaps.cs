using System;
using System.IO;
using UnityEngine;

// Token: 0x02000599 RID: 1433
public static class TestMaps
{
	// Token: 0x06002FE7 RID: 12263 RVA: 0x00173CE4 File Offset: 0x00171EE4
	public static void init()
	{
		if (TestMaps._initialized)
		{
			return;
		}
		TestMaps._initialized = true;
		using (ListPool<string> tTestMaps = new ListPool<string>())
		{
			string[] tFiles = Directory.GetFiles("test_maps", "*.wbox", SearchOption.AllDirectories);
			tTestMaps.AddRange(tFiles);
			string[] tOldFiles = Directory.GetFiles("test_maps", "*.json", SearchOption.AllDirectories);
			tTestMaps.AddRange(tOldFiles);
			tTestMaps.RemoveAll((string p) => p.Contains("debug"));
			TestMaps._maps = tTestMaps.ToArray<string>();
			TestMaps._index = Toolbox.loopIndex(Randy.randomInt(0, TestMaps._maps.Length * 100), TestMaps._maps.Length);
		}
	}

	// Token: 0x06002FE8 RID: 12264 RVA: 0x00173DA4 File Offset: 0x00171FA4
	public static void loadMap(int pIndex)
	{
		string tMap = TestMaps._maps[pIndex];
		Debug.Log(string.Format("Loading map: {0} ({1}/{2})", tMap, TestMaps._index + 1, TestMaps._maps.Length));
		string tFolder = Path.GetDirectoryName(tMap);
		tFolder = SaveManager.folderPath(tFolder);
		World.world.save_manager.loadWorld(tFolder, false);
	}

	// Token: 0x06002FE9 RID: 12265 RVA: 0x00173E00 File Offset: 0x00172000
	public static void loadNextMap()
	{
		TestMaps.init();
		TestMaps._index = Toolbox.loopIndex(TestMaps._index + 1, TestMaps._maps.Length);
		TestMaps.loadMap(TestMaps._index);
	}

	// Token: 0x06002FEA RID: 12266 RVA: 0x00173E29 File Offset: 0x00172029
	public static void loadPrevMap()
	{
		TestMaps.init();
		TestMaps._index = Toolbox.loopIndex(TestMaps._index - 1, TestMaps._maps.Length);
		TestMaps.loadMap(TestMaps._index);
	}

	// Token: 0x04002401 RID: 9217
	private static bool _initialized = false;

	// Token: 0x04002402 RID: 9218
	private static string[] _maps;

	// Token: 0x04002403 RID: 9219
	private static int _index = -1;
}
