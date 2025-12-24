using System;
using Newtonsoft.Json;

// Token: 0x0200058B RID: 1419
public static class JsonHelper
{
	// Token: 0x17000266 RID: 614
	// (get) Token: 0x06002F10 RID: 12048 RVA: 0x0016C38D File Offset: 0x0016A58D
	public static JsonSerializer writer
	{
		get
		{
			if (JsonHelper._writer == null)
			{
				JsonHelper._writer = JsonSerializer.Create(new JsonSerializerSettings
				{
					DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate
				});
			}
			return JsonHelper._writer;
		}
	}

	// Token: 0x17000267 RID: 615
	// (get) Token: 0x06002F11 RID: 12049 RVA: 0x0016C3B1 File Offset: 0x0016A5B1
	public static JsonSerializer reader
	{
		get
		{
			if (JsonHelper._reader == null)
			{
				JsonHelper._reader = JsonSerializer.Create(JsonHelper.read_settings);
			}
			return JsonHelper._reader;
		}
	}

	// Token: 0x17000268 RID: 616
	// (get) Token: 0x06002F12 RID: 12050 RVA: 0x0016C3D0 File Offset: 0x0016A5D0
	public static JsonSerializerSettings read_settings
	{
		get
		{
			if (JsonHelper._settings == null)
			{
				JsonHelper._settings = new JsonSerializerSettings();
				JsonHelper._settings.DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate;
				JsonHelper._settings.Converters.Add(new LongJsonConverter());
				JsonHelper._settings.Converters.Add(new LongListJsonConverter());
				JsonHelper._settings.Converters.Add(new NullableLongJsonConverter());
				JsonHelper._settings.Converters.Add(new NullableLongListJsonConverter());
			}
			return JsonHelper._settings;
		}
	}

	// Token: 0x0400230C RID: 8972
	private static JsonSerializer _writer;

	// Token: 0x0400230D RID: 8973
	private static JsonSerializer _reader;

	// Token: 0x0400230E RID: 8974
	private static JsonSerializerSettings _settings;
}
