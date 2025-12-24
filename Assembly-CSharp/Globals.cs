using System;
using UnityEngine;

// Token: 0x020001D0 RID: 464
public class Globals
{
	// Token: 0x04000D98 RID: 3480
	public const string HIDDEN_ID = "[HIDDEN]";

	// Token: 0x04000D99 RID: 3481
	public const int TOTAL_AGE_SLOTS = 8;

	// Token: 0x04000D9A RID: 3482
	public const long NEUTRAL_KINGDOM_NUMERIC_ID = 0L;

	// Token: 0x04000D9B RID: 3483
	public const int PAST_RULERS_LIMIT = 30;

	// Token: 0x04000D9C RID: 3484
	public const int TOTAL_RANKS = 10;

	// Token: 0x04000D9D RID: 3485
	public const float UNIT_COLOR_EFFECT_TIME = 0.3f;

	// Token: 0x04000D9E RID: 3486
	public const float PLOT_REMOVAL_EFFECT_TIME = 1f;

	// Token: 0x04000D9F RID: 3487
	public const int CITY_ITEMS_MAX = 15;

	// Token: 0x04000DA0 RID: 3488
	public const int CITY_MIN_ISLAND_TILES = 300;

	// Token: 0x04000DA1 RID: 3489
	public const int CITY_MIN_CAN_BE_FARMS_TILES = 5;

	// Token: 0x04000DA2 RID: 3490
	public const float CITY_TIMER_SUPPLY_DEFAULT = 30f;

	// Token: 0x04000DA3 RID: 3491
	public const float REGION_UPDATE_TIMEOUT = 0.5f;

	// Token: 0x04000DA4 RID: 3492
	public const int DEFAULT_CONSTRUCTION_SPEED = 2;

	// Token: 0x04000DA5 RID: 3493
	public const int HAPPINESS_FROM_BREEDING_ACTION = 40;

	// Token: 0x04000DA6 RID: 3494
	public const float FAST_SWIMMING_SPEED_MULTIPLIER = 5f;

	// Token: 0x04000DA7 RID: 3495
	public const int CULTURE_SPREAD_CHANCE_FROM_BOOK = 3;

	// Token: 0x04000DA8 RID: 3496
	public const int CULTURE_SPREAD_CHANCE_FROM_TALK = 3;

	// Token: 0x04000DA9 RID: 3497
	public const int LANGUAGE_SPREAD_CHANCE_FROM_TALK = 3;

	// Token: 0x04000DAA RID: 3498
	public const int LANGUAGE_SPREAD_CHANCE_FROM_BOOK = 3;

	// Token: 0x04000DAB RID: 3499
	public const int RELIGION_SPREAD_CHANCE_FROM_TALK = 3;

	// Token: 0x04000DAC RID: 3500
	public const int RELIGION_SPREAD_CHANCE_FROM_BOOK = 3;

	// Token: 0x04000DAD RID: 3501
	public const int CITY_POP_FOR_SETTLE_TARGET = 22;

	// Token: 0x04000DAE RID: 3502
	public static readonly bool AI_TEST_ACTIVE = true;

	// Token: 0x04000DAF RID: 3503
	public const float DEFAULT_SCALE_EFFECT = 0.25f;

	// Token: 0x04000DB0 RID: 3504
	public const float DEFAULT_SCALE_UNIT = 0.1f;

	// Token: 0x04000DB1 RID: 3505
	public const float DEFAULT_SCALE_BUILDING = 0.25f;

	// Token: 0x04000DB2 RID: 3506
	public const float DEFAULT_ARROW_START_HEIGHT = 0.6f;

	// Token: 0x04000DB3 RID: 3507
	public const float DEFAULT_PROJECTILE_START_HEIGHT = 0.2f;

	// Token: 0x04000DB4 RID: 3508
	public const float DEFAULT_SLASH_POSITION = 0.5f;

	// Token: 0x04000DB5 RID: 3509
	public const float DEFAULT_UNIT_SPEED = 10f;

	// Token: 0x04000DB6 RID: 3510
	public const float DEFAULT_UNIT_SPEED_MIN = 4f;

	// Token: 0x04000DB7 RID: 3511
	public const float SPEED_MULTIPLIER_IN_LIQUID_WITHOUT_STAMINA = 0.4f;

	// Token: 0x04000DB8 RID: 3512
	public const float ACCURACY_MIN = 1f;

	// Token: 0x04000DB9 RID: 3513
	public const float ACCURACY_MAX = 10f;

	// Token: 0x04000DBA RID: 3514
	public const float FIRE_EXTINGUISH_CHANCE_FROM_RAIN = 0.9f;

	// Token: 0x04000DBB RID: 3515
	public static readonly bool DIAGNOSTIC = false;

	// Token: 0x04000DBC RID: 3516
	public static readonly bool TRAILER_MODE = false;

	// Token: 0x04000DBD RID: 3517
	public static bool TRAILER_MODE_USE_RESOURCES = true;

	// Token: 0x04000DBE RID: 3518
	public static bool TRAILER_MODE_UPGRADE_BUILDINGS = true;

	// Token: 0x04000DBF RID: 3519
	public const int BOAT_TYPE_LIMIT_PER_DOCK = 1;

	// Token: 0x04000DC0 RID: 3520
	public const int CAST_HEIGHT = 15;

	// Token: 0x04000DC1 RID: 3521
	public const int UNITS_IN_SPAWNER_PER_REGION = 4;

	// Token: 0x04000DC2 RID: 3522
	public const int PATHFINDER_REGION_LIMIT = 4;

	// Token: 0x04000DC3 RID: 3523
	public const float DAYS_IN_YEAR = 360f;

	// Token: 0x04000DC4 RID: 3524
	public const float DAYS_IN_MONTH = 30f;

	// Token: 0x04000DC5 RID: 3525
	public const float MONTHS_IN_YEAR = 12f;

	// Token: 0x04000DC6 RID: 3526
	public const float MONTH_TIME = 5f;

	// Token: 0x04000DC7 RID: 3527
	public const float YEAR_TIME = 60f;

	// Token: 0x04000DC8 RID: 3528
	public const int ATTRIBUTE_BAD = 4;

	// Token: 0x04000DC9 RID: 3529
	public const int ATTRIBUTE_NORMAL = 9;

	// Token: 0x04000DCA RID: 3530
	public const int ATTRIBUTE_GOOD = 20;

	// Token: 0x04000DCB RID: 3531
	public const int BUILDING_TOWER_CAPTURE_POINTS = 10;

	// Token: 0x04000DCC RID: 3532
	public const int GENERATOR_MAX_TREES_PER_ZONE = 3;

	// Token: 0x04000DCD RID: 3533
	public const float MIN_ADS_INTERVAL_MINUTES = 2f;

	// Token: 0x04000DCE RID: 3534
	public const float ADS_INTERVAL_MINUTES = 5f;

	// Token: 0x04000DCF RID: 3535
	public static readonly bool specialAbstudio = false;

	// Token: 0x04000DD0 RID: 3536
	public const float SAME_GRASS_CHANCE = 0.05f;

	// Token: 0x04000DD1 RID: 3537
	public const int GREY_GOO_DAMAGE = 50;

	// Token: 0x04000DD2 RID: 3538
	public const int DAMAGE_ACID = 20;

	// Token: 0x04000DD3 RID: 3539
	public const int DAMAGE_HEAT = 50;

	// Token: 0x04000DD4 RID: 3540
	public const int MINIMUM_UNITS_IN_CITY_BEFORE_POP_POINTS = 50;

	// Token: 0x04000DD5 RID: 3541
	public const int CITIZEN_FOOD_COST = 1;

	// Token: 0x04000DD6 RID: 3542
	public const int ZONES_BETWEEN_CITIES = 3;

	// Token: 0x04000DD7 RID: 3543
	public const int BOMB_RANGE_ATOMIC_NUKE = 30;

	// Token: 0x04000DD8 RID: 3544
	public const int BOMB_RANGE_CZAR_BOMB = 70;

	// Token: 0x04000DD9 RID: 3545
	public const float SHAKE_DURATION = 0.3f;

	// Token: 0x04000DDA RID: 3546
	public const float SHAKE_INTERVAL = 0.01f;

	// Token: 0x04000DDB RID: 3547
	public const float SHAKE_INTENSITY = 2f;

	// Token: 0x04000DDC RID: 3548
	public const bool SHAKE_X = false;

	// Token: 0x04000DDD RID: 3549
	public const bool SHAKE_Y = true;

	// Token: 0x04000DDE RID: 3550
	public const float COUNTER_TWEEN_DURATION = 0.45f;

	// Token: 0x04000DDF RID: 3551
	public const int ANIMAL_BABY_MAKING_UNITS_AROUND_LIMIT = 6;

	// Token: 0x04000DE0 RID: 3552
	public const int FOOD_FROM_FISH = 1;

	// Token: 0x04000DE1 RID: 3553
	public const int FOOD_FROM_TILE = 1;

	// Token: 0x04000DE2 RID: 3554
	public const int FOOD_FROM_FARM = 2;

	// Token: 0x04000DE3 RID: 3555
	public const int FARM_DISTANCE_AREA = 9;

	// Token: 0x04000DE4 RID: 3556
	public const int ANIMAL_GOOD_FOR_HUNTING_AGE = 3;

	// Token: 0x04000DE5 RID: 3557
	public const int MAX_UNIT_LEVEL = 9999;

	// Token: 0x04000DE6 RID: 3558
	public const int BOAT_UNITS_LIMIT = 100;

	// Token: 0x04000DE7 RID: 3559
	public const int TRANSPORT_WAIT_TRY_LIMIT = 4;

	// Token: 0x04000DE8 RID: 3560
	public const float COLORS_ZONE_ALPHA = 0.78f;

	// Token: 0x04000DE9 RID: 3561
	public const int ISLAND_TILES_FOR_DOCKS = 2500;

	// Token: 0x04000DEA RID: 3562
	public const int FOOD_BUSH_REGROW_TIME = 90;

	// Token: 0x04000DEB RID: 3563
	public const int FOOD_BEEHIVE_FULL = 10;

	// Token: 0x04000DEC RID: 3564
	internal const int REWARD_MINUTES = 30;

	// Token: 0x04000DED RID: 3565
	internal const int REWARD_MINUTES_CLOCK = 720;

	// Token: 0x04000DEE RID: 3566
	internal const int REWARD_HOURS_CLOCK = 12;

	// Token: 0x04000DEF RID: 3567
	internal const int REWARD_DURATION = 1800;

	// Token: 0x04000DF0 RID: 3568
	internal const int REWARD_DURATION_CLOCK = 43200;

	// Token: 0x04000DF1 RID: 3569
	internal const int REWARD_SAVESLOT_HOURS = 3;

	// Token: 0x04000DF2 RID: 3570
	internal const int REWARD_SAVESLOT_DURATION = 10800;

	// Token: 0x04000DF3 RID: 3571
	internal const int REWARDS_PER_GIFT = 3;

	// Token: 0x04000DF4 RID: 3572
	internal const int FREE_SAVE_SLOTS = 15;

	// Token: 0x04000DF5 RID: 3573
	internal const int REWARD_AD_SAVESLOTS_OLD = 6;

	// Token: 0x04000DF6 RID: 3574
	internal const int REWARD_AD_SAVESLOTS_10HRS = 3;

	// Token: 0x04000DF7 RID: 3575
	internal const int REWARD_AD_SAVESLOTS_20HRS = 6;

	// Token: 0x04000DF8 RID: 3576
	internal const int DISCORD_MEMBERS = 560000;

	// Token: 0x04000DF9 RID: 3577
	internal const int FACEBOOK_MEMBERS = 82000;

	// Token: 0x04000DFA RID: 3578
	internal const int TWITTER_MEMBERS = 56000;

	// Token: 0x04000DFB RID: 3579
	internal const int REDDIT_MEMBERS = 140000;

	// Token: 0x04000DFC RID: 3580
	internal const string WB_EXAMPLE_MAP = "WB-5555-1166-5555";

	// Token: 0x04000DFD RID: 3581
	public const int TIMEOUT_CONQUER = 300;

	// Token: 0x04000DFE RID: 3582
	public static readonly Vector3 POINT_IN_VOID = new Vector3(-1000000f, -1000000f);

	// Token: 0x04000DFF RID: 3583
	public static readonly Vector2 POINT_IN_VOID_2 = new Vector2(-1000000f, -1000000f);

	// Token: 0x04000E00 RID: 3584
	public const string NORMAL = "0";

	// Token: 0x04000E01 RID: 3585
	public const int RATE_US_ID = 12;

	// Token: 0x04000E02 RID: 3586
	public const string voteLink = "https://play.google.com/store/apps/details?id=com.mkarpenko.worldbox";

	// Token: 0x04000E03 RID: 3587
	internal static Vector3 emptyVector = new Vector3(-100000f, -10000f);

	// Token: 0x04000E04 RID: 3588
	public const int TOTAL_WORLD_AGE_SLOTS = 8;
}
