using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007A9 RID: 1961
public class AxonElement : MonoBehaviour
{
	// Token: 0x06003E0C RID: 15884 RVA: 0x001B09F8 File Offset: 0x001AEBF8
	public void update()
	{
		this.mod_light -= Time.deltaTime * 2f;
		this.mod_light = Mathf.Max(0f, this.mod_light);
	}

	// Token: 0x06003E0D RID: 15885 RVA: 0x001B0A28 File Offset: 0x001AEC28
	public void clear()
	{
		this.axon_center = false;
	}

	// Token: 0x04002D11 RID: 11537
	public Image image;

	// Token: 0x04002D12 RID: 11538
	internal NeuronElement neuron_1;

	// Token: 0x04002D13 RID: 11539
	internal NeuronElement neuron_2;

	// Token: 0x04002D14 RID: 11540
	internal float mod_light = 1f;

	// Token: 0x04002D15 RID: 11541
	public bool axon_center;
}
