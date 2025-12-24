using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000EE RID: 238
[Serializable]
public class DynamicSpritesAsset : Asset
{
	// Token: 0x06000710 RID: 1808 RVA: 0x00069625 File Offset: 0x00067825
	public override void create()
	{
		base.create();
		if (this._atlas == null)
		{
			this._atlas = new UnitSpriteConstructorAtlas(this.atlas_id, this.big_atlas);
		}
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x0006964C File Offset: 0x0006784C
	public void resetAtlas()
	{
		this.clear();
		this._atlas.setBigAtlas(this.big_atlas);
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x00069665 File Offset: 0x00067865
	public bool hasSprite(long pID)
	{
		return this._dictionary_sprites.ContainsKey(pID);
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x00069673 File Offset: 0x00067873
	public void addSprite(long pHashCode, Sprite pSprite)
	{
		this._dictionary_sprites[pHashCode] = pSprite;
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x00069684 File Offset: 0x00067884
	public Sprite getSprite(long pID)
	{
		Sprite tResult;
		this._dictionary_sprites.TryGetValue(pID, out tResult);
		return tResult;
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x000696A1 File Offset: 0x000678A1
	public UnitSpriteConstructorAtlas getAtlas()
	{
		return this._atlas;
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x000696A9 File Offset: 0x000678A9
	public void checkAtlasDirty()
	{
		this._atlas.checkDirty();
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x000696B6 File Offset: 0x000678B6
	public void clear()
	{
		this._dictionary_sprites.Clear();
		this._atlas.clear();
		this.checkAtlasDirty();
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x000696D4 File Offset: 0x000678D4
	public int countSprites()
	{
		return this._dictionary_sprites.Count;
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x000696E1 File Offset: 0x000678E1
	public int countTextures()
	{
		return this._atlas.textures.Count;
	}

	// Token: 0x04000790 RID: 1936
	public bool big_atlas = true;

	// Token: 0x04000791 RID: 1937
	public bool check_wobbly_setting;

	// Token: 0x04000792 RID: 1938
	public UnitTextureAtlasID atlas_id;

	// Token: 0x04000793 RID: 1939
	public string export_folder_path;

	// Token: 0x04000794 RID: 1940
	public bool buildings;

	// Token: 0x04000795 RID: 1941
	private Dictionary<long, Sprite> _dictionary_sprites = new Dictionary<long, Sprite>();

	// Token: 0x04000796 RID: 1942
	private UnitSpriteConstructorAtlas _atlas;
}
