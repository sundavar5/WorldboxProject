using System;
using System.Collections;
using System.Reflection;

// Token: 0x02000458 RID: 1112
public static class FieldInfoExtensions
{
	// Token: 0x06002630 RID: 9776 RVA: 0x00138817 File Offset: 0x00136A17
	public static bool isString(this FieldInfo pField)
	{
		return pField.FieldType == typeof(string);
	}

	// Token: 0x06002631 RID: 9777 RVA: 0x00138830 File Offset: 0x00136A30
	public static bool isCollection(this FieldInfo pField)
	{
		Type tType = pField.FieldType;
		return !pField.isString() && typeof(ICollection).IsAssignableFrom(tType);
	}

	// Token: 0x06002632 RID: 9778 RVA: 0x00138860 File Offset: 0x00136A60
	public static bool isEnumerable(this FieldInfo pField)
	{
		Type tType = pField.FieldType;
		return !pField.isString() && typeof(IEnumerable).IsAssignableFrom(tType);
	}

	// Token: 0x06002633 RID: 9779 RVA: 0x00138890 File Offset: 0x00136A90
	public static bool isCloneable(this FieldInfo pField)
	{
		Type tType = pField.FieldType;
		return !pField.isString() && typeof(ICloneable).IsAssignableFrom(tType);
	}
}
