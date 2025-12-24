using System;
using UnityEngine;

// Token: 0x02000528 RID: 1320
public class DebugDropdown : MonoBehaviour
{
	// Token: 0x06002B3A RID: 11066 RVA: 0x0015688D File Offset: 0x00154A8D
	private void OnEnable()
	{
		this._debug_tool.active_dropdown = this;
	}

	// Token: 0x04002065 RID: 8293
	[SerializeField]
	private DebugTool _debug_tool;
}
