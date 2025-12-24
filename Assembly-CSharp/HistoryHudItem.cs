using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006C4 RID: 1732
public class HistoryHudItem : MonoBehaviour
{
	// Token: 0x06003784 RID: 14212 RVA: 0x00190D38 File Offset: 0x0018EF38
	private void Start()
	{
		this._history_hud = base.GetComponentInParent<HistoryHud>();
		this._canvas_group = base.GetComponent<CanvasGroup>();
		this._canvas_group.alpha = 0f;
		this._button = base.GetComponent<Button>();
		this._rect_transform = base.GetComponent<RectTransform>();
		this._button.onClick.AddListener(delegate()
		{
			if (MapBox.controlsLocked())
			{
				return;
			}
			if (MapBox.isControllingUnit())
			{
				return;
			}
			if (World.world.isAnyPowerSelected())
			{
				return;
			}
			this._remove_timer = 0f;
			this._message.jumpToLocation();
		});
	}

	// Token: 0x06003785 RID: 14213 RVA: 0x00190DA1 File Offset: 0x0018EFA1
	private void OnEnable()
	{
		this._creating = true;
		this._remove_timer = 8f;
		this._removing = false;
		base.GetComponent<CanvasGroup>().alpha = 0f;
	}

	// Token: 0x06003786 RID: 14214 RVA: 0x00190DCC File Offset: 0x0018EFCC
	public bool isRemoving()
	{
		return this._removing;
	}

	// Token: 0x06003787 RID: 14215 RVA: 0x00190DD4 File Offset: 0x0018EFD4
	public void setMessage(WorldLogMessage pMessage)
	{
		this.textField.text = pMessage.getFormatedText(this.textField);
		this.textField.GetComponent<LocalizedText>().checkTextFont(null);
		this.textField.GetComponent<LocalizedText>().checkSpecialLanguages(null);
		if (pMessage.getAsset().path_icon != "")
		{
			Sprite tSprite = SpriteTextureLoader.getSprite(pMessage.getAsset().path_icon);
			this.icon.sprite = tSprite;
		}
		else
		{
			this.icon.gameObject.SetActive(false);
		}
		this._message = pMessage;
	}

	// Token: 0x06003788 RID: 14216 RVA: 0x00190E68 File Offset: 0x0018F068
	public void moveTo(float newBottom)
	{
		this._time_limit = 0f;
		this.target_bottom = newBottom;
	}

	// Token: 0x06003789 RID: 14217 RVA: 0x00190E7C File Offset: 0x0018F07C
	public void moveToAndDestroy(float newBottom)
	{
		this._time_limit = 0f;
		this.target_bottom = newBottom;
		this._remove_timer = 0.5f;
		this._removing = true;
	}

	// Token: 0x0600378A RID: 14218 RVA: 0x00190EA4 File Offset: 0x0018F0A4
	private void Update()
	{
		this.background.raycastTarget = this._history_hud.raycastOn;
		this._rect_transform.sizeDelta = new Vector2(this._rect_transform.sizeDelta.x, 10f);
		if (this._creating)
		{
			if (this._canvas_group.alpha < 1f)
			{
				this._canvas_group.alpha += Time.deltaTime * Config.time_scale_asset.multiplier * 2f;
				return;
			}
			this._creating = false;
			return;
		}
		else
		{
			if (Config.paused || ScrollWindow.isWindowActive() || RewardedAds.isShowing())
			{
				return;
			}
			if (this._time_limit <= 2f)
			{
				this._time_limit += Time.deltaTime;
				this._rect_transform.SetTop(-Mathf.Lerp(this._rect_transform.offsetMax.y, -this.target_bottom, this._time_limit / 2f));
			}
			if (this._removing && this._rect_transform.offsetMax.y > 10f)
			{
				this._history_hud.makeInactive(this);
				return;
			}
			this._remove_timer -= Time.deltaTime;
			if (this._remove_timer <= 0f)
			{
				this._canvas_group.alpha -= Time.deltaTime * 2f;
				if (this._canvas_group.alpha <= 0f)
				{
					this._history_hud.makeInactive(this);
					return;
				}
			}
			return;
		}
	}

	// Token: 0x0600378B RID: 14219 RVA: 0x00191027 File Offset: 0x0018F227
	private void OnDisable()
	{
		this._message.clear();
	}

	// Token: 0x04002927 RID: 10535
	private bool _creating = true;

	// Token: 0x04002928 RID: 10536
	private float _remove_timer = 8f;

	// Token: 0x04002929 RID: 10537
	private CanvasGroup _canvas_group;

	// Token: 0x0400292A RID: 10538
	private Button _button;

	// Token: 0x0400292B RID: 10539
	private WorldLogMessage _message;

	// Token: 0x0400292C RID: 10540
	public Text textField;

	// Token: 0x0400292D RID: 10541
	public Image icon;

	// Token: 0x0400292E RID: 10542
	private RectTransform _rect_transform;

	// Token: 0x0400292F RID: 10543
	public Image background;

	// Token: 0x04002930 RID: 10544
	private bool _removing;

	// Token: 0x04002931 RID: 10545
	private HistoryHud _history_hud;

	// Token: 0x04002932 RID: 10546
	private float _time_limit;

	// Token: 0x04002933 RID: 10547
	internal float target_bottom;
}
