using System;
using UnityEngine;

// Token: 0x020002EC RID: 748
public class QuantumSpriteCacheData
{
	// Token: 0x06001C25 RID: 7205 RVA: 0x001004D4 File Offset: 0x000FE6D4
	public QuantumSpriteCacheData(int pCapacity)
	{
		this.checkSize(pCapacity);
	}

	// Token: 0x06001C26 RID: 7206 RVA: 0x001004E4 File Offset: 0x000FE6E4
	public void checkSize(int pTargetSize)
	{
		if (this.positions != null && this.positions.Length >= pTargetSize)
		{
			return;
		}
		this.positions = Toolbox.checkArraySize<Vector3>(this.positions, pTargetSize);
		this.scales = Toolbox.checkArraySize<Vector3>(this.scales, pTargetSize);
		this.shadow_scales = Toolbox.checkArraySize<Vector3>(this.shadow_scales, pTargetSize);
		this.rotations = Toolbox.checkArraySize<Vector3>(this.rotations, pTargetSize);
		this.sprites = Toolbox.checkArraySize<Sprite>(this.sprites, pTargetSize);
		this.materials = Toolbox.checkArraySize<Material>(this.materials, pTargetSize);
		this.flip_x_states = Toolbox.checkArraySize<bool>(this.flip_x_states, pTargetSize);
		this.colors = Toolbox.checkArraySize<Color>(this.colors, pTargetSize);
		this.indexes = Toolbox.checkArraySize<int>(this.indexes, pTargetSize);
		this.indexes_2 = Toolbox.checkArraySize<int>(this.indexes_2, pTargetSize);
	}

	// Token: 0x0400157E RID: 5502
	public Sprite[] sprites;

	// Token: 0x0400157F RID: 5503
	public Vector3[] scales;

	// Token: 0x04001580 RID: 5504
	public Vector3[] shadow_scales;

	// Token: 0x04001581 RID: 5505
	public Vector3[] positions;

	// Token: 0x04001582 RID: 5506
	public Material[] materials;

	// Token: 0x04001583 RID: 5507
	public Vector3[] rotations;

	// Token: 0x04001584 RID: 5508
	public bool[] flip_x_states;

	// Token: 0x04001585 RID: 5509
	public Color[] colors;

	// Token: 0x04001586 RID: 5510
	public int[] indexes;

	// Token: 0x04001587 RID: 5511
	public int[] indexes_2;
}
