using System;
using UnityEngine;

// Token: 0x0200015D RID: 349
public class BuildingRenderData
{
	// Token: 0x06000A61 RID: 2657 RVA: 0x000965D3 File Offset: 0x000947D3
	public BuildingRenderData(int pCapacity)
	{
		this.checkSize(pCapacity);
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x000965E4 File Offset: 0x000947E4
	public void checkSize(int pTargetSize)
	{
		if (this.positions != null && this.positions.Length >= pTargetSize)
		{
			return;
		}
		this.positions = Toolbox.checkArraySize<Vector3>(this.positions, pTargetSize);
		this.scales = Toolbox.checkArraySize<Vector3>(this.scales, pTargetSize);
		this.rotations = Toolbox.checkArraySize<Vector3>(this.rotations, pTargetSize);
		this.colored_sprites = Toolbox.checkArraySize<Sprite>(this.colored_sprites, pTargetSize);
		this.main_sprites = Toolbox.checkArraySize<Sprite>(this.main_sprites, pTargetSize);
		this.materials = Toolbox.checkArraySize<Material>(this.materials, pTargetSize);
		this.flip_x_states = Toolbox.checkArraySize<bool>(this.flip_x_states, pTargetSize);
		this.colors = Toolbox.checkArraySize<Color>(this.colors, pTargetSize);
		this.shadows = Toolbox.checkArraySize<bool>(this.shadows, pTargetSize);
		this.shadow_sprites = Toolbox.checkArraySize<Sprite>(this.shadow_sprites, pTargetSize);
	}

	// Token: 0x04000A17 RID: 2583
	public Vector3[] positions;

	// Token: 0x04000A18 RID: 2584
	public Vector3[] scales;

	// Token: 0x04000A19 RID: 2585
	public Vector3[] rotations;

	// Token: 0x04000A1A RID: 2586
	public Sprite[] colored_sprites;

	// Token: 0x04000A1B RID: 2587
	public Sprite[] main_sprites;

	// Token: 0x04000A1C RID: 2588
	public Material[] materials;

	// Token: 0x04000A1D RID: 2589
	public bool[] flip_x_states;

	// Token: 0x04000A1E RID: 2590
	public Color[] colors;

	// Token: 0x04000A1F RID: 2591
	public bool[] shadows;

	// Token: 0x04000A20 RID: 2592
	public Sprite[] shadow_sprites;
}
