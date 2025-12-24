using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000637 RID: 1591
public class ArmyMemberIcons : ArmyElement
{
	// Token: 0x060033D4 RID: 13268 RVA: 0x001840DC File Offset: 0x001822DC
	protected override void Awake()
	{
		this._list_warrior_avatars = new UnitAvatarLoader[]
		{
			this._top,
			this._top_left,
			this._right,
			this._bottom_right,
			this._bottom,
			this._bottom_left,
			this._left,
			this._top_right
		};
		base.Awake();
	}

	// Token: 0x060033D5 RID: 13269 RVA: 0x00184144 File Offset: 0x00182344
	protected override void clear()
	{
		UnitAvatarLoader[] list_warrior_avatars = this._list_warrior_avatars;
		for (int i = 0; i < list_warrior_avatars.Length; i++)
		{
			list_warrior_avatars[i].gameObject.SetActive(false);
		}
		this._banner.gameObject.SetActive(false);
	}

	// Token: 0x060033D6 RID: 13270 RVA: 0x00184185 File Offset: 0x00182385
	protected override IEnumerator showContent()
	{
		ArmyMemberIcons.<showContent>d__12 <showContent>d__ = new ArmyMemberIcons.<showContent>d__12(0);
		<showContent>d__.<>4__this = this;
		return <showContent>d__;
	}

	// Token: 0x0400272E RID: 10030
	[SerializeField]
	private UnitAvatarLoader _top;

	// Token: 0x0400272F RID: 10031
	[SerializeField]
	private UnitAvatarLoader _top_left;

	// Token: 0x04002730 RID: 10032
	[SerializeField]
	private UnitAvatarLoader _top_right;

	// Token: 0x04002731 RID: 10033
	[SerializeField]
	private UnitAvatarLoader _left;

	// Token: 0x04002732 RID: 10034
	[SerializeField]
	private UnitAvatarLoader _right;

	// Token: 0x04002733 RID: 10035
	[SerializeField]
	private UnitAvatarLoader _bottom;

	// Token: 0x04002734 RID: 10036
	[SerializeField]
	private UnitAvatarLoader _bottom_left;

	// Token: 0x04002735 RID: 10037
	[SerializeField]
	private UnitAvatarLoader _bottom_right;

	// Token: 0x04002736 RID: 10038
	[SerializeField]
	private ArmyBanner _banner;

	// Token: 0x04002737 RID: 10039
	private UnitAvatarLoader[] _list_warrior_avatars;
}
