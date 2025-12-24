using System;
using System.Collections;
using UnityEngine;

// Token: 0x020006DE RID: 1758
public class UnitFamilyOriginElement : UnitElement
{
	// Token: 0x06003865 RID: 14437 RVA: 0x001952B0 File Offset: 0x001934B0
	protected override IEnumerator showContent()
	{
		if (!this.actor.data.ancestor_family.hasValue())
		{
			yield break;
		}
		this._ancestor_family = World.world.families.get(this.actor.data.ancestor_family);
		if (this._ancestor_family.isRekt())
		{
			yield break;
		}
		this.track_objects.Add(this._ancestor_family);
		yield return new WaitForSecondsRealtime(0.025f);
		if (!this._ancestor_family.isAlive())
		{
			yield break;
		}
		this._family_origin_title.SetActive(true);
		this._origin_element.gameObject.SetActive(true);
		this._origin_element.show(this._ancestor_family);
		yield break;
	}

	// Token: 0x06003866 RID: 14438 RVA: 0x001952BF File Offset: 0x001934BF
	protected override void clear()
	{
		this._ancestor_family = null;
		this._family_origin_title.SetActive(false);
		this._origin_element.gameObject.SetActive(false);
		base.clear();
	}

	// Token: 0x040029DC RID: 10716
	[SerializeField]
	private FamilyListElement _origin_element;

	// Token: 0x040029DD RID: 10717
	[SerializeField]
	private GameObject _family_origin_title;

	// Token: 0x040029DE RID: 10718
	private Family _ancestor_family;
}
