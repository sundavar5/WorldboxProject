using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200048D RID: 1165
public class Tester : MonoBehaviour
{
	// Token: 0x060027E7 RID: 10215 RVA: 0x001417FC File Offset: 0x0013F9FC
	private void init()
	{
		this.events = new List<TestingEvent>();
		this.eventsCivs = new List<TestingEvent>();
		foreach (GodPower tPower in AssetManager.powers.list)
		{
			if (tPower.id[0] != '_')
			{
				TestingEvent tEvent = this.add(new TestingEvent
				{
					type = TestingEventType.RandomClick,
					powerID = tPower.id
				});
				if (tPower.type == PowerActionType.PowerDrawTile)
				{
					this.add(tEvent);
					this.add(tEvent);
					this.add(tEvent);
				}
			}
		}
		this.eventsCivs.Add(new TestingEvent
		{
			powerID = "humans",
			type = TestingEventType.RandomClick
		});
		this.eventsCivs.Add(new TestingEvent
		{
			powerID = "orcs",
			type = TestingEventType.RandomClick
		});
		this.eventsCivs.Add(new TestingEvent
		{
			powerID = "elves",
			type = TestingEventType.RandomClick
		});
		this.eventsCivs.Add(new TestingEvent
		{
			powerID = "dwarfs",
			type = TestingEventType.RandomClick
		});
		this.setTestStage(TestStage.SPAWN_CIVS);
		this.smoke.enabled = false;
		this.fire.enabled = false;
	}

	// Token: 0x060027E8 RID: 10216 RVA: 0x0014195C File Offset: 0x0013FB5C
	private void setTestStage(TestStage pStage)
	{
		this.testStage = pStage;
		switch (this.testStage)
		{
		case TestStage.SPAWN_CIVS:
			this.testStageTimer = 10f;
			return;
		case TestStage.WAIT_CIVS:
			this.testStageTimer = 60f;
			return;
		case TestStage.SPAWN_CHAOS:
			this.testStageTimer = 30f;
			return;
		case TestStage.REGENERATE:
			this.testStageTimer = 1f;
			return;
		default:
			return;
		}
	}

	// Token: 0x060027E9 RID: 10217 RVA: 0x001419BD File Offset: 0x0013FBBD
	private TestingEvent add(TestingEvent pEvent)
	{
		this.events.Add(pEvent);
		return pEvent;
	}

	// Token: 0x060027EA RID: 10218 RVA: 0x001419CC File Offset: 0x0013FBCC
	private void Update()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		if (this.events == null)
		{
			this.init();
			return;
		}
		if (!this.enableRandomSpawn)
		{
			return;
		}
		if (this.timer > 0f)
		{
			this.timer -= Time.deltaTime;
			return;
		}
		if (this.testStageTimer > 0f)
		{
			this.testStageTimer -= Time.deltaTime;
			TestStage testStage = this.testStage;
			TestingEvent tEvent;
			if (testStage != TestStage.SPAWN_CIVS)
			{
				if (testStage != TestStage.SPAWN_CHAOS)
				{
					return;
				}
				tEvent = this.events.GetRandom<TestingEvent>();
			}
			else
			{
				tEvent = this.eventsCivs.GetRandom<TestingEvent>();
			}
			ScrollWindow.hideAllEvent(false);
			if (tEvent == null)
			{
				return;
			}
			TestingEventType type = tEvent.type;
			if (type != TestingEventType.RandomClick)
			{
				return;
			}
			int tX = Randy.randomInt(0, MapBox.width);
			int tY = Randy.randomInt(0, MapBox.height);
			LogText.log(tEvent.powerID, "Test Power", "st");
			if (!AssetManager.powers.dict.ContainsKey(tEvent.powerID))
			{
				MonoBehaviour.print("TESTER ERROR... " + tEvent.powerID);
			}
			GodPower tPower = AssetManager.powers.get(tEvent.powerID);
			if (!tPower.tester_enabled)
			{
				return;
			}
			Config.current_brush = Brush.getRandom();
			World.world.player_control.clickedFinal(new Vector2Int(tX, tY), tPower, true);
			LogText.log(tEvent.powerID, "Test Power", "en");
			return;
		}
		else
		{
			switch (this.testStage)
			{
			case TestStage.SPAWN_CIVS:
				this.setTestStage(TestStage.WAIT_CIVS);
				return;
			case TestStage.WAIT_CIVS:
				this.setTestStage(TestStage.SPAWN_CHAOS);
				return;
			case TestStage.SPAWN_CHAOS:
				this.setTestStage(TestStage.REGENERATE);
				return;
			case TestStage.REGENERATE:
				Config.customZoneX = 7;
				Config.customZoneY = 7;
				World.world.generateNewMap();
				this.testStageTimer = 20f;
				this.setTestStage(TestStage.SPAWN_CIVS);
				return;
			default:
				return;
			}
		}
	}

	// Token: 0x04001E0B RID: 7691
	public GlowParticles smoke;

	// Token: 0x04001E0C RID: 7692
	public GlowParticles fire;

	// Token: 0x04001E0D RID: 7693
	public TestStage testStage;

	// Token: 0x04001E0E RID: 7694
	private List<TestingEvent> events;

	// Token: 0x04001E0F RID: 7695
	private List<TestingEvent> eventsCivs;

	// Token: 0x04001E10 RID: 7696
	private float timer = 1f;

	// Token: 0x04001E11 RID: 7697
	public float testStageTimer = 20f;

	// Token: 0x04001E12 RID: 7698
	public bool enableFastBuilding;

	// Token: 0x04001E13 RID: 7699
	public bool enableRandomSpawn = true;
}
