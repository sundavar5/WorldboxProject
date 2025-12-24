using System;
using UnityEngine;

// Token: 0x020007D3 RID: 2003
public class CustomizeWindow : MonoBehaviour
{
	// Token: 0x06003F46 RID: 16198 RVA: 0x001B5280 File Offset: 0x001B3480
	private void OnEnable()
	{
		if (this._created)
		{
			return;
		}
		this._created = true;
		AssetManager.meta_customization_library.getAsset(this.meta_type).customize_component(base.gameObject);
	}

	// Token: 0x04002DFE RID: 11774
	public ColorElement color_element_prefab;

	// Token: 0x04002DFF RID: 11775
	public MetaType meta_type;

	// Token: 0x04002E00 RID: 11776
	private bool _created;
}
