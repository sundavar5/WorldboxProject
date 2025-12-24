using System;

// Token: 0x02000182 RID: 386
public class UnitHandToolLibrary : AssetLibrary<UnitHandToolAsset>
{
	// Token: 0x06000B8A RID: 2954 RVA: 0x000A5B18 File Offset: 0x000A3D18
	public override void init()
	{
		base.init();
		this.add(new UnitHandToolAsset
		{
			id = "flag",
			animated = true,
			colored = true
		});
		this.add(new UnitHandToolAsset
		{
			id = "axe"
		});
		this.add(new UnitHandToolAsset
		{
			id = "basket"
		});
		this.add(new UnitHandToolAsset
		{
			id = "book"
		});
		this.add(new UnitHandToolAsset
		{
			id = "bucket"
		});
		this.add(new UnitHandToolAsset
		{
			id = "coffee_cup",
			animated = true
		});
		this.add(new UnitHandToolAsset
		{
			id = "hammer"
		});
		this.add(new UnitHandToolAsset
		{
			id = "hoe"
		});
		this.add(new UnitHandToolAsset
		{
			id = "notebook"
		});
		this.add(new UnitHandToolAsset
		{
			id = "pickaxe"
		});
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x000A5C28 File Offset: 0x000A3E28
	public override void post_init()
	{
		base.post_init();
		foreach (UnitHandToolAsset tAsset in this.list)
		{
			if (string.IsNullOrEmpty(tAsset.path_gameplay_sprite))
			{
				tAsset.path_gameplay_sprite = "items/tools/tool_" + tAsset.id;
			}
		}
	}

	// Token: 0x06000B8C RID: 2956 RVA: 0x000A5CA0 File Offset: 0x000A3EA0
	public void loadSprites()
	{
		foreach (UnitHandToolAsset unitHandToolAsset in this.list)
		{
			unitHandToolAsset.gameplay_sprites = SpriteTextureLoader.getSpriteList(unitHandToolAsset.path_gameplay_sprite, false);
		}
	}
}
