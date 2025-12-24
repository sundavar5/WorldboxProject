using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200081B RID: 2075
public class WorldLawsWindow : TabbedWindow
{
	// Token: 0x06004102 RID: 16642 RVA: 0x001BBDC5 File Offset: 0x001B9FC5
	protected override void create()
	{
		base.create();
		this.initCursedWorld();
	}

	// Token: 0x06004103 RID: 16643 RVA: 0x001BBDD3 File Offset: 0x001B9FD3
	private void initCursedWorld()
	{
		this._cursed_world_button.init(WorldLawLibrary.world_law_cursed_world);
		this._cursed_world_button.addListener(delegate
		{
			this.checkShakeAndClose();
		});
	}

	// Token: 0x06004104 RID: 16644 RVA: 0x001BBDFC File Offset: 0x001B9FFC
	private void checkShakeAndClose()
	{
		if (CursedSacrifice.justGotCursedWorld())
		{
			World.world.startShake(0.3f, 0.01f, 2f, true, true);
			this.checkForbiddenKnowledgeElements();
		}
		WorldLawsTextInsult.removeInsultTimeout();
		((IShakable)this.scroll_window).shake();
	}

	// Token: 0x06004105 RID: 16645 RVA: 0x001BBE36 File Offset: 0x001BA036
	private void OnEnable()
	{
		this.checkForbiddenKnowledgeElements();
	}

	// Token: 0x06004106 RID: 16646 RVA: 0x001BBE40 File Offset: 0x001BA040
	private void checkForbiddenKnowledgeElements()
	{
		bool tWorldIsCursed = WorldLawLibrary.world_law_cursed_world.isEnabled();
		bool tIsReadyForCurse = CursedSacrifice.isWorldReadyForCURSE();
		this._description_forbidden_knowledge_3_cursed.SetActive(tWorldIsCursed);
		if (tWorldIsCursed)
		{
			this._description_forbidden_knowledge_1_before_sacrifice.SetActive(false);
			this._description_forbidden_knowledge_2_non_cursed.SetActive(false);
		}
		else
		{
			this._description_forbidden_knowledge_1_before_sacrifice.SetActive(!tIsReadyForCurse);
			this._description_forbidden_knowledge_2_non_cursed.SetActive(tIsReadyForCurse);
		}
		this._description_forbidden_knowledge_warn.SetActive(tIsReadyForCurse);
		if (tIsReadyForCurse)
		{
			this._background_star_mark_element.minHeight = 180f;
		}
		else
		{
			this._background_star_mark_element.minHeight = 205f;
		}
		float tBlackholeScale = Mathf.Lerp(0.5f, 1f, CursedSacrifice.getCurseProgressRatioForBlackhole());
		this._blackhole_container.transform.localScale = new Vector3(tBlackholeScale, tBlackholeScale, tBlackholeScale);
		if (tWorldIsCursed)
		{
			this._center_blackhole.sprite = this._blackhole_normal_eye_open;
		}
		else if (tIsReadyForCurse)
		{
			this._center_blackhole.sprite = this._blackhole_normal_eye;
		}
		else
		{
			this._center_blackhole.sprite = this._blackhole_normal;
		}
		this._cursed_world_button.gameObject.SetActive(tIsReadyForCurse);
		this._blackhole_butt.SetActive(tIsReadyForCurse);
	}

	// Token: 0x04002F32 RID: 12082
	[SerializeField]
	private WorldLawElement _cursed_world_button;

	// Token: 0x04002F33 RID: 12083
	[SerializeField]
	private GameObject _blackhole_butt;

	// Token: 0x04002F34 RID: 12084
	[SerializeField]
	private Image _center_blackhole;

	// Token: 0x04002F35 RID: 12085
	[SerializeField]
	private Transform _blackhole_container;

	// Token: 0x04002F36 RID: 12086
	[SerializeField]
	private Sprite _blackhole_normal;

	// Token: 0x04002F37 RID: 12087
	[SerializeField]
	private Sprite _blackhole_normal_eye;

	// Token: 0x04002F38 RID: 12088
	[SerializeField]
	private Sprite _blackhole_normal_eye_open;

	// Token: 0x04002F39 RID: 12089
	[SerializeField]
	private LayoutElement _background_star_mark_element;

	// Token: 0x04002F3A RID: 12090
	[SerializeField]
	private GameObject _description_forbidden_knowledge_1_before_sacrifice;

	// Token: 0x04002F3B RID: 12091
	[SerializeField]
	private GameObject _description_forbidden_knowledge_2_non_cursed;

	// Token: 0x04002F3C RID: 12092
	[SerializeField]
	private GameObject _description_forbidden_knowledge_3_cursed;

	// Token: 0x04002F3D RID: 12093
	[SerializeField]
	private GameObject _description_forbidden_knowledge_warn;
}
