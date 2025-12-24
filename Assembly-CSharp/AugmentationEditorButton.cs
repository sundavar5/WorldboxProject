using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200063D RID: 1597
public class AugmentationEditorButton<TAugmentationButton, TAugmentation> : MonoBehaviour where TAugmentationButton : AugmentationButton<TAugmentation> where TAugmentation : BaseAugmentationAsset
{
	// Token: 0x0400274C RID: 10060
	public TAugmentationButton augmentation_button;

	// Token: 0x0400274D RID: 10061
	public Image selected_icon;
}
