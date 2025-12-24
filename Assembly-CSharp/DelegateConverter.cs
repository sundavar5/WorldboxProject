using System;
using Newtonsoft.Json;

// Token: 0x02000571 RID: 1393
public class DelegateConverter : JsonConverter
{
	// Token: 0x06002D74 RID: 11636 RVA: 0x001644B8 File Offset: 0x001626B8
	public override void WriteJson(JsonWriter pWriter, object pValue, JsonSerializer pSerializer)
	{
		if (pValue == null)
		{
			return;
		}
		Delegate[] tDelegates = ((Delegate)pValue).GetInvocationList();
		string[] tDelegateNames = new string[tDelegates.Length];
		for (int i = 0; i < tDelegates.Length; i++)
		{
			string[] array = tDelegateNames;
			int num = i;
			Type declaringType = tDelegates[i].Method.DeclaringType;
			array[num] = ((declaringType != null) ? declaringType.ToString() : null) + "." + tDelegates[i].Method.Name;
		}
		pSerializer.Serialize(pWriter, tDelegateNames, typeof(string[]));
	}

	// Token: 0x06002D75 RID: 11637 RVA: 0x00164531 File Offset: 0x00162731
	public override object ReadJson(JsonReader pReader, Type pObjectType, object pExistingValue, JsonSerializer pSerializer)
	{
		return null;
	}

	// Token: 0x06002D76 RID: 11638 RVA: 0x00164534 File Offset: 0x00162734
	public override bool CanConvert(Type pObjectType)
	{
		return pObjectType != null && (pObjectType == typeof(Delegate) || pObjectType.IsSubclassOf(typeof(Delegate)));
	}
}
