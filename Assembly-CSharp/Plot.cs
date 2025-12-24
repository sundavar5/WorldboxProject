using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002CE RID: 718
public class Plot : MetaObject<PlotData>
{
	// Token: 0x1700018F RID: 399
	// (get) Token: 0x06001A8A RID: 6794 RVA: 0x000F92E7 File Offset: 0x000F74E7
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Plot;
		}
	}

	// Token: 0x17000190 RID: 400
	// (get) Token: 0x06001A8B RID: 6795 RVA: 0x000F92EB File Offset: 0x000F74EB
	public override BaseSystemManager manager
	{
		get
		{
			return World.world.plots;
		}
	}

	// Token: 0x06001A8C RID: 6796 RVA: 0x000F92F8 File Offset: 0x000F74F8
	public void newPlot(Actor pAuthor, PlotAsset pAsset, bool pForced)
	{
		this.data.plot_type_id = pAsset.id;
		this._plot_asset = pAsset;
		string tName = this._plot_asset.getLocaleID().Localize();
		base.setName(tName, true);
		this.data.forced = pForced;
		this.data.founder_name = pAuthor.getName();
		this.data.founder_id = pAuthor.getID();
		pAuthor.setPlot(this);
		this._plot_author = pAuthor;
	}

	// Token: 0x06001A8D RID: 6797 RVA: 0x000F9374 File Offset: 0x000F7574
	protected sealed override void setDefaultValues()
	{
		base.setDefaultValues();
		this._state = PlotState.Active;
		this._power = 0;
		this._last_index = -1;
		this.transition_animation = 1f;
		this._transition_animation_speed = 0.8f;
		this.progress_target = 0f;
		this.last_update_progress = 0.0;
	}

	// Token: 0x06001A8E RID: 6798 RVA: 0x000F93CC File Offset: 0x000F75CC
	private bool isState(PlotState pState)
	{
		return this._state == pState;
	}

	// Token: 0x06001A8F RID: 6799 RVA: 0x000F93D8 File Offset: 0x000F75D8
	public void finishPlot(PlotState pState, Actor pActor)
	{
		if (this._state == pState)
		{
			return;
		}
		this._state = pState;
		PlotState state = this._state;
		if (state != PlotState.Finished)
		{
			if (state == PlotState.Cancelled)
			{
				World.world.game_stats.data.plotsForgotten += 1L;
				World.world.map_stats.plotsForgotten += 1L;
				if (pActor != null)
				{
					pActor.addStatusEffect("recovery_plot", 0f, true);
				}
			}
		}
		else
		{
			World.world.game_stats.data.plotsSucceeded += 1L;
			World.world.map_stats.plotsSucceeded += 1L;
			if (pActor != null)
			{
				pActor.changeHappiness("just_finished_plot", 0);
				pActor.addStatusEffect("recovery_plot", 0f, true);
			}
		}
		this.startRemovalAnimation();
		foreach (Actor actor in base.units)
		{
			actor.leavePlot();
		}
		this.startAnimation();
		this.data.progress_current = this.getProgressMax();
	}

	// Token: 0x06001A90 RID: 6800 RVA: 0x000F9510 File Offset: 0x000F7710
	public PlotState getState()
	{
		return this._state;
	}

	// Token: 0x06001A91 RID: 6801 RVA: 0x000F9518 File Offset: 0x000F7718
	public override bool isReadyForRemoval()
	{
		return !this.isActive();
	}

	// Token: 0x06001A92 RID: 6802 RVA: 0x000F9524 File Offset: 0x000F7724
	public override void loadData(PlotData pData)
	{
		base.loadData(pData);
		this._plot_asset = AssetManager.plots_library.get(this.data.plot_type_id);
		this.target_city = World.world.cities.get(this.data.id_target_city);
		this.target_kingdom = World.world.kingdoms.get(this.data.id_target_kingdom);
		this.target_alliance = World.world.alliances.get(this.data.id_target_alliance);
		this.target_war = World.world.wars.get(this.data.id_target_war);
	}

	// Token: 0x06001A93 RID: 6803 RVA: 0x000F95D4 File Offset: 0x000F77D4
	public void loadAuthors()
	{
		this._plot_author = World.world.units.get(this.data.founder_id);
		this.target_actor = World.world.units.get(this.data.id_target_actor);
	}

	// Token: 0x06001A94 RID: 6804 RVA: 0x000F9624 File Offset: 0x000F7824
	public override void save()
	{
		base.save();
		PlotData data = this.data;
		Actor actor = this.target_actor;
		data.id_target_actor = ((actor != null) ? actor.data.id : -1L);
		PlotData data2 = this.data;
		City city = this.target_city;
		data2.id_target_city = ((city != null) ? city.data.id : -1L);
		PlotData data3 = this.data;
		Kingdom kingdom = this.target_kingdom;
		data3.id_target_kingdom = ((kingdom != null) ? kingdom.data.id : -1L);
		PlotData data4 = this.data;
		Alliance alliance = this.target_alliance;
		data4.id_target_alliance = ((alliance != null) ? alliance.data.id : -1L);
		PlotData data5 = this.data;
		War war = this.target_war;
		data5.id_target_war = ((war != null) ? war.data.id : -1L);
	}

	// Token: 0x06001A95 RID: 6805 RVA: 0x000F96E6 File Offset: 0x000F78E6
	public void updateAnimations(float pElapsed)
	{
		if (this.transition_animation > 1f)
		{
			this.transition_animation -= Time.deltaTime * this._transition_animation_speed;
			if (this.transition_animation < 1f)
			{
				this.transition_animation = 1f;
			}
		}
	}

	// Token: 0x06001A96 RID: 6806 RVA: 0x000F9728 File Offset: 0x000F7928
	public override ColorAsset getColor()
	{
		Actor tSupporter = this.getAuthor();
		ColorAsset tColorAsset = null;
		if (tSupporter != null)
		{
			tColorAsset = tSupporter.kingdom.getColor();
		}
		return tColorAsset;
	}

	// Token: 0x06001A97 RID: 6807 RVA: 0x000F9750 File Offset: 0x000F7950
	public bool updateProgressTarget(Actor pActor, float pIntelligence)
	{
		this.last_update_progress = World.world.getCurWorldTime();
		this.progress_target += pIntelligence + 1f;
		if (this.progress_target > this.getProgressMax())
		{
			this.progress_target = this.getProgressMax();
		}
		float tMod = this.getProgressMod();
		this._transition_animation_speed = 1.5f * tMod;
		if (this.transition_animation <= 1f)
		{
			this.startAnimation();
		}
		if (this.data.progress_current >= this.getProgressMax())
		{
			if (this._plot_asset.action(pActor))
			{
				PlotAction post_action = this._plot_asset.post_action;
				if (post_action != null)
				{
					post_action(pActor);
				}
			}
			this.finishPlot(PlotState.Finished, pActor);
			return true;
		}
		return false;
	}

	// Token: 0x06001A98 RID: 6808 RVA: 0x000F980C File Offset: 0x000F7A0C
	public void updateProgress(float pElapsed)
	{
		if (this.data.progress_current < this.progress_target)
		{
			this.data.progress_current += pElapsed * 2f;
			if (this.data.progress_current > this.progress_target)
			{
				this.data.progress_current = this.progress_target;
			}
		}
	}

	// Token: 0x06001A99 RID: 6809 RVA: 0x000F986C File Offset: 0x000F7A6C
	public int getProgressPercentage()
	{
		float tTime = this.getProgress();
		float tTimeMax = this.getProgressMax();
		if (tTimeMax == 0f)
		{
			return 0;
		}
		return (int)(tTime / tTimeMax * 100f);
	}

	// Token: 0x06001A9A RID: 6810 RVA: 0x000F989C File Offset: 0x000F7A9C
	public float getProgressMod()
	{
		float progress = this.getProgress();
		float tTimeMax = this.getProgressMax();
		return progress / tTimeMax;
	}

	// Token: 0x06001A9B RID: 6811 RVA: 0x000F98B8 File Offset: 0x000F7AB8
	public bool checkInitiatorAndTargets()
	{
		if (this._plot_asset.check_target_actor && !this.target_actor.isAlive())
		{
			return false;
		}
		if (this._plot_asset.check_target_alliance && !this.target_alliance.isAlive())
		{
			return false;
		}
		if (this._plot_asset.check_target_city && !this.target_city.isAlive())
		{
			return false;
		}
		if (this._plot_asset.check_target_kingdom)
		{
			if (!this.target_kingdom.isAlive())
			{
				return false;
			}
			if (!this.target_kingdom.hasCities())
			{
				return false;
			}
		}
		return !this._plot_asset.check_target_war || this.target_war.isAlive();
	}

	// Token: 0x06001A9C RID: 6812 RVA: 0x000F9961 File Offset: 0x000F7B61
	public PlotAsset getAsset()
	{
		return this._plot_asset;
	}

	// Token: 0x06001A9D RID: 6813 RVA: 0x000F9969 File Offset: 0x000F7B69
	public int getPower()
	{
		return this._power;
	}

	// Token: 0x06001A9E RID: 6814 RVA: 0x000F9971 File Offset: 0x000F7B71
	public int getMaxSupporters()
	{
		return 15;
	}

	// Token: 0x06001A9F RID: 6815 RVA: 0x000F9975 File Offset: 0x000F7B75
	public int getSupporters()
	{
		return base.units.Count;
	}

	// Token: 0x06001AA0 RID: 6816 RVA: 0x000F9982 File Offset: 0x000F7B82
	public float getProgressMax()
	{
		return this._plot_asset.progress_needed;
	}

	// Token: 0x06001AA1 RID: 6817 RVA: 0x000F998F File Offset: 0x000F7B8F
	public float getProgress()
	{
		return this.data.progress_current;
	}

	// Token: 0x06001AA2 RID: 6818 RVA: 0x000F999C File Offset: 0x000F7B9C
	public void startAnimation()
	{
		this.transition_animation = 1.3f;
	}

	// Token: 0x06001AA3 RID: 6819 RVA: 0x000F99AC File Offset: 0x000F7BAC
	public void startRemovalAnimation()
	{
		foreach (Actor tActor in base.units)
		{
			if (tActor.isAlive())
			{
				World.world.stack_effects.plot_removals.Add(new PlotIconData
				{
					actor = tActor,
					sprite = this.getSpritePath(),
					timestamp = World.world.getCurSessionTime()
				});
			}
		}
	}

	// Token: 0x06001AA4 RID: 6820 RVA: 0x000F9A44 File Offset: 0x000F7C44
	public Sprite getSprite()
	{
		return SpriteTextureLoader.getSprite(this.getSpritePath());
	}

	// Token: 0x06001AA5 RID: 6821 RVA: 0x000F9A51 File Offset: 0x000F7C51
	public string getSpritePath()
	{
		if (this.isFinished())
		{
			return "plots/icons/progress/plot_finished";
		}
		if (this.isCancelled())
		{
			return "plots/icons/progress/plot_cancelled";
		}
		return this._plot_asset.path_icon;
	}

	// Token: 0x06001AA6 RID: 6822 RVA: 0x000F9A7C File Offset: 0x000F7C7C
	public Sprite getSpriteIconProgress()
	{
		int tSpriteIndex = (int)(this.getProgress() / this.getProgressMax() * (float)(Plot._total_sprites + 1));
		if (tSpriteIndex > Plot._total_sprites)
		{
			tSpriteIndex = Plot._total_sprites;
		}
		if (Plot._cache_sprites == null)
		{
			Plot._cache_sprites = new Dictionary<int, Sprite>();
			for (int i = 0; i <= Plot._total_sprites; i++)
			{
				Plot._cache_sprites.Add(i, SpriteTextureLoader.getSprite("plots/speech_bubbles/speech_bubble_0" + i.ToString()));
			}
		}
		Sprite result = Plot._cache_sprites[tSpriteIndex];
		if (tSpriteIndex != this._last_index)
		{
			this._last_index = tSpriteIndex;
			this.startAnimation();
		}
		return result;
	}

	// Token: 0x06001AA7 RID: 6823 RVA: 0x000F9B12 File Offset: 0x000F7D12
	public bool hasSupporter(Actor pActor)
	{
		return base.units.Contains(pActor);
	}

	// Token: 0x06001AA8 RID: 6824 RVA: 0x000F9B25 File Offset: 0x000F7D25
	public bool isActive()
	{
		return this.isState(PlotState.Active);
	}

	// Token: 0x06001AA9 RID: 6825 RVA: 0x000F9B2E File Offset: 0x000F7D2E
	public bool isCancelled()
	{
		return this.isState(PlotState.Cancelled);
	}

	// Token: 0x06001AAA RID: 6826 RVA: 0x000F9B37 File Offset: 0x000F7D37
	public bool isFinished()
	{
		return this.isState(PlotState.Finished);
	}

	// Token: 0x06001AAB RID: 6827 RVA: 0x000F9B40 File Offset: 0x000F7D40
	public bool isSameType(PlotAsset pAsset)
	{
		return pAsset == this.getAsset();
	}

	// Token: 0x06001AAC RID: 6828 RVA: 0x000F9B4B File Offset: 0x000F7D4B
	public Actor getAuthor()
	{
		this._plot_author = World.world.units.get(this.data.founder_id);
		return this._plot_author;
	}

	// Token: 0x06001AAD RID: 6829 RVA: 0x000F9B73 File Offset: 0x000F7D73
	public override void clearLastYearStats()
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x06001AAE RID: 6830 RVA: 0x000F9B85 File Offset: 0x000F7D85
	public override void increaseBirths()
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x06001AAF RID: 6831 RVA: 0x000F9B97 File Offset: 0x000F7D97
	public override void increaseDeaths(AttackType pType)
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x06001AB0 RID: 6832 RVA: 0x000F9BA9 File Offset: 0x000F7DA9
	public override void Dispose()
	{
		this.target_city = null;
		this.target_kingdom = null;
		this.target_alliance = null;
		this.target_actor = null;
		this.target_war = null;
		this._plot_asset = null;
		this._plot_author = null;
		base.Dispose();
	}

	// Token: 0x06001AB1 RID: 6833 RVA: 0x000F9BE2 File Offset: 0x000F7DE2
	public bool isAuthorDead()
	{
		return this._plot_author == null || !this._plot_author.isAlive();
	}

	// Token: 0x040014A0 RID: 5280
	public static bool DEBUG_PLOTS = false;

	// Token: 0x040014A1 RID: 5281
	private static Dictionary<int, Sprite> _cache_sprites;

	// Token: 0x040014A2 RID: 5282
	public City target_city;

	// Token: 0x040014A3 RID: 5283
	public Kingdom target_kingdom;

	// Token: 0x040014A4 RID: 5284
	public Alliance target_alliance;

	// Token: 0x040014A5 RID: 5285
	public Actor target_actor;

	// Token: 0x040014A6 RID: 5286
	public War target_war;

	// Token: 0x040014A7 RID: 5287
	private PlotAsset _plot_asset;

	// Token: 0x040014A8 RID: 5288
	private PlotState _state;

	// Token: 0x040014A9 RID: 5289
	private int _power;

	// Token: 0x040014AA RID: 5290
	private static int _total_sprites = 7;

	// Token: 0x040014AB RID: 5291
	private int _last_index;

	// Token: 0x040014AC RID: 5292
	public float transition_animation;

	// Token: 0x040014AD RID: 5293
	private float _transition_animation_speed;

	// Token: 0x040014AE RID: 5294
	private Actor _plot_author;

	// Token: 0x040014AF RID: 5295
	public float progress_target;

	// Token: 0x040014B0 RID: 5296
	public double last_update_progress;
}
