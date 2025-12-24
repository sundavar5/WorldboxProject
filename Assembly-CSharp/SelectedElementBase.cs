using System;
using UnityEngine;

// Token: 0x02000750 RID: 1872
public class SelectedElementBase<TComponent> : MonoBehaviour where TComponent : Component
{
	// Token: 0x06003B3C RID: 15164 RVA: 0x001A0333 File Offset: 0x0019E533
	protected void clear()
	{
		ObjectPoolGenericMono<TComponent> pool = this._pool;
		if (pool == null)
		{
			return;
		}
		pool.clear(true);
	}

	// Token: 0x06003B3D RID: 15165 RVA: 0x001A0346 File Offset: 0x0019E546
	protected virtual void refresh(NanoObject pNano)
	{
	}

	// Token: 0x06003B3E RID: 15166 RVA: 0x001A0348 File Offset: 0x0019E548
	private void OnDisable()
	{
		this.clear();
	}

	// Token: 0x04002B96 RID: 11158
	[SerializeField]
	protected Transform _grid;

	// Token: 0x04002B97 RID: 11159
	protected ObjectPoolGenericMono<TComponent> _pool;
}
