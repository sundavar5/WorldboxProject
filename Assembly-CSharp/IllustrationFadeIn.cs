using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000680 RID: 1664
public class IllustrationFadeIn : MonoBehaviour
{
	// Token: 0x06003581 RID: 13697 RVA: 0x00188F1C File Offset: 0x0018711C
	private void Awake()
	{
		Button tButton;
		if (!base.TryGetComponent<Button>(out tButton))
		{
			tButton = base.gameObject.AddComponent<Button>();
		}
		tButton.onClick.AddListener(new UnityAction(this.onCLick));
		base.GetComponent<Image>().raycastTarget = true;
	}

	// Token: 0x06003582 RID: 13698 RVA: 0x00188F62 File Offset: 0x00187162
	private void OnEnable()
	{
		this.startTween();
	}

	// Token: 0x06003583 RID: 13699 RVA: 0x00188F6C File Offset: 0x0018716C
	public void startTween()
	{
		Vector3 tStartScale = new Vector3(this.scale_start, this.scale_start, this.scale_start);
		Vector3 tEndScale = new Vector3(this.scale_end, this.scale_end, this.scale_end);
		base.transform.DOKill(false);
		base.transform.DOScale(tEndScale, this.duration).From(tStartScale, true, false).SetEase(this.ease_type);
	}

	// Token: 0x06003584 RID: 13700 RVA: 0x00188FDE File Offset: 0x001871DE
	public void onCLick()
	{
		this.startTween();
	}

	// Token: 0x040027E7 RID: 10215
	public float scale_start = 1.5f;

	// Token: 0x040027E8 RID: 10216
	public float scale_end = 1f;

	// Token: 0x040027E9 RID: 10217
	public float duration = 1f;

	// Token: 0x040027EA RID: 10218
	public Ease ease_type = Ease.OutQuart;
}
