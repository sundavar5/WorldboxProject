using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x02000265 RID: 613
public class LongJsonConverter : JsonConverter
{
	// Token: 0x17000155 RID: 341
	// (get) Token: 0x06001704 RID: 5892 RVA: 0x000E5543 File Offset: 0x000E3743
	public override bool CanWrite
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000156 RID: 342
	// (get) Token: 0x06001705 RID: 5893 RVA: 0x000E5546 File Offset: 0x000E3746
	public override bool CanRead
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06001706 RID: 5894 RVA: 0x000E5549 File Offset: 0x000E3749
	public static void reset()
	{
		LongJsonConverter.next_long = 100000000L;
		LongJsonConverter.longs.Clear();
	}

	// Token: 0x06001707 RID: 5895 RVA: 0x000E5560 File Offset: 0x000E3760
	public static long getLong(string pString, JsonReader pReader)
	{
		if (string.IsNullOrEmpty(pString))
		{
			return -1L;
		}
		string tString = pString;
		if (pString.IndexOf('_') > 0)
		{
			string[] tSplit = pString.Split('_', StringSplitOptions.None);
			if (tSplit.Length == 2)
			{
				string tPrefix = tSplit[0] + "_";
				if (MapStats.possible_formats.IndexOf(tPrefix) > -1)
				{
					tString = tSplit[1];
				}
			}
		}
		long result;
		if (long.TryParse(tString, out result))
		{
			return result;
		}
		bool tIsGuid = pString.Length == 8 || (pString.Length == 36 && pString[8] == '-' && pString[13] == '-' && pString[18] == '-' && pString[23] == '-');
		long tLong;
		if (!LongJsonConverter.longs.TryGetValue(pString, out tLong))
		{
			long num = LongJsonConverter.next_long;
			LongJsonConverter.next_long = num + 1L;
			tLong = num;
			LongJsonConverter.longs[pString] = tLong;
			if (!tIsGuid)
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					pReader.Path,
					" Failed to parse long <b>",
					pString,
					"</b> ",
					pString.Length.ToString(),
					" -> ",
					tLong.ToString()
				}));
			}
		}
		else if (!tIsGuid)
		{
			Debug.LogWarning(string.Concat(new string[]
			{
				pReader.Path,
				" Failed to parse long <b>",
				pString,
				"</b> ",
				pString.Length.ToString(),
				" -> ",
				tLong.ToString(),
				" already had it"
			}));
		}
		return tLong;
	}

	// Token: 0x06001708 RID: 5896 RVA: 0x000E56F0 File Offset: 0x000E38F0
	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		switch (reader.TokenType)
		{
		case JsonToken.Integer:
			return Convert.ToInt64(reader.Value);
		case JsonToken.String:
			return LongJsonConverter.getLong((string)reader.Value, reader);
		case JsonToken.Null:
			return -1L;
		}
		string[] array = new string[8];
		array[0] = "Unhandled type ";
		array[1] = reader.Path;
		array[2] = " ";
		int num = 3;
		object value = reader.Value;
		array[num] = ((value != null) ? value.ToString() : null);
		array[4] = " ";
		array[5] = reader.TokenType.ToString();
		array[6] = " -> ";
		array[7] = -1L.ToString();
		Debug.LogWarning(string.Concat(array));
		return -1L;
	}

	// Token: 0x06001709 RID: 5897 RVA: 0x000E57CB File Offset: 0x000E39CB
	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		writer.WriteValue(value);
	}

	// Token: 0x0600170A RID: 5898 RVA: 0x000E57D4 File Offset: 0x000E39D4
	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(long);
	}

	// Token: 0x040012DB RID: 4827
	internal static long next_long = 100000000L;

	// Token: 0x040012DC RID: 4828
	internal static Dictionary<string, long> longs = new Dictionary<string, long>();
}
