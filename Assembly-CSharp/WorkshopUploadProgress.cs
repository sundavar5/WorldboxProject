using System;
using UnityEngine;

// Token: 0x020005B1 RID: 1457
internal class WorkshopUploadProgress : IProgress<float>
{
	// Token: 0x0600302F RID: 12335 RVA: 0x00175195 File Offset: 0x00173395
	public void Report(float value)
	{
		if (this.lastvalue >= value)
		{
			return;
		}
		this.lastvalue = value;
		WorkshopMaps.uploadProgress = this.lastvalue;
		Debug.Log(value);
	}

	// Token: 0x04002453 RID: 9299
	internal float lastvalue;
}
