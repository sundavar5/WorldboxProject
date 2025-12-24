using System;
using UnityEngine;

// Token: 0x02000036 RID: 54
public class CloudLibrary : AssetLibrary<CloudAsset>
{
	// Token: 0x0600023F RID: 575 RVA: 0x00014D70 File Offset: 0x00012F70
	public override void init()
	{
		base.init();
		this.add(new CloudAsset
		{
			id = "cloud_rain",
			color_hex = "#5D728C",
			drop_id = "rain",
			cloud_action_1 = new CloudAction(CloudLibrary.dropAction),
			path_sprites = CloudLibrary._sprites_big,
			speed_max = 4f
		});
		this.add(new CloudAsset
		{
			id = "cloud_rage",
			color_hex = "#FF3030",
			drop_id = "rage",
			cloud_action_1 = new CloudAction(CloudLibrary.dropAction),
			path_sprites = CloudLibrary._sprites_big,
			speed_max = 4f,
			considered_disaster = true,
			draw_light_area = true
		});
		this.add(new CloudAsset
		{
			id = "cloud_lightning",
			color_hex = "#445366",
			drop_id = "rain",
			cloud_action_1 = new CloudAction(CloudLibrary.dropAction),
			cloud_action_2 = new CloudAction(CloudLibrary.spawnLightning),
			path_sprites = CloudLibrary._sprites_big,
			speed_max = 4f,
			considered_disaster = true
		});
		this.add(new CloudAsset
		{
			id = "cloud_acid",
			color_hex = "#17D41C",
			drop_id = "acid",
			cloud_action_1 = new CloudAction(CloudLibrary.dropAction),
			path_sprites = CloudLibrary._sprites_big,
			speed_max = 4f,
			considered_disaster = true
		});
		this.add(new CloudAsset
		{
			id = "cloud_lava",
			color_hex = "#D17119",
			drop_id = "lava",
			cloud_action_1 = new CloudAction(CloudLibrary.dropAction),
			path_sprites = CloudLibrary._sprites_big,
			speed_max = 3f,
			considered_disaster = true,
			draw_light_area = true
		});
		this.add(new CloudAsset
		{
			id = "cloud_fire",
			color_hex = "#D14219",
			drop_id = "fire",
			cloud_action_1 = new CloudAction(CloudLibrary.dropAction),
			path_sprites = CloudLibrary._sprites_big,
			speed_max = 3f,
			considered_disaster = true,
			draw_light_area = true
		});
		this.add(new CloudAsset
		{
			id = "cloud_snow",
			color_hex = "#B8FFFA",
			drop_id = "snow",
			cloud_action_1 = new CloudAction(CloudLibrary.dropAction),
			path_sprites = CloudLibrary._sprites_big,
			considered_disaster = true,
			speed_max = 4f
		});
		this.add(new CloudAsset
		{
			id = "cloud_ash",
			color_hex = "#C6B077",
			drop_id = "ash",
			cloud_action_1 = new CloudAction(CloudLibrary.dropAction),
			path_sprites = CloudLibrary._sprites_big,
			considered_disaster = true,
			speed_max = 4f
		});
		this.add(new CloudAsset
		{
			id = "cloud_magic",
			color_hex = "#C976CC",
			drop_id = "magic_rain",
			cloud_action_1 = new CloudAction(CloudLibrary.dropAction),
			path_sprites = CloudLibrary._sprites_small,
			considered_disaster = true,
			speed_max = 7f,
			draw_light_area = true
		});
		this.add(new CloudAsset
		{
			id = "cloud_normal",
			max_alpha = 0.5f,
			interval_action_1 = 2f,
			drop_id = "life_seed",
			cloud_action_1 = new CloudAction(CloudLibrary.dropAction),
			path_sprites = CloudLibrary._sprites_all,
			speed_min = 2f,
			normal_cloud = true
		});
	}

	// Token: 0x06000240 RID: 576 RVA: 0x00015150 File Offset: 0x00013350
	public override void linkAssets()
	{
		base.linkAssets();
		using (ListPool<Sprite> tSprites = new ListPool<Sprite>())
		{
			foreach (CloudAsset tAsset in this.list)
			{
				if (tAsset.color_hex != null)
				{
					tAsset.color = Toolbox.makeColor(tAsset.color_hex);
				}
				tSprites.Clear();
				foreach (string tPath in tAsset.path_sprites)
				{
					Sprite tSprite = SpriteTextureLoader.getSprite(tPath);
					if (tSprite == null)
					{
						BaseAssetLibrary.logAssetError("cloud sprite not found", tPath);
					}
					else
					{
						tSprites.Add(tSprite);
					}
				}
				tAsset.cached_sprites = tSprites.ToArray<Sprite>();
			}
		}
	}

	// Token: 0x06000241 RID: 577 RVA: 0x00015234 File Offset: 0x00013434
	public static void dropAction(Cloud pCloud)
	{
		if (pCloud.alive_time < 3f)
		{
			return;
		}
		float tWidth = pCloud.effect_texture_width;
		float tHeight = pCloud.effect_texture_height;
		int posX = (int)pCloud.transform.localPosition.x;
		int posY = (int)pCloud.transform.localPosition.y;
		posX += (int)Randy.randomFloat(-tWidth, tWidth);
		posY += (int)Randy.randomFloat(-tHeight + pCloud.spriteShadow.offset.y, tHeight + pCloud.spriteShadow.offset.y);
		WorldTile tTile = World.world.GetTile(posX, posY);
		if (tTile == null)
		{
			return;
		}
		World.world.drop_manager.spawn(tTile, pCloud.asset.drop_id, 10f, -1f, -1L);
	}

	// Token: 0x06000242 RID: 578 RVA: 0x000152F8 File Offset: 0x000134F8
	public static void spawnLightning(Cloud pCloud)
	{
		if (!Randy.randomChance(0.01f))
		{
			return;
		}
		int posX = (int)pCloud.transform.localPosition.x;
		int posY = (int)pCloud.transform.localPosition.y;
		float tWidth = pCloud.effect_texture_width;
		float tHeight = pCloud.effect_texture_height;
		posX += (int)Randy.randomFloat(tWidth * 0.5f, tWidth);
		posY += (int)Randy.randomFloat(-tHeight + pCloud.spriteShadow.offset.y, tHeight + pCloud.spriteShadow.offset.y);
		WorldTile tTile = World.world.GetTile(posX, posY);
		if (tTile != null)
		{
			if (Randy.randomBool())
			{
				MapBox.spawnLightningMedium(tTile, 0.15f, null);
				return;
			}
			MapBox.spawnLightningSmall(tTile, 0.15f, null);
		}
	}

	// Token: 0x040001F7 RID: 503
	private static string[] _sprites_small = new string[]
	{
		"effects/clouds/cloud_small_1",
		"effects/clouds/cloud_small_2"
	};

	// Token: 0x040001F8 RID: 504
	private static string[] _sprites_big = new string[]
	{
		"effects/clouds/cloud_big_1",
		"effects/clouds/cloud_big_2",
		"effects/clouds/cloud_big_3"
	};

	// Token: 0x040001F9 RID: 505
	private static string[] _sprites_all = new string[]
	{
		"effects/clouds/cloud_small_1",
		"effects/clouds/cloud_small_2",
		"effects/clouds/cloud_big_1",
		"effects/clouds/cloud_big_2",
		"effects/clouds/cloud_big_3"
	};
}
