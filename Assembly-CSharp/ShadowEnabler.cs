using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000779 RID: 1913
public class ShadowEnabler : MonoBehaviour
{
	// Token: 0x06003C8D RID: 15501 RVA: 0x001A411F File Offset: 0x001A231F
	private void Awake()
	{
		this.shadowObjects = base.GetComponentsInChildren<Shadow>(true);
	}

	// Token: 0x06003C8E RID: 15502 RVA: 0x001A4130 File Offset: 0x001A2330
	private void Update()
	{
		bool _enabled = base.transform.localScale.y == 1f;
		if (this.isEnabled != _enabled)
		{
			this.isEnabled = _enabled;
			this.toggle();
		}
	}

	// Token: 0x06003C8F RID: 15503 RVA: 0x001A416B File Offset: 0x001A236B
	private void OnDisable()
	{
		this.isEnabled = false;
		this.toggle();
	}

	// Token: 0x06003C90 RID: 15504 RVA: 0x001A417A File Offset: 0x001A237A
	private void OnEnable()
	{
		this.isEnabled = false;
		this.toggle();
	}

	// Token: 0x06003C91 RID: 15505 RVA: 0x001A418C File Offset: 0x001A238C
	private void toggle()
	{
		for (int i = 0; i < this.shadowObjects.Length; i++)
		{
			Shadow shadowItem = this.shadowObjects[i];
			if (!(shadowItem == null))
			{
				shadowItem.enabled = this.isEnabled;
			}
		}
	}

	// Token: 0x04002BFE RID: 11262
	public Shadow[] shadowObjects = new Shadow[0];

	// Token: 0x04002BFF RID: 11263
	private bool isEnabled;
}
