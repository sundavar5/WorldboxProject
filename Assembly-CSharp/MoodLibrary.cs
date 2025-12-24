using System;

// Token: 0x02000064 RID: 100
public class MoodLibrary : AssetLibrary<MoodAsset>
{
	// Token: 0x06000380 RID: 896 RVA: 0x0001FFE0 File Offset: 0x0001E1E0
	public override void init()
	{
		base.init();
		this.add(new MoodAsset
		{
			id = "sad",
			icon = "iconMoodSad"
		});
		this.t.base_stats["multiplier_speed"] = -0.2f;
		this.t.base_stats["multiplier_diplomacy"] = -0.1f;
		this.t.base_stats["loyalty_mood"] = -5f;
		this.t.base_stats["opinion"] = -5f;
		this.add(new MoodAsset
		{
			id = "normal",
			icon = "iconMoodNormal"
		});
		this.add(new MoodAsset
		{
			id = "happy",
			icon = "iconMoodHappy"
		});
		this.t.base_stats["multiplier_speed"] = 0.1f;
		this.t.base_stats["multiplier_diplomacy"] = 0.1f;
		this.t.base_stats["loyalty_mood"] = 10f;
		this.t.base_stats["opinion"] = 10f;
		this.add(new MoodAsset
		{
			id = "angry",
			icon = "iconMoodAngry"
		});
		this.t.base_stats["multiplier_speed"] = 0.1f;
		this.t.base_stats["multiplier_diplomacy"] = -0.3f;
		this.t.base_stats["loyalty_mood"] = -15f;
		this.t.base_stats["opinion"] = -15f;
		this.add(new MoodAsset
		{
			id = "dark"
		});
		this.t.base_stats["loyalty_mood"] = -20f;
		this.t.base_stats["opinion"] = -20f;
	}
}
