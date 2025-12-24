using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200077E RID: 1918
public class TooltipDebugHelper
{
	// Token: 0x06003CD9 RID: 15577 RVA: 0x001A58F0 File Offset: 0x001A3AF0
	public static void checkCreate()
	{
		if (!DebugConfig.isOn(DebugOption.DebugTooltipUI))
		{
			return;
		}
		MapBox.on_world_loaded = (Action)Delegate.Combine(MapBox.on_world_loaded, new Action(TooltipDebugHelper.loadButtons));
		HotkeyAsset cancel = HotkeyLibrary.cancel;
		cancel.just_pressed_action = (HotkeyAction)Delegate.Combine(cancel.just_pressed_action, new HotkeyAction(TooltipDebugHelper.killButtons));
	}

	// Token: 0x06003CDA RID: 15578 RVA: 0x001A5950 File Offset: 0x001A3B50
	public static void killButtons(HotkeyAsset pAsset)
	{
		Object.Destroy(TooltipDebugHelper._debug_canvas);
		TooltipDebugHelper._debug_canvas = null;
	}

	// Token: 0x06003CDB RID: 15579 RVA: 0x001A5964 File Offset: 0x001A3B64
	public static void loadButtons()
	{
		TooltipDebugHelper._debug_canvas = new GameObject("Canvas Debug", new Type[]
		{
			typeof(RectTransform)
		});
		RectTransform tDebugTransform = TooltipDebugHelper._debug_canvas.GetComponent<RectTransform>();
		tDebugTransform.SetParent(CanvasMain.instance.canvas_ui.transform, true);
		tDebugTransform.anchorMin = new Vector2(0f, 0f);
		tDebugTransform.anchorMax = new Vector2(1f, 1f);
		tDebugTransform.offsetMin = new Vector2(0f, 0f);
		tDebugTransform.offsetMax = new Vector2(0f, 0f);
		tDebugTransform.localScale = new Vector3(1f, 1f, 1f);
		GridLayoutGroup gridLayoutGroup = tDebugTransform.AddComponent<GridLayoutGroup>();
		gridLayoutGroup.cellSize = new Vector2(28f, 28f);
		gridLayoutGroup.spacing = new Vector2(2f, 2f);
		using (ListPool<PowerButton> tButtons = new ListPool<PowerButton>(PowerButton.power_buttons.Count + PowerButton.toggle_buttons.Count))
		{
			tButtons.AddRange(PowerButton.power_buttons);
			tButtons.AddRange(PowerButton.toggle_buttons);
			for (int i = 0; i < 9; i++)
			{
				tButtons.Shuffle<PowerButton>();
				foreach (PowerButton ptr in tButtons)
				{
					PowerButton tButton = ptr;
					tButton.gameObject.SetActive(false);
					PowerButton powerButton = Object.Instantiate<PowerButton>(tButton, tDebugTransform);
					powerButton.transform.name = tButton.transform.name;
					powerButton.destroyLockIcon();
					IconRotationAnimation iconRotationAnimation = powerButton.gameObject.AddComponent<IconRotationAnimation>();
					iconRotationAnimation.delay = Randy.randomFloat(1f, 10f);
					iconRotationAnimation.randomDelay = true;
					tButton.gameObject.SetActive(true);
					powerButton.gameObject.SetActive(true);
				}
			}
		}
	}

	// Token: 0x04002C62 RID: 11362
	private static GameObject _debug_canvas;
}
