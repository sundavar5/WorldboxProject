using System;
using UnityEngine;

// Token: 0x0200075C RID: 1884
public class TabCenterer : MonoBehaviour
{
	// Token: 0x06003BB3 RID: 15283 RVA: 0x001A13D5 File Offset: 0x0019F5D5
	private void Update()
	{
		this.centerTab();
	}

	// Token: 0x06003BB4 RID: 15284 RVA: 0x001A13E0 File Offset: 0x0019F5E0
	public void centerTab()
	{
	}

	// Token: 0x04002BBC RID: 11196
	[SerializeField]
	private PowersTab _powers_tab;

	// Token: 0x04002BBD RID: 11197
	public const bool ENABLE_CENTERING = false;
}
