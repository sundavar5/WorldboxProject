using System;

// Token: 0x02000454 RID: 1108
public static class EnumExtensions
{
	// Token: 0x06002627 RID: 9767 RVA: 0x001386D0 File Offset: 0x001368D0
	public static int Count<TEnum>(this TEnum pEnum) where TEnum : Enum
	{
		int iCount = 0;
		int tEnum = Convert.ToInt32(pEnum);
		while (tEnum != 0)
		{
			tEnum &= tEnum - 1;
			iCount++;
		}
		return iCount;
	}
}
