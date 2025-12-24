using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000668 RID: 1640
public class ClanMembersContainer : ClanElement
{
	// Token: 0x06003513 RID: 13587 RVA: 0x00187DC8 File Offset: 0x00185FC8
	protected override void Awake()
	{
		this._pool_members = new ObjectPoolGenericMono<PrefabUnitElement>(this._prefab, this._list_members);
		base.Awake();
	}

	// Token: 0x06003514 RID: 13588 RVA: 0x00187DE7 File Offset: 0x00185FE7
	protected override IEnumerator showContent()
	{
		ClanMembersContainer.<showContent>d__6 <showContent>d__ = new ClanMembersContainer.<showContent>d__6(0);
		<showContent>d__.<>4__this = this;
		return <showContent>d__;
	}

	// Token: 0x06003515 RID: 13589 RVA: 0x00187DF6 File Offset: 0x00185FF6
	private void showMember(Actor pActor)
	{
		PrefabUnitElement next = this._pool_members.getNext();
		next.transform.localScale = new Vector3(0.9f, 0.9f, 1f);
		next.show(pActor);
	}

	// Token: 0x06003516 RID: 13590 RVA: 0x00187E28 File Offset: 0x00186028
	protected override void clear()
	{
		this._title_members.gameObject.SetActive(false);
		this._list_members.gameObject.SetActive(false);
		this._pool_members.clear(true);
		base.clear();
	}

	// Token: 0x040027D0 RID: 10192
	private ObjectPoolGenericMono<PrefabUnitElement> _pool_members;

	// Token: 0x040027D1 RID: 10193
	[SerializeField]
	private RectTransform _list_members;

	// Token: 0x040027D2 RID: 10194
	[SerializeField]
	private LocalizedText _title_members;

	// Token: 0x040027D3 RID: 10195
	[SerializeField]
	private PrefabUnitElement _prefab;

	// Token: 0x040027D4 RID: 10196
	[SerializeField]
	private Text _members_counter;
}
