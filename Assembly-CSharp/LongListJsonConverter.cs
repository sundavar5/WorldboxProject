using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x02000266 RID: 614
public class LongListJsonConverter : JsonConverter
{
	// Token: 0x17000157 RID: 343
	// (get) Token: 0x0600170D RID: 5901 RVA: 0x000E5805 File Offset: 0x000E3A05
	public override bool CanWrite
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000158 RID: 344
	// (get) Token: 0x0600170E RID: 5902 RVA: 0x000E5808 File Offset: 0x000E3A08
	public override bool CanRead
	{
		get
		{
			return true;
		}
	}

	// Token: 0x0600170F RID: 5903 RVA: 0x000E580C File Offset: 0x000E3A0C
	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		if (reader.TokenType == JsonToken.Null)
		{
			return null;
		}
		if (reader.TokenType == JsonToken.StartArray)
		{
			using (ListPool<long> tList = new ListPool<long>())
			{
				while (reader.Read())
				{
					JsonToken tokenType = reader.TokenType;
					switch (tokenType)
					{
					case JsonToken.Integer:
						tList.Add(Convert.ToInt64(reader.Value));
						break;
					case JsonToken.Float:
					case JsonToken.Boolean:
						break;
					case JsonToken.String:
					{
						string tString = (string)reader.Value;
						tList.Add(LongJsonConverter.getLong(tString, reader));
						break;
					}
					case JsonToken.Null:
						tList.Add(-1L);
						break;
					default:
						if (tokenType == JsonToken.EndArray)
						{
							return new List<long>(tList);
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

	// Token: 0x06001710 RID: 5904 RVA: 0x000E5930 File Offset: 0x000E3B30
	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		writer.WriteValue(value);
	}

	// Token: 0x06001711 RID: 5905 RVA: 0x000E5939 File Offset: 0x000E3B39
	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(List<long>);
	}
}
