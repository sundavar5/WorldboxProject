using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000449 RID: 1097
public static class ButtonExtensions
{
	// Token: 0x06002604 RID: 9732 RVA: 0x00137CCC File Offset: 0x00135ECC
	public static void TriggerHover(this Button button)
	{
		if (!Input.mousePresent)
		{
			return;
		}
		EventTrigger tTriggerEnter = button.gameObject.GetComponent<EventTrigger>();
		if (tTriggerEnter == null)
		{
			tTriggerEnter = button.gameObject.AddComponent<EventTrigger>();
		}
		tTriggerEnter.OnPointerEnter(new PointerEventData(EventSystem.current));
	}

	// Token: 0x06002605 RID: 9733 RVA: 0x00137D14 File Offset: 0x00135F14
	public static void OnHover(this Button button, UnityAction call)
	{
		if (!Input.mousePresent)
		{
			return;
		}
		EventTrigger tTriggerEnter = button.gameObject.GetComponent<EventTrigger>();
		if (tTriggerEnter == null)
		{
			tTriggerEnter = button.gameObject.AddComponent<EventTrigger>();
		}
		EventTrigger.Entry tPointerEnter = new EventTrigger.Entry();
		tPointerEnter.eventID = EventTriggerType.PointerEnter;
		tPointerEnter.callback.AddListener(delegate(BaseEventData e)
		{
			call();
		});
		tTriggerEnter.triggers.Add(tPointerEnter);
	}

	// Token: 0x06002606 RID: 9734 RVA: 0x00137D88 File Offset: 0x00135F88
	public static void OnHoverOut(this Button button, UnityAction call)
	{
		if (!Input.mousePresent)
		{
			return;
		}
		EventTrigger tTriggerEnter = button.gameObject.GetComponent<EventTrigger>();
		if (tTriggerEnter == null)
		{
			tTriggerEnter = button.gameObject.AddComponent<EventTrigger>();
		}
		EventTrigger.Entry tPointerExit = new EventTrigger.Entry();
		tPointerExit.eventID = EventTriggerType.PointerExit;
		tPointerExit.callback.AddListener(delegate(BaseEventData e)
		{
			call();
		});
		tTriggerEnter.triggers.Add(tPointerExit);
	}

	// Token: 0x06002607 RID: 9735 RVA: 0x00137DFC File Offset: 0x00135FFC
	public static void OnHover(this Slider slider, UnityAction call)
	{
		if (!Input.mousePresent)
		{
			return;
		}
		EventTrigger tTriggerEnter = slider.gameObject.GetComponent<EventTrigger>();
		if (tTriggerEnter == null)
		{
			tTriggerEnter = slider.gameObject.AddComponent<EventTrigger>();
		}
		EventTrigger.Entry tPointerEnter = new EventTrigger.Entry();
		tPointerEnter.eventID = EventTriggerType.PointerEnter;
		tPointerEnter.callback.AddListener(delegate(BaseEventData e)
		{
			call();
		});
		tTriggerEnter.triggers.Add(tPointerEnter);
	}

	// Token: 0x06002608 RID: 9736 RVA: 0x00137E70 File Offset: 0x00136070
	public static void OnHoverOut(this Slider slider, UnityAction call)
	{
		if (!Input.mousePresent)
		{
			return;
		}
		EventTrigger tTriggerEnter = slider.gameObject.GetComponent<EventTrigger>();
		if (tTriggerEnter == null)
		{
			tTriggerEnter = slider.gameObject.AddComponent<EventTrigger>();
		}
		EventTrigger.Entry tPointerExit = new EventTrigger.Entry();
		tPointerExit.eventID = EventTriggerType.PointerExit;
		tPointerExit.callback.AddListener(delegate(BaseEventData e)
		{
			call();
		});
		tTriggerEnter.triggers.Add(tPointerExit);
	}
}
