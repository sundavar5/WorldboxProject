using System;
using Newtonsoft.Json;

// Token: 0x02000520 RID: 1312
public class BrushPixelDataConverter : JsonConverter
{
	// Token: 0x06002B06 RID: 11014 RVA: 0x00155A38 File Offset: 0x00153C38
	public override void WriteJson(JsonWriter pWriter, object pValue, JsonSerializer pSerializer)
	{
		BrushPixelData tBrushPixelData = (BrushPixelData)pValue;
		string tString = string.Concat(new string[]
		{
			tBrushPixelData.x.ToString(),
			",",
			tBrushPixelData.y.ToString(),
			",",
			tBrushPixelData.dist.ToString()
		});
		pSerializer.Serialize(pWriter, tString, typeof(string));
	}

	// Token: 0x06002B07 RID: 11015 RVA: 0x00155AA8 File Offset: 0x00153CA8
	public override object ReadJson(JsonReader pReader, Type pObjectType, object pExistingValue, JsonSerializer pSerializer)
	{
		string tString = pSerializer.Deserialize<string>(pReader);
		if (string.IsNullOrEmpty(tString))
		{
			return null;
		}
		int[] tArray = Array.ConvertAll<string, int>(tString.Split(',', StringSplitOptions.None), new Converter<string, int>(int.Parse));
		return new BrushPixelData(tArray[0], tArray[1], tArray[2]);
	}

	// Token: 0x06002B08 RID: 11016 RVA: 0x00155AF6 File Offset: 0x00153CF6
	public override bool CanConvert(Type pObjectType)
	{
		return pObjectType != null && pObjectType == typeof(BrushPixelData);
	}
}
