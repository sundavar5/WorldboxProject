using System;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x02000267 RID: 615
public class NullableLongJsonConverter : JsonConverter
{
	// Token: 0x17000159 RID: 345
	// (get) Token: 0x06001713 RID: 5907 RVA: 0x000E5953 File Offset: 0x000E3B53
	public override bool CanWrite
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700015A RID: 346
	// (get) Token: 0x06001714 RID: 5908 RVA: 0x000E5956 File Offset: 0x000E3B56
	public override bool CanRead
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06001715 RID: 5909 RVA: 0x000E595C File Offset: 0x000E3B5C
	public static long? getLong(string pString, JsonReader pReader)
	{
		if (string.IsNullOrEmpty(pString))
		{
			return null;
		}
		return new long?(LongJsonConverter.getLong(pString, pReader));
	}

	// Token: 0x06001716 RID: 5910 RVA: 0x000E5988 File Offset: 0x000E3B88
	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		switch (reader.TokenType)
		{
		case JsonToken.Integer:
			return Convert.ToInt64(reader.Value);
		case JsonToken.String:
			return NullableLongJsonConverter.getLong((string)reader.Value, reader);
		case JsonToken.Null:
			return null;
		}
		string[] array = new string[7];
		array[0] = "Unhandled type ";
		array[1] = reader.Path;
		array[2] = " ";
		int num = 3;
		object value = reader.Value;
		array[num] = ((value != null) ? value.ToString() : null);
		array[4] = " ";
		array[5] = reader.TokenType.ToString();
		array[6] = " -> null";
		Debug.LogWarning(string.Concat(array));
		return null;
	}

	// Token: 0x06001717 RID: 5911 RVA: 0x000E5A4A File Offset: 0x000E3C4A
	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		writer.WriteValue(value);
	}

	// Token: 0x06001718 RID: 5912 RVA: 0x000E5A53 File Offset: 0x000E3C53
	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(long?);
	}
}
