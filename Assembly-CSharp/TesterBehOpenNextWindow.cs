using System;
using System.Collections.Generic;
using ai.behaviours;
using UnityEngine;

// Token: 0x020004C6 RID: 1222
public class TesterBehOpenNextWindow : BehaviourActionTester
{
	// Token: 0x060029E0 RID: 10720 RVA: 0x00149EE0 File Offset: 0x001480E0
	public TesterBehOpenNextWindow(bool pOnlyMeta = false, bool pRandom = false)
	{
		this._only_meta = pOnlyMeta;
		this._random = pRandom;
	}

	// Token: 0x060029E1 RID: 10721 RVA: 0x00149EF8 File Offset: 0x001480F8
	public override BehResult execute(AutoTesterBot pObject)
	{
		if (this._windows == null)
		{
			this._windows = AssetManager.window_library.getTestableWindows();
			if (this._only_meta)
			{
				this._windows = this._windows.FindAll((WindowAsset pWindow) => pWindow.meta_type_asset != null);
				this._windows = this._windows.FindAll((WindowAsset pWindow) => !pWindow.id.EndsWith("_customize"));
			}
		}
		if (this._random)
		{
			this._current_window = Random.Range(0, this._windows.Count);
		}
		else
		{
			this._current_window = Toolbox.loopIndex(this._current_window + 1, this._windows.Count);
		}
		WindowAsset tWindow = this._windows[this._current_window];
		if (this._only_meta && tWindow.meta_type_asset == null)
		{
			return BehResult.RepeatStep;
		}
		if (tWindow.meta_type_asset != null)
		{
			NanoObject tObject = tWindow.meta_type_asset.get_selected();
			if (tObject == null || !tObject.isAlive())
			{
				return BehResult.RepeatStep;
			}
		}
		Config.debug_window_stats.setCurrent(tWindow.id);
		ScrollWindow.get(tWindow.id).show("right", "right", true, false);
		pObject.wait = 0.1f;
		return BehResult.Continue;
	}

	// Token: 0x04001F34 RID: 7988
	private int _current_window;

	// Token: 0x04001F35 RID: 7989
	private bool _only_meta;

	// Token: 0x04001F36 RID: 7990
	private bool _random;

	// Token: 0x04001F37 RID: 7991
	private List<WindowAsset> _windows;
}
