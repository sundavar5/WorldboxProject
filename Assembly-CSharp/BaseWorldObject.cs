using System;
using UnityEngine;

// Token: 0x020001C3 RID: 451
public class BaseWorldObject : MonoBehaviour, IDisposable
{
	// Token: 0x06000D4B RID: 3403 RVA: 0x000BB915 File Offset: 0x000B9B15
	private void Start()
	{
		if (!this.created)
		{
			this.create();
		}
	}

	// Token: 0x06000D4C RID: 3404 RVA: 0x000BB925 File Offset: 0x000B9B25
	public virtual void update(float pElapsed)
	{
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x000BB927 File Offset: 0x000B9B27
	internal virtual void create()
	{
		this.created = true;
		this.m_transform = base.gameObject.transform;
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x000BB941 File Offset: 0x000B9B41
	public virtual void Dispose()
	{
		this.m_transform = null;
	}

	// Token: 0x04000CA7 RID: 3239
	internal bool created;

	// Token: 0x04000CA8 RID: 3240
	internal Transform m_transform;
}
