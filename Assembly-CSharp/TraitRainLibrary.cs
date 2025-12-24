using System;

// Token: 0x02000187 RID: 391
[Serializable]
public class TraitRainLibrary : AssetLibrary<TraitRainAsset>
{
	// Token: 0x06000B9D RID: 2973 RVA: 0x000A5D5C File Offset: 0x000A3F5C
	public override void init()
	{
		base.init();
		this.add(new TraitRainAsset
		{
			id = "traits_delta_rain_edit"
		});
		this.t.get_list = (() => PlayerConfig.instance.data.trait_editor_delta);
		this.t.get_state = (() => PlayerConfig.instance.data.trait_editor_delta_state);
		this.t.set_state = delegate(RainState pState)
		{
			PlayerConfig.instance.data.trait_editor_delta_state = pState;
		};
		this.t.path_art = "ui/illustrations/art_trait_rain_delta";
		this.t.path_art_void = "ui/illustrations/art_trait_rain_delta_void";
		this.add(new TraitRainAsset
		{
			id = "traits_gamma_rain_edit"
		});
		this.t.get_list = (() => PlayerConfig.instance.data.trait_editor_gamma);
		this.t.get_state = (() => PlayerConfig.instance.data.trait_editor_gamma_state);
		this.t.set_state = delegate(RainState pState)
		{
			PlayerConfig.instance.data.trait_editor_gamma_state = pState;
		};
		this.t.path_art = "ui/illustrations/art_trait_rain_gamma";
		this.t.path_art_void = "ui/illustrations/art_trait_rain_gamma_void";
		this.add(new TraitRainAsset
		{
			id = "traits_omega_rain_edit"
		});
		this.t.get_list = (() => PlayerConfig.instance.data.trait_editor_omega);
		this.t.get_state = (() => PlayerConfig.instance.data.trait_editor_omega_state);
		this.t.set_state = delegate(RainState pState)
		{
			PlayerConfig.instance.data.trait_editor_omega_state = pState;
		};
		this.t.path_art = "ui/illustrations/art_trait_rain_omega";
		this.t.path_art_void = "ui/illustrations/art_trait_rain_omega_void";
	}
}
