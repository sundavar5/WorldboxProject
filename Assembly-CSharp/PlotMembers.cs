using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200072A RID: 1834
public class PlotMembers : PlotElement
{
	// Token: 0x06003A6C RID: 14956 RVA: 0x0019DBAE File Offset: 0x0019BDAE
	protected override void Awake()
	{
		this._pool_members = new ObjectPoolGenericMono<UiUnitAvatarElement>(this._prefab_avatar, this._transform_members);
		base.Awake();
	}

	// Token: 0x06003A6D RID: 14957 RVA: 0x0019DBCD File Offset: 0x0019BDCD
	protected override IEnumerator showContent()
	{
		PlotMembers.<showContent>d__4 <showContent>d__ = new PlotMembers.<showContent>d__4(0);
		<showContent>d__.<>4__this = this;
		return <showContent>d__;
	}

	// Token: 0x06003A6E RID: 14958 RVA: 0x0019DBDC File Offset: 0x0019BDDC
	private IEnumerator showMember(Actor pActor)
	{
		if (pActor == null)
		{
			yield break;
		}
		yield return new WaitForSecondsRealtime(0.025f);
		UiUnitAvatarElement next = this._pool_members.getNext();
		next.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
		next.show(pActor);
		yield break;
	}

	// Token: 0x06003A6F RID: 14959 RVA: 0x0019DBF2 File Offset: 0x0019BDF2
	protected override void clear()
	{
		this._pool_members.clear(true);
		base.clear();
	}

	// Token: 0x04002B23 RID: 11043
	[SerializeField]
	private UiUnitAvatarElement _prefab_avatar;

	// Token: 0x04002B24 RID: 11044
	[SerializeField]
	private Transform _transform_members;

	// Token: 0x04002B25 RID: 11045
	private ObjectPoolGenericMono<UiUnitAvatarElement> _pool_members;
}
