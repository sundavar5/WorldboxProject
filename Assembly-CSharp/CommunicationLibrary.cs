using System;

// Token: 0x020000D7 RID: 215
public class CommunicationLibrary : AssetLibrary<CommunicationAsset>
{
	// Token: 0x0600065A RID: 1626 RVA: 0x0005F81C File Offset: 0x0005DA1C
	public override void init()
	{
		base.init();
		CommunicationLibrary.normal = this.add(new CommunicationAsset
		{
			id = "normal",
			icon_path = "speech/speech_bubble",
			show_topic = true
		});
		this.add(new CommunicationAsset
		{
			id = "singing",
			icon_path = "speech/speech_02"
		});
		this.add(new CommunicationAsset
		{
			id = "exclamation",
			icon_path = "speech/speech_03"
		});
		this.add(new CommunicationAsset
		{
			id = "questioning",
			icon_path = "speech/speech_04"
		});
		this.add(new CommunicationAsset
		{
			id = "offensive",
			icon_path = "speech/speech_05"
		});
		this.add(new CommunicationAsset
		{
			id = "defensive",
			icon_path = "speech/speech_06"
		});
		this.add(new CommunicationAsset
		{
			id = "mortality",
			icon_path = "speech/speech_07"
		});
		this.add(new CommunicationAsset
		{
			id = "romantic",
			icon_path = "speech/speech_08"
		});
		this.add(new CommunicationAsset
		{
			id = "pleasant",
			icon_path = "speech/speech_09"
		});
		this.add(new CommunicationAsset
		{
			id = "unpleasant",
			icon_path = "speech/speech_10"
		});
	}

	// Token: 0x0400072B RID: 1835
	public static CommunicationAsset normal;
}
