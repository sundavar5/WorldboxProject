using System;
using UnityEngine;

// Token: 0x0200048E RID: 1166
public class TextSortingLayer : MonoBehaviour
{
	// Token: 0x060027EC RID: 10220 RVA: 0x00141BB2 File Offset: 0x0013FDB2
	private void Start()
	{
		this.meshRenderer = base.gameObject.GetComponent<MeshRenderer>();
		this.meshRenderer.sortingLayerID = SortingLayer.NameToID("MapOverlay");
		this.meshRenderer.sortingOrder = 200;
	}

	// Token: 0x04001E14 RID: 7700
	private MeshRenderer meshRenderer;
}
