using System;
using UnityEngine;

// Token: 0x0200017F RID: 383
public class TileManager
{
	// Token: 0x06000B7F RID: 2943 RVA: 0x000A5909 File Offset: 0x000A3B09
	public void setup(int pWidth, int pHeight, WorldTile[,] pTilesMap)
	{
		this.tiles_count = pWidth * pHeight;
		this.setupAnimationSeeds();
		this.setupVector3Positions();
		this.setupFires();
	}

	// Token: 0x06000B80 RID: 2944 RVA: 0x000A5926 File Offset: 0x000A3B26
	private void setupFires()
	{
		if (this.fires == null || this.fires.Length != this.tiles_count)
		{
			this.fires = new bool[this.tiles_count];
		}
	}

	// Token: 0x06000B81 RID: 2945 RVA: 0x000A5954 File Offset: 0x000A3B54
	private void setupVector3Positions()
	{
		if (this.positions_vector3 == null || this.positions_vector3.Length != this.tiles_count)
		{
			this.positions_vector3 = new Vector3[this.tiles_count];
		}
		foreach (WorldTile tTile in World.world.tiles_list)
		{
			this.positions_vector3[tTile.data.tile_id] = tTile.posV3;
		}
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x000A59C4 File Offset: 0x000A3BC4
	private void setupAnimationSeeds()
	{
		if (this.random_seeds == null || this.random_seeds.Length != this.tiles_count)
		{
			this.random_seeds = new int[this.tiles_count];
			this.fire_animation_set = new int[this.tiles_count];
			for (int i = 0; i < this.random_seeds.Length; i++)
			{
				this.random_seeds[i] = Randy.randomInt(0, 10000);
				if (this.random_seeds[i] % 6 == 0)
				{
					this.fire_animation_set[i] = 2;
				}
				else if (this.random_seeds[i] % 3 == 0)
				{
					this.fire_animation_set[i] = 1;
				}
				else
				{
					this.fire_animation_set[i] = 0;
				}
			}
		}
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x000A5A68 File Offset: 0x000A3C68
	public void clear()
	{
		bool[] array = this.fires;
		if (array == null)
		{
			return;
		}
		array.Clear<bool>();
	}

	// Token: 0x04000B19 RID: 2841
	public int[] random_seeds;

	// Token: 0x04000B1A RID: 2842
	public int[] fire_animation_set;

	// Token: 0x04000B1B RID: 2843
	public bool[] fires;

	// Token: 0x04000B1C RID: 2844
	public Vector3[] positions_vector3;

	// Token: 0x04000B1D RID: 2845
	public int tiles_count;
}
