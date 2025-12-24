using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007FD RID: 2045
public abstract class WindowMetaElementBase : MonoBehaviour, IShouldRefreshWindow
{
	// Token: 0x06004030 RID: 16432 RVA: 0x001B791D File Offset: 0x001B5B1D
	protected virtual void Awake()
	{
		this.clearInitial();
		this.clear();
	}

	// Token: 0x06004031 RID: 16433 RVA: 0x001B792B File Offset: 0x001B5B2B
	protected virtual void OnEnable()
	{
		this.clear();
		base.StartCoroutine(this.showContent());
	}

	// Token: 0x06004032 RID: 16434 RVA: 0x001B7940 File Offset: 0x001B5B40
	public virtual void refresh()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		base.StopAllCoroutines();
		this.clear();
		base.StartCoroutine(this.showContent());
	}

	// Token: 0x06004033 RID: 16435 RVA: 0x001B7969 File Offset: 0x001B5B69
	protected virtual IEnumerator showContent()
	{
		yield break;
	}

	// Token: 0x06004034 RID: 16436 RVA: 0x001B7971 File Offset: 0x001B5B71
	protected virtual void OnDisable()
	{
		this.clear();
	}

	// Token: 0x06004035 RID: 16437 RVA: 0x001B7979 File Offset: 0x001B5B79
	protected virtual void clear()
	{
		this.track_objects.Clear();
	}

	// Token: 0x06004036 RID: 16438 RVA: 0x001B7986 File Offset: 0x001B5B86
	protected virtual void clearInitial()
	{
	}

	// Token: 0x06004037 RID: 16439 RVA: 0x001B7988 File Offset: 0x001B5B88
	public virtual bool checkRefreshWindow()
	{
		using (List<NanoObject>.Enumerator enumerator = this.track_objects.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isRekt())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x04002E8D RID: 11917
	protected readonly List<NanoObject> track_objects = new List<NanoObject>();
}
