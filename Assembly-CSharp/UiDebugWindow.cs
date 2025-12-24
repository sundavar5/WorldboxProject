using System;
using UnityEngine.UI;

// Token: 0x02000543 RID: 1347
public class UiDebugWindow : TabbedWindow
{
	// Token: 0x06002BFD RID: 11261 RVA: 0x0015B237 File Offset: 0x00159437
	private void OnEnable()
	{
		this.showInfo();
	}

	// Token: 0x06002BFE RID: 11262 RVA: 0x0015B23F File Offset: 0x0015943F
	public void showInfo()
	{
		this.mask.enabled = true;
		AchievementLibrary.god_mode.check(null);
		this.clear();
	}

	// Token: 0x06002BFF RID: 11263 RVA: 0x0015B25F File Offset: 0x0015945F
	private void clear()
	{
	}

	// Token: 0x06002C00 RID: 11264 RVA: 0x0015B261 File Offset: 0x00159461
	public void clickConsole()
	{
		if (Config.game_loaded)
		{
			World.world.console.Show();
		}
	}

	// Token: 0x06002C01 RID: 11265 RVA: 0x0015B279 File Offset: 0x00159479
	public void clickNewDebugWindow()
	{
		DebugConfig.createTool("Game Info", 80, -10, -1);
	}

	// Token: 0x06002C02 RID: 11266 RVA: 0x0015B28A File Offset: 0x0015948A
	public void clickNewDebugWindowBench()
	{
		DebugConfig.createTool("Benchmark All", 80, -10, -1);
	}

	// Token: 0x06002C03 RID: 11267 RVA: 0x0015B29C File Offset: 0x0015949C
	public void clickRandomKingdomColor()
	{
		AssetsDebugManager.newKingdomColors();
		if (this.assets_actor.gameObject.activeSelf)
		{
			this.assets_actor.refresh();
		}
		if (this.assets_building.gameObject.activeSelf)
		{
			this.assets_building.refresh();
		}
	}

	// Token: 0x06002C04 RID: 11268 RVA: 0x0015B2E8 File Offset: 0x001594E8
	public void clickRandomSkinColor()
	{
		AssetsDebugManager.newSkinColors();
		this.assets_actor.refresh();
	}

	// Token: 0x06002C05 RID: 11269 RVA: 0x0015B2FA File Offset: 0x001594FA
	public void clickChangeSex()
	{
		AssetsDebugManager.changeSex();
		this.assets_actor.refresh();
	}

	// Token: 0x06002C06 RID: 11270 RVA: 0x0015B30C File Offset: 0x0015950C
	public void showDebugAvatarsWindow()
	{
		ScrollWindow.showWindow("debug_avatars");
	}

	// Token: 0x040021CD RID: 8653
	public Mask mask;

	// Token: 0x040021CE RID: 8654
	public ActorDebugAssetsComponent assets_actor;

	// Token: 0x040021CF RID: 8655
	public BuildingDebugAssetsComponent assets_building;
}
