using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// Token: 0x02000572 RID: 1394
public class OrderedContractResolver : DefaultContractResolver
{
	// Token: 0x06002D78 RID: 11640 RVA: 0x0016456D File Offset: 0x0016276D
	protected override IList<JsonProperty> CreateProperties(Type pObjectType, MemberSerialization pMemberSerialization)
	{
		List<JsonProperty> list = new List<JsonProperty>(base.CreateProperties(pObjectType, pMemberSerialization));
		list.Sort(new Comparison<JsonProperty>(this.orderedPropertySorter));
		return list;
	}

	// Token: 0x06002D79 RID: 11641 RVA: 0x00164590 File Offset: 0x00162790
	private int orderedPropertySorter(JsonProperty p1, JsonProperty p2)
	{
		int? order = p1.Order;
		int? order2 = p2.Order;
		if (!(order.GetValueOrDefault() == order2.GetValueOrDefault() & order != null == (order2 != null)))
		{
			int tOrder = p1.Order ?? int.MaxValue;
			int tOrder2 = p2.Order ?? int.MaxValue;
			return tOrder.CompareTo(tOrder2);
		}
		bool tIsDelegate = this.isDelegate(p1.PropertyType);
		bool tIsDelegate2 = this.isDelegate(p2.PropertyType);
		if (tIsDelegate != tIsDelegate2)
		{
			return tIsDelegate.CompareTo(tIsDelegate2);
		}
		bool tIsEnumerable = this.isCollection(p1.PropertyType);
		bool tIsEnumerable2 = this.isCollection(p2.PropertyType);
		if (tIsEnumerable != tIsEnumerable2)
		{
			return tIsEnumerable.CompareTo(tIsEnumerable2);
		}
		return p1.PropertyName.CompareTo(p2.PropertyName);
	}

	// Token: 0x06002D7A RID: 11642 RVA: 0x0016467C File Offset: 0x0016287C
	private int getBaseTypesCount(Type pType)
	{
		int tCount = 0;
		while (pType != null)
		{
			tCount++;
			pType = pType.BaseType;
		}
		return tCount;
	}

	// Token: 0x06002D7B RID: 11643 RVA: 0x001646A3 File Offset: 0x001628A3
	private bool isDelegate(Type pType)
	{
		return pType == typeof(Delegate) || pType.IsSubclassOf(typeof(Delegate));
	}

	// Token: 0x06002D7C RID: 11644 RVA: 0x001646C9 File Offset: 0x001628C9
	private bool isCollection(Type pType)
	{
		return typeof(ICollection).IsAssignableFrom(pType);
	}
}
