using System;
using UnityEngine;

// Token: 0x0200015C RID: 348
public class ActorRenderData
{
	// Token: 0x06000A5F RID: 2655 RVA: 0x00096470 File Offset: 0x00094670
	public ActorRenderData(int pCapacity)
	{
		this.checkSize(pCapacity);
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x00096480 File Offset: 0x00094680
	public void checkSize(int pTargetSize)
	{
		if (this.positions != null && this.positions.Length >= pTargetSize)
		{
			return;
		}
		this.positions = Toolbox.checkArraySize<Vector3>(this.positions, pTargetSize);
		this.scales = Toolbox.checkArraySize<Vector3>(this.scales, pTargetSize);
		this.rotations = Toolbox.checkArraySize<Vector3>(this.rotations, pTargetSize);
		this.colors = Toolbox.checkArraySize<Color>(this.colors, pTargetSize);
		this.has_normal_render = Toolbox.checkArraySize<bool>(this.has_normal_render, pTargetSize);
		this.main_sprites = Toolbox.checkArraySize<Sprite>(this.main_sprites, pTargetSize);
		this.main_sprite_colored = Toolbox.checkArraySize<Sprite>(this.main_sprite_colored, pTargetSize);
		this.materials = Toolbox.checkArraySize<Material>(this.materials, pTargetSize);
		this.flip_x_states = Toolbox.checkArraySize<bool>(this.flip_x_states, pTargetSize);
		this.shadows = Toolbox.checkArraySize<bool>(this.shadows, pTargetSize);
		this.shadow_sprites = Toolbox.checkArraySize<Sprite>(this.shadow_sprites, pTargetSize);
		this.shadow_position = Toolbox.checkArraySize<Vector3>(this.shadow_position, pTargetSize);
		this.shadow_scales = Toolbox.checkArraySize<Vector3>(this.shadow_scales, pTargetSize);
		this.has_item = Toolbox.checkArraySize<bool>(this.has_item, pTargetSize);
		this.item_scale = Toolbox.checkArraySize<Vector3>(this.item_scale, pTargetSize);
		this.item_pos = Toolbox.checkArraySize<Vector3>(this.item_pos, pTargetSize);
		this.item_sprites = Toolbox.checkArraySize<Sprite>(this.item_sprites, pTargetSize);
	}

	// Token: 0x04000A06 RID: 2566
	public Vector3[] positions;

	// Token: 0x04000A07 RID: 2567
	public Vector3[] scales;

	// Token: 0x04000A08 RID: 2568
	public Vector3[] rotations;

	// Token: 0x04000A09 RID: 2569
	public Color[] colors;

	// Token: 0x04000A0A RID: 2570
	public bool[] has_normal_render;

	// Token: 0x04000A0B RID: 2571
	public Sprite[] main_sprites;

	// Token: 0x04000A0C RID: 2572
	public Sprite[] main_sprite_colored;

	// Token: 0x04000A0D RID: 2573
	public Material[] materials;

	// Token: 0x04000A0E RID: 2574
	public bool[] flip_x_states;

	// Token: 0x04000A0F RID: 2575
	public bool[] shadows;

	// Token: 0x04000A10 RID: 2576
	public Vector3[] shadow_position;

	// Token: 0x04000A11 RID: 2577
	public Vector3[] shadow_scales;

	// Token: 0x04000A12 RID: 2578
	public Sprite[] shadow_sprites;

	// Token: 0x04000A13 RID: 2579
	public bool[] has_item;

	// Token: 0x04000A14 RID: 2580
	public Vector3[] item_scale;

	// Token: 0x04000A15 RID: 2581
	public Vector3[] item_pos;

	// Token: 0x04000A16 RID: 2582
	public Sprite[] item_sprites;
}
