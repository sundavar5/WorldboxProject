using System;
using System.Collections.Generic;
using UnityEngine.UI;

// Token: 0x020004F6 RID: 1270
public class AutoTesterBot : BaseMapObject
{
	// Token: 0x17000241 RID: 577
	// (get) Token: 0x06002A42 RID: 10818 RVA: 0x0014C08C File Offset: 0x0014A28C
	public Image icon
	{
		get
		{
			if (this._icon == null)
			{
				this._icon = base.transform.Find("Icon").GetComponent<Image>();
			}
			return this._icon;
		}
	}

	// Token: 0x06002A43 RID: 10819 RVA: 0x0014C0BD File Offset: 0x0014A2BD
	internal void clearWorld()
	{
		this.beh_tile_target = null;
		this.beh_asset_target = null;
		this.beh_year_target = 0;
		AiSystemTester aiSystemTester = this.ai;
		if (aiSystemTester == null)
		{
			return;
		}
		aiSystemTester.restartJob();
	}

	// Token: 0x06002A44 RID: 10820 RVA: 0x0014C0E4 File Offset: 0x0014A2E4
	internal override void create()
	{
		if (this.ai != null)
		{
			this.ai.reset();
			this.ai = null;
		}
		base.create();
		this.ai = new AiSystemTester(this);
		this.ai.next_job_delegate = new GetNextJobID(AssetManager.tester_jobs.getNextJob);
		DebugConfig.createTool("Auto Tester", 150, 0, -1);
		this.startAutoTester();
	}

	// Token: 0x06002A45 RID: 10821 RVA: 0x0014C150 File Offset: 0x0014A350
	internal void create(string pJob)
	{
		if (this.ai != null)
		{
			this.ai.reset();
			this.ai = null;
		}
		base.create();
		this.active_tester = pJob;
		List<string> tJobs = new List<string>
		{
			pJob
		};
		this.ai = new AiSystemTester(this);
		this.ai.next_job_delegate = delegate()
		{
			if (tJobs.Count == 0)
			{
				this.active = false;
				this.gameObject.SetActive(false);
				return null;
			}
			return tJobs.Pop<string>();
		};
		this.startAutoTester();
	}

	// Token: 0x06002A46 RID: 10822 RVA: 0x0014C1CC File Offset: 0x0014A3CC
	public override void update(float pElapsed)
	{
		if (!this.active)
		{
			return;
		}
		base.update(pElapsed);
		if (this.wait > 0f)
		{
			this.wait -= pElapsed;
			return;
		}
		this.ai.update();
	}

	// Token: 0x06002A47 RID: 10823 RVA: 0x0014C208 File Offset: 0x0014A408
	private void updateButton()
	{
		if (this.active)
		{
			this.icon.sprite = SpriteTextureLoader.getSprite("ui/Icons/iconPause");
			WorldTip.instance.showToolbarText("Auto tester running");
			return;
		}
		this.icon.sprite = SpriteTextureLoader.getSprite("ui/Icons/iconPlay");
		WorldTip.instance.showToolbarText("Auto tester paused");
	}

	// Token: 0x06002A48 RID: 10824 RVA: 0x0014C266 File Offset: 0x0014A466
	public void startAutoTester()
	{
		this.active = true;
		this.updateButton();
	}

	// Token: 0x06002A49 RID: 10825 RVA: 0x0014C275 File Offset: 0x0014A475
	public void stopAutoTester()
	{
		this.active = false;
		this.updateButton();
	}

	// Token: 0x06002A4A RID: 10826 RVA: 0x0014C284 File Offset: 0x0014A484
	public void toggleAutoTester()
	{
		if (this.ai == null)
		{
			this.create();
		}
		this.active = !this.active;
		this.updateButton();
	}

	// Token: 0x04001F78 RID: 8056
	internal string debugString = "";

	// Token: 0x04001F79 RID: 8057
	internal bool active;

	// Token: 0x04001F7A RID: 8058
	internal AiSystemTester ai;

	// Token: 0x04001F7B RID: 8059
	internal float wait;

	// Token: 0x04001F7C RID: 8060
	internal int beh_year_target;

	// Token: 0x04001F7D RID: 8061
	internal WorldTile beh_tile_target;

	// Token: 0x04001F7E RID: 8062
	internal string beh_asset_target;

	// Token: 0x04001F7F RID: 8063
	internal string active_tester = "";

	// Token: 0x04001F80 RID: 8064
	private Image _icon;
}
