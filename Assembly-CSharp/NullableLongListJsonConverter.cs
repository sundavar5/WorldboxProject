using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x02000268 RID: 616
public class NullableLongListJsonConverter : JsonConverter
{
	// Token: 0x1700015B RID: 347
	// (get) Token: 0x0600171A RID: 5914 RVA: 0x000E5A6D File Offset: 0x000E3C6D
	public override bool CanWrite
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700015C RID: 348
	// (get) Token: 0x0600171B RID: 5915 RVA: 0x000E5A70 File Offset: 0x000E3C70
	public override bool CanRead
	{
		get
		{
			return true;
		}
	}

	// Token: 0x0600171C RID: 5916 RVA: 0x000E5A74 File Offset: 0x000E3C74
	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		if (reader.TokenType == JsonToken.Null)
		{
			return null;
		}
		if (reader.TokenType == JsonToken.StartArray)
		{
			using (ListPool<long?> tList = new ListPool<long?>())
			{
				while (reader.Read())
				{
					JsonToken tokenType = reader.TokenType;
					switch (tokenType)
					{
					case JsonToken.Integer:
						tList.Add(new long?(Convert.ToInt64(reader.Value)));
						break;
					case JsonToken.Float:
					case JsonToken.Boolean:
						break;
					case JsonToken.String:
					{
						string tString = (string)reader.Value;
						tList.Add(NullableLongJsonConverter.getLong(tString, reader));
						break;
					}
					case JsonToken.Null:
						tList.Add(null);
						break;
					default:
						if (tokenType == JsonToken.EndArray)
						{
							return new List<long?>(tList);
						}
						break;
					}
				}
			}
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

	// Token: 0x0600171D RID: 5917 RVA: 0x000E5BA8 File Offset: 0x000E3DA8
	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		writer.WriteValue(value);
	}

	// Token: 0x0600171E RID: 5918 RVA: 0x000E5BB1 File Offset: 0x000E3DB1
	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(List<long?>);
	}
}
