using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007D2 RID: 2002
public class ComponentListSapient<TListElement, TMetaObject, TData, TComponent> : ComponentListBase<TListElement, TMetaObject, TData, TComponent>, ISapientListComponent where TListElement : WindowListElementBase<TMetaObject, TData> where TMetaObject : CoreSystemObject<TData> where TData : BaseSystemData where TComponent : ComponentListBase<TListElement, TMetaObject, TData, TComponent>
{
	// Token: 0x06003F3D RID: 16189 RVA: 0x001B51C8 File Offset: 0x001B33C8
	protected override void show()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		base.show();
		if (this._sapient_counter != null)
		{
			this._sapient_counter.text = this.latest_counted.ToString();
		}
		if (this._non_sapient_counter != null)
		{
			this._non_sapient_counter.text = this.latest_counted.ToString();
		}
	}

	// Token: 0x06003F3E RID: 16190 RVA: 0x001B522B File Offset: 0x001B342B
	protected override IEnumerable<TMetaObject> getFiltered(IEnumerable<TMetaObject> pList)
	{
		switch (this._filter)
		{
		case SapientListFilter.Default:
		{
			foreach (TMetaObject tMeta in base.getFiltered(pList))
			{
				yield return tMeta;
			}
			IEnumerator<TMetaObject> enumerator = null;
			break;
		}
		case SapientListFilter.Sapient:
		{
			foreach (!1 ! in pList)
			{
				ISapient tMeta2 = (ISapient)((object)!);
				if (tMeta2.isSapient())
				{
					yield return (TMetaObject)((object)tMeta2);
				}
			}
			IEnumerator<TMetaObject> enumerator = null;
			break;
		}
		case SapientListFilter.NonSapient:
		{
			foreach (!1 !2 in pList)
			{
				ISapient tMeta3 = (ISapient)((object)!2);
				if (!tMeta3.isSapient())
				{
					yield return (TMetaObject)((object)tMeta3);
				}
			}
			IEnumerator<TMetaObject> enumerator = null;
			break;
		}
		}
		yield break;
		yield break;
	}

	// Token: 0x06003F3F RID: 16191 RVA: 0x001B5242 File Offset: 0x001B3442
	public void setShowSapientOnly()
	{
		this._filter = SapientListFilter.Sapient;
	}

	// Token: 0x06003F40 RID: 16192 RVA: 0x001B524B File Offset: 0x001B344B
	public void setShowNonSapientOnly()
	{
		this._filter = SapientListFilter.NonSapient;
	}

	// Token: 0x06003F41 RID: 16193 RVA: 0x001B5254 File Offset: 0x001B3454
	public override void setDefault()
	{
		this._filter = SapientListFilter.Default;
	}

	// Token: 0x06003F42 RID: 16194 RVA: 0x001B525D File Offset: 0x001B345D
	public void setSapientCounter(Text pCounter)
	{
		this._sapient_counter = pCounter;
	}

	// Token: 0x06003F43 RID: 16195 RVA: 0x001B5266 File Offset: 0x001B3466
	public void setNonSapientCounter(Text pCounter)
	{
		this._non_sapient_counter = pCounter;
	}

	// Token: 0x04002DFB RID: 11771
	[SerializeField]
	private Text _sapient_counter;

	// Token: 0x04002DFC RID: 11772
	[SerializeField]
	private Text _non_sapient_counter;

	// Token: 0x04002DFD RID: 11773
	private SapientListFilter _filter;
}
