using System;
using UnityEngine;

// Token: 0x02000848 RID: 2120
public class MouseCursor : MonoBehaviour
{
	// Token: 0x06004263 RID: 16995 RVA: 0x001C1414 File Offset: 0x001BF614
	private void Awake()
	{
		if (!Input.mousePresent)
		{
			Object.Destroy(this);
			return;
		}
		MouseCursor._cursors = new Texture2D[]
		{
			this.mouseCursorDefault,
			this.mouseCursorDown,
			this.mouseCursorUp1,
			this.mouseCursorUp2,
			this.mouseCursorUp3,
			this.mouseCursorUp4,
			this.mouseCursorHold,
			this.mouseCursorDrag,
			this.mouseCursorPinkie1,
			this.mouseCursorPinkie2,
			this.mouseCursorPinkie3,
			this.mouseCursorPinkie4,
			this.mouseCursorPinkie5,
			this.mouseCursorSprinkle1,
			this.mouseCursorSprinkle2,
			this.mouseCursorSprinkle3,
			this.mouseCursorSprinkle4,
			this.mouseCursorDraw1,
			this.mouseCursorDraw2,
			this.mouseCursorDraw3,
			this.mouseCursorDraw4,
			this.mouseCursorDraw5,
			this.mouseCursorAttack1,
			this.mouseCursorAttack2,
			this.mouseCursorAttack3,
			this.mouseCursorAttack4,
			this.mouseCursorInspect,
			this.mouseCursorOverUI
		};
	}

	// Token: 0x06004264 RID: 16996 RVA: 0x001C154A File Offset: 0x001BF74A
	private void OnEnable()
	{
		this.setCursorDefault();
	}

	// Token: 0x06004265 RID: 16997 RVA: 0x001C1554 File Offset: 0x001BF754
	private void Update()
	{
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
		{
			if (!MouseCursor._pressed)
			{
				MouseCursor._pressing = 0;
				MouseCursor._dragged = 0;
				MouseCursor._counter = 0;
			}
			MouseCursor._pressed = true;
			MouseCursor._anim_done = false;
			MouseCursor._right = Input.GetMouseButtonDown(1);
			MouseCursor._middle = Input.GetMouseButtonDown(2);
		}
		else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
		{
			if (!Input.anyKey)
			{
				MouseCursor._pressed = false;
				MouseCursor._pressing = 0;
				MouseCursor._dragged = 0;
				MouseCursor._right = Input.GetMouseButtonUp(1);
				MouseCursor._middle = Input.GetMouseButtonUp(2);
			}
			else
			{
				MouseCursor._right = Input.GetMouseButton(1);
				MouseCursor._middle = Input.GetMouseButton(2);
			}
		}
		else if (MouseCursor._pressed)
		{
			if (!Input.anyKey)
			{
				MouseCursor._pressed = false;
				MouseCursor._pressing = 0;
				MouseCursor._dragged = 0;
				MouseCursor._right = false;
				MouseCursor._middle = false;
			}
			else
			{
				MouseCursor._right = Input.GetMouseButton(1);
				MouseCursor._middle = Input.GetMouseButton(2);
				MouseCursor._pressing++;
				if (Config.isDraggingItem())
				{
					MouseCursor._dragged++;
				}
			}
		}
		MouseCursor._can_drag = (World.world.isOverUI() || World.world.canDragMap());
		MouseCursor._selected_unit_attack = ControllableUnit.isControllingUnit();
		MouseCursor._mouse_hold_animation = World.world.getSelectedPowerHoldAnimation();
		if (MouseCursor._selected_unit_attack)
		{
			this.setCursor(22);
		}
		else if (MouseCursor._can_drag && Config.isDraggingItem() && MouseCursor._pressed && MouseCursor._dragged > 3)
		{
			if (MouseCursor._dragged < 10)
			{
				this.setCursor(6);
			}
			else
			{
				this.setCursor(7);
			}
		}
		else if (!MouseCursor._pressed && MouseCursor._anim_done)
		{
			if (this.shouldShowInspectCursor())
			{
				this.setCursorInspect();
			}
			else if (MouseCursor._right)
			{
				this.setCursor(8);
			}
			else
			{
				this.setCursorDefault();
			}
			MouseCursor._counter = 0;
		}
		else if (MouseCursor._can_drag && MouseCursor._pressed && MouseCursor._pressing > 3)
		{
			if (!World.world.isOverUI() || World.world.player_control.isPointerOverUIScroll())
			{
				if (MouseCursor._pressing < 10)
				{
					this.setCursor(6);
				}
				else
				{
					this.setCursor(7);
				}
			}
			else if (MouseCursor._right)
			{
				this.setCursor(8);
			}
			else
			{
				this.setCursor(1);
			}
		}
		else if (!MouseCursor._pressed && this.shouldShowInspectCursor())
		{
			this.setCursorInspect();
		}
		else if (MouseCursor._right)
		{
			int value = Mathf.CeilToInt((float)(MouseCursor._counter / 5));
			if (value > 0 && value < 5)
			{
				this.setCursor(value + 8);
			}
			else
			{
				this.setCursor(8);
			}
			MouseCursor._counter++;
			if (MouseCursor._counter > 40)
			{
				MouseCursor._counter = 0;
				if (!MouseCursor._pressed)
				{
					MouseCursor._anim_done = true;
				}
			}
		}
		else
		{
			MouseHoldAnimation mouse_hold_animation = MouseCursor._mouse_hold_animation;
			int tPauseFrames;
			if (mouse_hold_animation != MouseHoldAnimation.Sprinkle)
			{
				if (mouse_hold_animation != MouseHoldAnimation.Draw)
				{
					tPauseFrames = 6;
					int tValue = Mathf.CeilToInt((float)(MouseCursor._counter / 5));
					if (tValue < 6)
					{
						this.setCursor(tValue);
					}
					else
					{
						this.setCursorDefault();
					}
				}
				else
				{
					tPauseFrames = 5;
					int tValue = Mathf.CeilToInt((float)(MouseCursor._counter / 5)) % 5;
					this.setCursor(17 + tValue);
				}
			}
			else
			{
				tPauseFrames = 4;
				int tValue = Mathf.CeilToInt((float)(MouseCursor._counter / 5)) % 4;
				this.setCursor(13 + tValue);
			}
			MouseCursor._counter++;
			if (MouseCursor._counter >= tPauseFrames * 5 && !MouseCursor._pressed)
			{
				MouseCursor._anim_done = true;
			}
		}
		this.renderCursor();
	}

	// Token: 0x06004266 RID: 16998 RVA: 0x001C18D4 File Offset: 0x001BFAD4
	private void renderCursor()
	{
		int tTextureID = -1;
		if (MouseCursor._selected_cursor_texture != null)
		{
			tTextureID = MouseCursor._selected_cursor_texture.GetHashCode();
		}
		if (MouseCursor._last_texture_id == tTextureID)
		{
			return;
		}
		Cursor.SetCursor(MouseCursor._selected_cursor_texture, Vector2.zero, CursorMode.Auto);
		MouseCursor._last_texture_id = tTextureID;
	}

	// Token: 0x06004267 RID: 16999 RVA: 0x001C191A File Offset: 0x001BFB1A
	private void setCursor(int pCursor = -1)
	{
		MouseCursor._cur_cursor = pCursor;
		MouseCursor._selected_cursor_texture = MouseCursor._cursors[pCursor];
		if (MouseCursor._selected_cursor_texture == null)
		{
			MouseCursor._selected_cursor_texture = this.mouseCursorDefault;
		}
	}

	// Token: 0x06004268 RID: 17000 RVA: 0x001C1948 File Offset: 0x001BFB48
	private bool shouldShowInspectCursor()
	{
		if (UnitSelectionEffect.last_actor != null)
		{
			return true;
		}
		if (World.world.isOverUI())
		{
			return false;
		}
		if (ScrollWindow.isWindowActive())
		{
			return false;
		}
		if (World.world.quality_changer.isLowRes())
		{
			WorldTile tCursorTile = World.world.getMouseTilePosCachedFrame();
			if (tCursorTile == null)
			{
				return false;
			}
			if (Zones.showMapBorders())
			{
				MetaTypeAsset tMetaTypeAsset = World.world.getCachedMapMetaAsset();
				if (tMetaTypeAsset == null)
				{
					return false;
				}
				if (tMetaTypeAsset.check_tile_has_meta(tCursorTile.zone, tMetaTypeAsset, tMetaTypeAsset.getZoneOptionState()))
				{
					return true;
				}
			}
		}
		return World.world.nameplate_manager.isOverNameplate();
	}

	// Token: 0x06004269 RID: 17001 RVA: 0x001C19DD File Offset: 0x001BFBDD
	private void setCursorDefault()
	{
		MapBox world = World.world;
		if (world != null && world.isOverUiButton())
		{
			this.setCursor(27);
			return;
		}
		this.setCursor(0);
	}

	// Token: 0x0600426A RID: 17002 RVA: 0x001C1A02 File Offset: 0x001BFC02
	private void setCursorInspect()
	{
		this.setCursor(26);
	}

	// Token: 0x0600426B RID: 17003 RVA: 0x001C1A0C File Offset: 0x001BFC0C
	public static void debug(DebugTool pTool)
	{
		pTool.setText("_counter:", MouseCursor._counter, 0f, false, 0L, false, false, "");
		pTool.setText("_cur_cursor:", MouseCursor._cur_cursor, 0f, false, 0L, false, false, "");
		string tCursorName;
		switch (MouseCursor._cur_cursor)
		{
		case 0:
			tCursorName = "mouseCursorDefault";
			break;
		case 1:
			tCursorName = "mouseCursorDown";
			break;
		case 2:
			tCursorName = "mouseCursorUp1";
			break;
		case 3:
			tCursorName = "mouseCursorUp2";
			break;
		case 4:
			tCursorName = "mouseCursorUp3";
			break;
		case 5:
			tCursorName = "mouseCursorUp4";
			break;
		case 6:
			tCursorName = "mouseCursorHold";
			break;
		case 7:
			tCursorName = "mouseCursorDrag";
			break;
		case 8:
			tCursorName = "mouseCursorPinkie1";
			break;
		case 9:
			tCursorName = "mouseCursorPinkie2";
			break;
		case 10:
			tCursorName = "mouseCursorPinkie3";
			break;
		case 11:
			tCursorName = "mouseCursorPinkie4";
			break;
		case 12:
			tCursorName = "mouseCursorPinkie5";
			break;
		case 13:
			tCursorName = "mouseCursorSprinkle1";
			break;
		case 14:
			tCursorName = "mouseCursorSprinkle2";
			break;
		case 15:
			tCursorName = "mouseCursorSprinkle3";
			break;
		case 16:
			tCursorName = "mouseCursorSprinkle4";
			break;
		case 17:
			tCursorName = "mouseCursorDraw1";
			break;
		case 18:
			tCursorName = "mouseCursorDraw2";
			break;
		case 19:
			tCursorName = "mouseCursorDraw3";
			break;
		case 20:
			tCursorName = "mouseCursorDraw4";
			break;
		case 21:
			tCursorName = "mouseCursorDraw5";
			break;
		default:
			tCursorName = MouseCursor._cur_cursor.ToString();
			break;
		}
		pTool.setText("_cur_cursor:", tCursorName, 0f, false, 0L, false, false, "");
		pTool.setText("_pressed:", MouseCursor._pressed, 0f, false, 0L, false, false, "");
		pTool.setText("_pressing:", MouseCursor._pressing, 0f, false, 0L, false, false, "");
		pTool.setText("_dragged:", MouseCursor._dragged, 0f, false, 0L, false, false, "");
		pTool.setText("_right:", MouseCursor._right, 0f, false, 0L, false, false, "");
		pTool.setText("_middle:", MouseCursor._middle, 0f, false, 0L, false, false, "");
		pTool.setText("_anim_done:", MouseCursor._anim_done, 0f, false, 0L, false, false, "");
		pTool.setSeparator();
		pTool.setText("_can_drag:", MouseCursor._can_drag, 0f, false, 0L, false, false, "");
		pTool.setText("_hold_animation:", MouseCursor._mouse_hold_animation, 0f, false, 0L, false, false, "");
		pTool.setSeparator();
		pTool.setText("_last_texture_id:", MouseCursor._last_texture_id, 0f, false, 0L, false, false, "");
		pTool.setText("_selected_cursor_texture:", MouseCursor._selected_cursor_texture.GetHashCode().ToString(), 0f, false, 0L, false, false, "");
	}

	// Token: 0x04003070 RID: 12400
	private const int CURSOR_DEFAULT_0 = 0;

	// Token: 0x04003071 RID: 12401
	private const int CURSOR_DOWN_1 = 1;

	// Token: 0x04003072 RID: 12402
	private const int CURSOR_UP_2 = 2;

	// Token: 0x04003073 RID: 12403
	private const int CURSOR_HOLD_6 = 6;

	// Token: 0x04003074 RID: 12404
	private const int CURSOR_DRAG_7 = 7;

	// Token: 0x04003075 RID: 12405
	private const int CURSOR_PINKIE_8 = 8;

	// Token: 0x04003076 RID: 12406
	private const int CURSOR_SPRINKLE_13 = 13;

	// Token: 0x04003077 RID: 12407
	private const int CURSOR_DRAW_17 = 17;

	// Token: 0x04003078 RID: 12408
	private const int CURSOR_ATTACK = 22;

	// Token: 0x04003079 RID: 12409
	private const int CURSOR_INSPECT = 26;

	// Token: 0x0400307A RID: 12410
	private const int CURSOR_OVER_UI = 27;

	// Token: 0x0400307B RID: 12411
	private const int CLICK_FRAMES = 6;

	// Token: 0x0400307C RID: 12412
	private const int UP_FRAMES = 4;

	// Token: 0x0400307D RID: 12413
	private const int PINKIE_FRAMES = 5;

	// Token: 0x0400307E RID: 12414
	private const int SPRINKLE_FRAMES = 4;

	// Token: 0x0400307F RID: 12415
	private const int DRAW_FRAMES = 5;

	// Token: 0x04003080 RID: 12416
	private const int ATTACK_FRAMES = 4;

	// Token: 0x04003081 RID: 12417
	private const int ANIM_SPEED = 5;

	// Token: 0x04003082 RID: 12418
	public Texture2D mouseCursorDefault;

	// Token: 0x04003083 RID: 12419
	public Texture2D mouseCursorDown;

	// Token: 0x04003084 RID: 12420
	public Texture2D mouseCursorUp1;

	// Token: 0x04003085 RID: 12421
	public Texture2D mouseCursorUp2;

	// Token: 0x04003086 RID: 12422
	public Texture2D mouseCursorUp3;

	// Token: 0x04003087 RID: 12423
	public Texture2D mouseCursorUp4;

	// Token: 0x04003088 RID: 12424
	public Texture2D mouseCursorHold;

	// Token: 0x04003089 RID: 12425
	public Texture2D mouseCursorDrag;

	// Token: 0x0400308A RID: 12426
	public Texture2D mouseCursorPinkie1;

	// Token: 0x0400308B RID: 12427
	public Texture2D mouseCursorPinkie2;

	// Token: 0x0400308C RID: 12428
	public Texture2D mouseCursorPinkie3;

	// Token: 0x0400308D RID: 12429
	public Texture2D mouseCursorPinkie4;

	// Token: 0x0400308E RID: 12430
	public Texture2D mouseCursorPinkie5;

	// Token: 0x0400308F RID: 12431
	public Texture2D mouseCursorSprinkle1;

	// Token: 0x04003090 RID: 12432
	public Texture2D mouseCursorSprinkle2;

	// Token: 0x04003091 RID: 12433
	public Texture2D mouseCursorSprinkle3;

	// Token: 0x04003092 RID: 12434
	public Texture2D mouseCursorSprinkle4;

	// Token: 0x04003093 RID: 12435
	public Texture2D mouseCursorDraw1;

	// Token: 0x04003094 RID: 12436
	public Texture2D mouseCursorDraw2;

	// Token: 0x04003095 RID: 12437
	public Texture2D mouseCursorDraw3;

	// Token: 0x04003096 RID: 12438
	public Texture2D mouseCursorDraw4;

	// Token: 0x04003097 RID: 12439
	public Texture2D mouseCursorDraw5;

	// Token: 0x04003098 RID: 12440
	public Texture2D mouseCursorAttack1;

	// Token: 0x04003099 RID: 12441
	public Texture2D mouseCursorAttack2;

	// Token: 0x0400309A RID: 12442
	public Texture2D mouseCursorAttack3;

	// Token: 0x0400309B RID: 12443
	public Texture2D mouseCursorAttack4;

	// Token: 0x0400309C RID: 12444
	public Texture2D mouseCursorInspect;

	// Token: 0x0400309D RID: 12445
	public Texture2D mouseCursorOverUI;

	// Token: 0x0400309E RID: 12446
	private static int _counter = 0;

	// Token: 0x0400309F RID: 12447
	private static bool _pressed = false;

	// Token: 0x040030A0 RID: 12448
	private static int _pressing = 0;

	// Token: 0x040030A1 RID: 12449
	private static int _dragged = 0;

	// Token: 0x040030A2 RID: 12450
	private static bool _right = false;

	// Token: 0x040030A3 RID: 12451
	private static bool _middle = false;

	// Token: 0x040030A4 RID: 12452
	private static bool _anim_done = true;

	// Token: 0x040030A5 RID: 12453
	private static int _cur_cursor = -1;

	// Token: 0x040030A6 RID: 12454
	private static Texture2D[] _cursors;

	// Token: 0x040030A7 RID: 12455
	private static int _last_texture_id = -1;

	// Token: 0x040030A8 RID: 12456
	private static bool _can_drag = false;

	// Token: 0x040030A9 RID: 12457
	private static bool _selected_unit_attack = false;

	// Token: 0x040030AA RID: 12458
	private static MouseHoldAnimation _mouse_hold_animation = MouseHoldAnimation.Default;

	// Token: 0x040030AB RID: 12459
	private static Texture2D _selected_cursor_texture;
}
