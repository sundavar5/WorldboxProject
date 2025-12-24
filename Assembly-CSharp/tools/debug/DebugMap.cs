using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace tools.debug
{
	// Token: 0x02000862 RID: 2146
	public class DebugMap
	{
		// Token: 0x06004339 RID: 17209 RVA: 0x001C8890 File Offset: 0x001C6A90
		public static void makeDebugMap()
		{
			DebugMap.createDebugButtons();
			WorldTile[] tiles_list = World.world.tiles_list;
			for (int k = 0; k < tiles_list.Length; k++)
			{
				MapAction.terraformTile(tiles_list[k], TileLibrary.soil_low, TopTileLibrary.grass_low, TerraformLibrary.destroy, false);
			}
			int xx = 10;
			int yy = 10;
			int i = 0;
			int j = AssetManager.buildings.list.Count;
			while (i < j)
			{
				BuildingAsset tAsset = AssetManager.buildings.list[i];
				if (tAsset.id.Contains("!"))
				{
					i++;
				}
				else
				{
					i++;
					xx += 20;
					if (xx > 200)
					{
						xx = 10;
						yy += 10;
					}
					Building tBuilding = World.world.buildings.addBuilding(tAsset, World.world.GetTile(xx, yy), false, false, BuildPlacingType.New);
					tBuilding.kingdom = World.world.kingdoms_wild.get("nature");
					tBuilding.updateBuild(10000);
					if (tBuilding.asset.docks)
					{
						foreach (WorldTile pTile in tBuilding.tiles)
						{
							MapAction.terraformMain(pTile, TileLibrary.shallow_waters, TerraformLibrary.flash, false);
						}
					}
				}
			}
			Config.paused = true;
		}

		// Token: 0x0600433A RID: 17210 RVA: 0x001C89F4 File Offset: 0x001C6BF4
		private static void debugConstructionZone()
		{
			foreach (Building building in World.world.buildings)
			{
				building.debugConstructions();
			}
		}

		// Token: 0x0600433B RID: 17211 RVA: 0x001C8A44 File Offset: 0x001C6C44
		private static void debugNextFrame()
		{
		}

		// Token: 0x0600433C RID: 17212 RVA: 0x001C8A46 File Offset: 0x001C6C46
		private static void debugRuins()
		{
		}

		// Token: 0x0600433D RID: 17213 RVA: 0x001C8A48 File Offset: 0x001C6C48
		public static void createDebugButtons()
		{
			Button button = DebugMap.makeNewButton("debug_next_frame", "iconBuildings");
			button.onClick.AddListener(new UnityAction(DebugMap.debugNextFrame));
			button.GetComponent<RectTransform>().anchoredPosition = new Vector2(50f, -20f);
			Button button2 = DebugMap.makeNewButton("debug_ruins", "iconDemolish");
			button2.onClick.AddListener(new UnityAction(DebugMap.debugRuins));
			button2.GetComponent<RectTransform>().anchoredPosition = new Vector2(100f, -20f);
			Button button3 = DebugMap.makeNewButton("debug_construction", "iconBucket");
			button3.onClick.AddListener(new UnityAction(DebugMap.debugConstructionZone));
			button3.GetComponent<RectTransform>().anchoredPosition = new Vector2(150f, -20f);
		}

		// Token: 0x0600433E RID: 17214 RVA: 0x001C8B14 File Offset: 0x001C6D14
		private static Button makeNewButton(string pName, string pIcon)
		{
			Button button = Object.Instantiate<Button>((Button)Resources.Load("ui/PrefabWorldBoxButton", typeof(Button)), World.world.canvas.transform);
			button.transform.name = pName;
			button.transform.parent = World.world.canvas.transform;
			Sprite tSprite = (Sprite)Resources.Load("ui/Icons/" + pIcon, typeof(Sprite));
			button.transform.Find("Icon").GetComponent<Image>().sprite = tSprite;
			return button;
		}
	}
}
