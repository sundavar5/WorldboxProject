using System;
using System.Globalization;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine.UI;

// Token: 0x02000497 RID: 1175
public static class TweenExtensions
{
	// Token: 0x0600288E RID: 10382 RVA: 0x00145724 File Offset: 0x00143924
	public static TweenerCore<int, int, NoOptions> DOUpCounter(this Text target, int endValue)
	{
		int v;
		if (!int.TryParse(target.text, NumberStyles.Any, CultureInfo.CurrentCulture, out v))
		{
			v = 0;
		}
		return target.DOUpCounter(v, endValue, 0.45f, "", "");
	}

	// Token: 0x0600288F RID: 10383 RVA: 0x00145764 File Offset: 0x00143964
	public static TweenerCore<int, int, NoOptions> DOUpCounter(this Text target, int endValue, float duration, string pEnding = "", string pColor = "")
	{
		int v;
		if (!int.TryParse(target.text, NumberStyles.Any, CultureInfo.CurrentCulture, out v))
		{
			v = 0;
		}
		return target.DOUpCounter(v, endValue, duration, pEnding, pColor);
	}

	// Token: 0x06002890 RID: 10384 RVA: 0x00145798 File Offset: 0x00143998
	public static TweenerCore<int, int, NoOptions> DOUpCounter(this Text target, int fromValue, int endValue, float duration, string pEnding = "", string pColor = "")
	{
		TweenerCore<int, int, NoOptions> tweenerCore = DOTween.To(() => fromValue, delegate(int x)
		{
			fromValue = x;
			if (pColor != "")
			{
				target.text = Toolbox.coloredText(fromValue.ToText(4) + pEnding, pColor, false);
				return;
			}
			target.text = fromValue.ToText(4) + pEnding;
		}, endValue, duration);
		tweenerCore.SetEase(Ease.OutQuart);
		tweenerCore.SetTarget(target);
		return tweenerCore;
	}

	// Token: 0x06002891 RID: 10385 RVA: 0x00145800 File Offset: 0x00143A00
	public static TweenerCore<float, float, FloatOptions> DOUpCounter(this Text target, float fromValue, float endValue, float duration, string pEnding = "", string pColor = "")
	{
		TweenerCore<float, float, FloatOptions> tweenerCore = DOTween.To(() => fromValue, delegate(float x)
		{
			fromValue = x;
			if (pColor != "")
			{
				target.text = Toolbox.coloredText(fromValue.ToText() + pEnding, pColor, false);
				return;
			}
			target.text = fromValue.ToText() + pEnding;
		}, endValue, duration);
		tweenerCore.SetEase(Ease.InQuint);
		tweenerCore.SetTarget(target);
		return tweenerCore;
	}

	// Token: 0x06002892 RID: 10386 RVA: 0x00145868 File Offset: 0x00143A68
	public static TweenerCore<long, long, NoOptions> DORandomCounter(this Text target, long fromValue, long endValue, float duration)
	{
		long current = fromValue;
		int endLength = endValue.ToString().Length;
		string endVal = endValue.ToString();
		TweenerCore<long, long, NoOptions> tweenerCore = DOTween.To(() => current, delegate(long x)
		{
			current = x;
			string text = "";
			string currentVal = current.ToString();
			bool fRandom = (float)(current - fromValue) < (float)(endValue - fromValue) * 0.95f;
			for (int i = 0; i < endLength; i++)
			{
				if (fRandom)
				{
					text += Randy.randomInt((i == 0) ? 1 : 0, 10).ToString();
				}
				else if (currentVal.Length >= endLength && endVal.Substring(i, 1) == currentVal.Substring(i, 1))
				{
					text += endVal.Substring(i, 1);
				}
				else if (i == 0)
				{
					int maxInt = int.Parse(endVal.Substring(i, 1)) + 1;
					text += ((maxInt < 2) ? 1 : Randy.randomInt(1, (maxInt > 10) ? 10 : maxInt)).ToString();
				}
				else
				{
					text += Randy.randomInt(0, 10).ToString();
				}
			}
			target.text = long.Parse(text).ToText();
		}, endValue, duration);
		tweenerCore.SetEase(Ease.OutQuart);
		tweenerCore.SetTarget(target);
		return tweenerCore;
	}

	// Token: 0x06002893 RID: 10387 RVA: 0x00145900 File Offset: 0x00143B00
	public static TweenerCore<int, int, NoOptions> DORandomCounter(this Text target, int fromValue, int endValue, float duration)
	{
		int current = fromValue;
		int endLength = endValue.ToString().Length;
		string endVal = endValue.ToString();
		TweenerCore<int, int, NoOptions> tweenerCore = DOTween.To(() => current, delegate(int x)
		{
			current = x;
			string text = "";
			string currentVal = current.ToString();
			bool fRandom = (float)(current - fromValue) < (float)(endValue - fromValue) * 0.95f;
			for (int i = 0; i < endLength; i++)
			{
				if (fRandom)
				{
					text += Randy.randomInt((i == 0) ? 1 : 0, 10).ToString();
				}
				else if (currentVal.Length >= endLength && endVal.Substring(i, 1) == currentVal.Substring(i, 1))
				{
					text += endVal.Substring(i, 1);
				}
				else if (i == 0)
				{
					int maxInt = int.Parse(endVal.Substring(i, 1)) + 1;
					text += ((maxInt < 2) ? 1 : Randy.randomInt(1, (maxInt > 10) ? 10 : maxInt)).ToString();
				}
				else
				{
					text += Randy.randomInt(0, 10).ToString();
				}
			}
			target.text = int.Parse(text).ToText();
		}, endValue, duration);
		tweenerCore.SetEase(Ease.OutQuart);
		tweenerCore.SetTarget(target);
		return tweenerCore;
	}

	// Token: 0x06002894 RID: 10388 RVA: 0x00145998 File Offset: 0x00143B98
	public static TweenerCore<float, float, FloatOptions> DOMinHeight(this LayoutElement target, float endValue, float duration, bool snapping = false)
	{
		TweenerCore<float, float, FloatOptions> tweenerCore = DOTween.To(() => target.minHeight, delegate(float x)
		{
			target.minHeight = x;
		}, endValue, duration);
		tweenerCore.SetOptions(snapping).SetTarget(target);
		return tweenerCore;
	}

	// Token: 0x06002895 RID: 10389 RVA: 0x001459E4 File Offset: 0x00143BE4
	public static TweenerCore<float, float, FloatOptions> DOPreferredHeight(this LayoutElement target, float endValue, float duration, bool snapping = false)
	{
		TweenerCore<float, float, FloatOptions> tweenerCore = DOTween.To(() => target.preferredHeight, delegate(float x)
		{
			target.preferredHeight = x;
		}, endValue, duration);
		tweenerCore.SetOptions(snapping).SetTarget(target);
		return tweenerCore;
	}
}
