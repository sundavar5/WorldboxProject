using System;
using System.Collections.Generic;
using ai.behaviours;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020004D7 RID: 1239
public class TesterBehScreenshotTooltips : BehaviourActionTester
{
	// Token: 0x06002A01 RID: 10753 RVA: 0x0014A5F6 File Offset: 0x001487F6
	public TesterBehScreenshotTooltips(bool pScreenshot = true)
	{
		this._screenshot = pScreenshot;
	}

	// Token: 0x06002A02 RID: 10754 RVA: 0x0014A610 File Offset: 0x00148810
	public override BehResult execute(AutoTesterBot pObject)
	{
		string tFolder = TesterBehScreenshotFolder.getScreenshotFolder(LocalizedTextManager.instance.language);
		ScrollWindow tCurrentWindow = ScrollWindow.getCurrentWindow();
		string tWindowId = tCurrentWindow.screen_id;
		RectTransform tViewRect = tCurrentWindow.transform.FindRecursive("Viewport").gameObject.GetComponent<RectTransform>();
		int i = (int)tCurrentWindow.transform.FindRecursive("Content").gameObject.GetComponent<RectTransform>().localPosition.y;
		string tContentPosition = i.ToString("D4");
		switch (this.state)
		{
		case TooltipScreenshotState.Load:
			foreach (Button tButton in tCurrentWindow.gameObject.GetComponentsInChildren<Button>())
			{
				if (tButton.isActiveAndEnabled && tButton.gameObject.activeInHierarchy)
				{
					EventTrigger eventTrigger = tButton.gameObject.GetComponent<EventTrigger>();
					if (!(eventTrigger == null) && !(tButton.name == "Close") && (!(tButton.transform.GetComponentInParent<ScrollWindow>() != null) || tButton.gameObject.GetComponent<RectTransform>().GetWorldRect().Overlaps(tViewRect.GetWorldRect())))
					{
						int tIndex = 0;
						foreach (EventTrigger.Entry tTrigger in eventTrigger.triggers)
						{
							if (tTrigger.eventID == EventTriggerType.PointerEnter)
							{
								this.triggers.Add(new ButtonTrigger(tButton, tTrigger, ++tIndex));
							}
						}
					}
				}
			}
			this.state = TooltipScreenshotState.NextTrigger;
			return BehResult.RepeatStep;
		case TooltipScreenshotState.Screenshot:
			if (!Tooltip.anyActive())
			{
				this.state = TooltipScreenshotState.NextTrigger;
				return BehResult.RepeatStep;
			}
			if (this._screenshot)
			{
				this.screenshots++;
				string tExtension = "";
				if (this.activeTrigger.index > 1)
				{
					string str = "_";
					i = this.activeTrigger.index;
					tExtension = str + i.ToString();
				}
				string tFilename = string.Concat(new string[]
				{
					tWindowId,
					"_",
					tContentPosition,
					"_",
					this.screenshots.ToString("D3"),
					"_",
					this.activeTrigger.button.name,
					tExtension,
					"_5"
				});
				ScreenCapture.CaptureScreenshot(tFolder + "/" + tFilename + ".png");
			}
			this.state = TooltipScreenshotState.Cleanup;
			return BehResult.RepeatStep;
		case TooltipScreenshotState.Cleanup:
			Tooltip.hideTooltipNow();
			this.state = TooltipScreenshotState.NextTrigger;
			return BehResult.RepeatStep;
		case TooltipScreenshotState.NextTrigger:
			if (this.triggers.Count == 0)
			{
				this.state = TooltipScreenshotState.Finish;
				return BehResult.RepeatStep;
			}
			this.activeTrigger = this.triggers.Shift<ButtonTrigger>();
			if (!this.activeTrigger.button.isActiveAndEnabled)
			{
				Debug.LogWarning("button was already disabled: " + this.activeTrigger.button.name, this.activeTrigger.button);
				return BehResult.RepeatStep;
			}
			this.activeTrigger.entry.callback.Invoke(new BaseEventData(EventSystem.current));
			this.state = TooltipScreenshotState.Screenshot;
			pObject.wait = 0.01f;
			return BehResult.RepeatStep;
		case TooltipScreenshotState.Finish:
			this.state = TooltipScreenshotState.Load;
			this.screenshots = 0;
			this.activeTrigger = default(ButtonTrigger);
			this.triggers.Clear();
			return BehResult.Continue;
		default:
			Debug.LogError("TesterBehScreenshotTooltips: Unknown state: " + this.state.ToString());
			return BehResult.Stop;
		}
	}

	// Token: 0x04001F4A RID: 8010
	private int screenshots;

	// Token: 0x04001F4B RID: 8011
	private TooltipScreenshotState state;

	// Token: 0x04001F4C RID: 8012
	private List<ButtonTrigger> triggers = new List<ButtonTrigger>();

	// Token: 0x04001F4D RID: 8013
	private ButtonTrigger activeTrigger;

	// Token: 0x04001F4E RID: 8014
	private bool _screenshot;
}
