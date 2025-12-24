using System;
using UnityEngine;

// Token: 0x02000536 RID: 1334
public class DebugVariables : MonoBehaviour
{
	// Token: 0x04002172 RID: 8562
	public static DebugVariables instance;

	// Token: 0x04002173 RID: 8563
	[Range(1f, 1000f)]
	public float multiplier = 1f;

	// Token: 0x04002174 RID: 8564
	[Range(1f, 10000000f)]
	public float bonus = 1f;

	// Token: 0x04002175 RID: 8565
	public float time;

	// Token: 0x04002176 RID: 8566
	[Range(0f, 1000f)]
	public float gravity = 9.8f;

	// Token: 0x04002177 RID: 8567
	[Range(0f, 10f)]
	public float unit_force_multiplier = 1f;

	// Token: 0x04002178 RID: 8568
	[Range(0f, 10f)]
	public float test_mass = 2f;

	// Token: 0x04002179 RID: 8569
	public bool layout_city_test;

	// Token: 0x0400217A RID: 8570
	public bool layout_lines_horizontal;

	// Token: 0x0400217B RID: 8571
	public bool layout_lines_vertical;

	// Token: 0x0400217C RID: 8572
	public bool layout_cross;

	// Token: 0x0400217D RID: 8573
	public bool layout_diagonal;

	// Token: 0x0400217E RID: 8574
	public bool layout_lattice_small;

	// Token: 0x0400217F RID: 8575
	public bool layout_lattice_medium;

	// Token: 0x04002180 RID: 8576
	public bool layout_lattice_big;

	// Token: 0x04002181 RID: 8577
	public bool layout_clusters_small;

	// Token: 0x04002182 RID: 8578
	public bool layout_clusters_medium;

	// Token: 0x04002183 RID: 8579
	public bool layout_clusters_big;

	// Token: 0x04002184 RID: 8580
	public bool layout_ring;

	// Token: 0x04002185 RID: 8581
	public bool layout_diamond;

	// Token: 0x04002186 RID: 8582
	public bool layout_diamond_cluster;

	// Token: 0x04002187 RID: 8583
	public bool layout_honeycomb;

	// Token: 0x04002188 RID: 8584
	public bool layout_brick_vertical;

	// Token: 0x04002189 RID: 8585
	public bool layout_brick_horizontal;

	// Token: 0x0400218A RID: 8586
	public bool layout_madman_labyrinth;

	// Token: 0x0400218B RID: 8587
	public bool layout_map_ring;
}
