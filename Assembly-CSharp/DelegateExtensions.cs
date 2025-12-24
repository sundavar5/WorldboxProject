using System;

// Token: 0x0200044E RID: 1102
public static class DelegateExtensions
{
	// Token: 0x0600261C RID: 9756 RVA: 0x00138004 File Offset: 0x00136204
	public static string AsString<T>(this T pDelegate) where T : Delegate
	{
		if (pDelegate == null)
		{
			return "";
		}
		string result;
		using (ListPool<string> tStringToPrint = new ListPool<string>(pDelegate.GetInvocationList().Length))
		{
			Delegate[] invocationList = pDelegate.GetInvocationList();
			for (int i = 0; i < invocationList.Length; i++)
			{
				T tObject = (T)((object)invocationList[i]);
				tStringToPrint.Add(tObject.Method.Name);
			}
			result = string.Join(", ", tStringToPrint.ToArray<string>());
		}
		return result;
	}
}
