using System;
using System.Collections.Generic;
using System.Reflection;

// Token: 0x02000498 RID: 1176
public static class TypeExtensions
{
	// Token: 0x06002896 RID: 10390 RVA: 0x00145A30 File Offset: 0x00143C30
	public static bool hasField(this Type pStaticType, string pFieldName)
	{
		return pStaticType.GetField(pFieldName, BindingFlags.Static | BindingFlags.Public) != null;
	}

	// Token: 0x06002897 RID: 10391 RVA: 0x00145A46 File Offset: 0x00143C46
	public static IEnumerable<string> getFields(this Type pStaticType)
	{
		FieldInfo[] tFields = pStaticType.GetFields(BindingFlags.Static | BindingFlags.Public);
		foreach (FieldInfo tField in tFields)
		{
			yield return tField.Name;
		}
		FieldInfo[] array = null;
		yield break;
	}
}
