using System;
using System.Reflection;
using Newtonsoft.Json;

// Token: 0x02000264 RID: 612
public class CustomDataContainerConverter : JsonConverter
{
	// Token: 0x06001700 RID: 5888 RVA: 0x000E54C8 File Offset: 0x000E36C8
	public override void WriteJson(JsonWriter pWriter, object pValue, JsonSerializer pSerializer)
	{
		FieldInfo field = pValue.GetType().GetField("dict", BindingFlags.Instance | BindingFlags.NonPublic);
		Type tDictType = field.FieldType;
		object tDictValue = field.GetValue(pValue);
		pSerializer.Serialize(pWriter, tDictValue, tDictType);
	}

	// Token: 0x06001701 RID: 5889 RVA: 0x000E5500 File Offset: 0x000E3700
	public override object ReadJson(JsonReader pReader, Type pObjectType, object pExistingValue, JsonSerializer pSerializer)
	{
		object tContainer = Activator.CreateInstance(pObjectType);
		FieldInfo field = pObjectType.GetField("dict", BindingFlags.Instance | BindingFlags.NonPublic);
		Type tDictType = field.FieldType;
		field.SetValue(tContainer, pSerializer.Deserialize(pReader, tDictType));
		return tContainer;
	}

	// Token: 0x06001702 RID: 5890 RVA: 0x000E5538 File Offset: 0x000E3738
	public override bool CanConvert(Type pObjectType)
	{
		return false;
	}
}
