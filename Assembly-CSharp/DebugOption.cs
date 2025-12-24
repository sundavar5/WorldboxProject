using System;

// Token: 0x02000531 RID: 1329
public enum DebugOption
{
	// Token: 0x0400208A RID: 8330
	Nothing,
	// Token: 0x0400208B RID: 8331
	DisablePremium,
	// Token: 0x0400208C RID: 8332
	TestAds,
	// Token: 0x0400208D RID: 8333
	Connections,
	// Token: 0x0400208E RID: 8334
	LastPath,
	// Token: 0x0400208F RID: 8335
	CursorChunk,
	// Token: 0x04002090 RID: 8336
	Region,
	// Token: 0x04002091 RID: 8337
	ConnectedZones,
	// Token: 0x04002092 RID: 8338
	RegionNeighbours,
	// Token: 0x04002093 RID: 8339
	CityZones,
	// Token: 0x04002094 RID: 8340
	Chunks,
	// Token: 0x04002095 RID: 8341
	ChunksDirty,
	// Token: 0x04002096 RID: 8342
	RegionsDirty,
	// Token: 0x04002097 RID: 8343
	CityPlaces,
	// Token: 0x04002098 RID: 8344
	CitySettleCalc,
	// Token: 0x04002099 RID: 8345
	ChunkEdges,
	// Token: 0x0400209A RID: 8346
	ChunkBounds,
	// Token: 0x0400209B RID: 8347
	PathRegions,
	// Token: 0x0400209C RID: 8348
	ActivePaths,
	// Token: 0x0400209D RID: 8349
	Greg,
	// Token: 0x0400209E RID: 8350
	Graphy,
	// Token: 0x0400209F RID: 8351
	UnlockAllTraits,
	// Token: 0x040020A0 RID: 8352
	UnlockAllEquipment,
	// Token: 0x040020A1 RID: 8353
	UnlockAllGenes,
	// Token: 0x040020A2 RID: 8354
	UnlockAllActors,
	// Token: 0x040020A3 RID: 8355
	UnlockAllPlots,
	// Token: 0x040020A4 RID: 8356
	Buildings,
	// Token: 0x040020A5 RID: 8357
	FastSpawn,
	// Token: 0x040020A6 RID: 8358
	UltraFastSpawn,
	// Token: 0x040020A7 RID: 8359
	SonicSpeed,
	// Token: 0x040020A8 RID: 8360
	KillAllUnitsOnLoad,
	// Token: 0x040020A9 RID: 8361
	PauseOnStart,
	// Token: 0x040020AA RID: 8362
	MemoryCaptureOnClearWorld,
	// Token: 0x040020AB RID: 8363
	ExportAssetLibraries,
	// Token: 0x040020AC RID: 8364
	GenerateGameplayReport,
	// Token: 0x040020AD RID: 8365
	PauseDrops,
	// Token: 0x040020AE RID: 8366
	PauseEffects,
	// Token: 0x040020AF RID: 8367
	DebugZones,
	// Token: 0x040020B0 RID: 8368
	TargetedBy,
	// Token: 0x040020B1 RID: 8369
	DrawCitizenJobIcons,
	// Token: 0x040020B2 RID: 8370
	DrawCitizenJobIconsAll,
	// Token: 0x040020B3 RID: 8371
	CitizenJobFireman,
	// Token: 0x040020B4 RID: 8372
	CitizenJobBuilder,
	// Token: 0x040020B5 RID: 8373
	CitizenJobGathererBushes,
	// Token: 0x040020B6 RID: 8374
	CitizenJobGathererHerbs,
	// Token: 0x040020B7 RID: 8375
	CitizenJobGathererHoney,
	// Token: 0x040020B8 RID: 8376
	CitizenJobFarmer,
	// Token: 0x040020B9 RID: 8377
	CitizenJobHunter,
	// Token: 0x040020BA RID: 8378
	CitizenJobWoodcutter,
	// Token: 0x040020BB RID: 8379
	CitizenJobMiner,
	// Token: 0x040020BC RID: 8380
	CitizenJobMinerDeposit,
	// Token: 0x040020BD RID: 8381
	CitizenJobRoadBuilder,
	// Token: 0x040020BE RID: 8382
	CitizenJobCleaner,
	// Token: 0x040020BF RID: 8383
	CitizenJobManureCleaner,
	// Token: 0x040020C0 RID: 8384
	CitizenJobAttacker,
	// Token: 0x040020C1 RID: 8385
	UnitsInside,
	// Token: 0x040020C2 RID: 8386
	UnitKingdoms,
	// Token: 0x040020C3 RID: 8387
	SystemUnitPathfinding,
	// Token: 0x040020C4 RID: 8388
	SystemZoneGrowth,
	// Token: 0x040020C5 RID: 8389
	SystemBuildTick,
	// Token: 0x040020C6 RID: 8390
	SystemCityPlaceFinder,
	// Token: 0x040020C7 RID: 8391
	SystemWorldBehaviours,
	// Token: 0x040020C8 RID: 8392
	SystemProduceNewCitizens,
	// Token: 0x040020C9 RID: 8393
	SystemCheckUnitAction,
	// Token: 0x040020CA RID: 8394
	SystemUpdateUnits,
	// Token: 0x040020CB RID: 8395
	SystemUpdateBuildings,
	// Token: 0x040020CC RID: 8396
	SystemRedrawMap,
	// Token: 0x040020CD RID: 8397
	SystemUpdateCities,
	// Token: 0x040020CE RID: 8398
	SystemCityTasks,
	// Token: 0x040020CF RID: 8399
	AddJobManagerSkips,
	// Token: 0x040020D0 RID: 8400
	ProKing,
	// Token: 0x040020D1 RID: 8401
	ProLeader,
	// Token: 0x040020D2 RID: 8402
	ProUnit,
	// Token: 0x040020D3 RID: 8403
	ProWarrior,
	// Token: 0x040020D4 RID: 8404
	SystemUpdateDirtyChunks,
	// Token: 0x040020D5 RID: 8405
	DisplayUnitTiles,
	// Token: 0x040020D6 RID: 8406
	UseGlobalPathLock,
	// Token: 0x040020D7 RID: 8407
	ConstructionTiles,
	// Token: 0x040020D8 RID: 8408
	SystemMusic,
	// Token: 0x040020D9 RID: 8409
	RenderBigItems,
	// Token: 0x040020DA RID: 8410
	RenderFavoriteFoods,
	// Token: 0x040020DB RID: 8411
	RenderHoldingResources,
	// Token: 0x040020DC RID: 8412
	ShowMushInfection,
	// Token: 0x040020DD RID: 8413
	ShowPlagueInfection,
	// Token: 0x040020DE RID: 8414
	ShowZombieInfection,
	// Token: 0x040020DF RID: 8415
	ShowCursed,
	// Token: 0x040020E0 RID: 8416
	ShowPopulationTotal,
	// Token: 0x040020E1 RID: 8417
	ShowDragonTargetting,
	// Token: 0x040020E2 RID: 8418
	ShowGodFingerTargetting,
	// Token: 0x040020E3 RID: 8419
	ShowSwimToIslandLogic,
	// Token: 0x040020E4 RID: 8420
	RenderMapRegionEdges,
	// Token: 0x040020E5 RID: 8421
	SystemUpdateUnitAnimation,
	// Token: 0x040020E6 RID: 8422
	RenderIslands,
	// Token: 0x040020E7 RID: 8423
	RenderConnectedIslands,
	// Token: 0x040020E8 RID: 8424
	RenderIslandsTileCorners,
	// Token: 0x040020E9 RID: 8425
	RenderIslandCenterRegions,
	// Token: 0x040020EA RID: 8426
	RenderRegionOutsideRegionCorners,
	// Token: 0x040020EB RID: 8427
	RenderIslandsInsideRegionCorners,
	// Token: 0x040020EC RID: 8428
	ShowAmountNearArmy,
	// Token: 0x040020ED RID: 8429
	ShowWarriorsCityText,
	// Token: 0x040020EE RID: 8430
	ShowFoodCityText,
	// Token: 0x040020EF RID: 8431
	ShowCityWeaponsText,
	// Token: 0x040020F0 RID: 8432
	RenderCityDangerZones,
	// Token: 0x040020F1 RID: 8433
	RenderCityCenterZones,
	// Token: 0x040020F2 RID: 8434
	RenderCityFarmPlaces,
	// Token: 0x040020F3 RID: 8435
	PossibleCityReach,
	// Token: 0x040020F4 RID: 8436
	RenderVisibleZones,
	// Token: 0x040020F5 RID: 8437
	ArrowsUnitsAttackTargets,
	// Token: 0x040020F6 RID: 8438
	ArrowUnitsBehActorTarget,
	// Token: 0x040020F7 RID: 8439
	ArrowsUnitsPaths,
	// Token: 0x040020F8 RID: 8440
	ArrowsUnitsNavigationTargets,
	// Token: 0x040020F9 RID: 8441
	ArrowsUnitsHeight,
	// Token: 0x040020FA RID: 8442
	ArrowsUnitsFavoritesOnly,
	// Token: 0x040020FB RID: 8443
	ArrowsUnitsNextStepPosition,
	// Token: 0x040020FC RID: 8444
	ArrowsUnitsNextStepTile,
	// Token: 0x040020FD RID: 8445
	ArrowsUnitsCurrentPosition,
	// Token: 0x040020FE RID: 8446
	ArrowsOnlyForCursorCities,
	// Token: 0x040020FF RID: 8447
	CursorUnitAttackRange,
	// Token: 0x04002100 RID: 8448
	CursorUnitSize,
	// Token: 0x04002101 RID: 8449
	CursorUnitAttackChunks,
	// Token: 0x04002102 RID: 8450
	CursorCityZoneRange,
	// Token: 0x04002103 RID: 8451
	CursorEnemyFinderChunks,
	// Token: 0x04002104 RID: 8452
	KingdomDrawAttackTarget,
	// Token: 0x04002105 RID: 8453
	CivDrawSettleTarget,
	// Token: 0x04002106 RID: 8454
	CivDrawCityClaimZone,
	// Token: 0x04002107 RID: 8455
	KingdomHeads,
	// Token: 0x04002108 RID: 8456
	ActorGizmosBoatTaxiRequestTargets,
	// Token: 0x04002109 RID: 8457
	ActorGizmosBoatTaxiTarget,
	// Token: 0x0400210A RID: 8458
	SystemCheckGoodForBoat,
	// Token: 0x0400210B RID: 8459
	MakeUnitsFollowCursor,
	// Token: 0x0400210C RID: 8460
	SystemSplitAstar,
	// Token: 0x0400210D RID: 8461
	ParallelJobsUpdater,
	// Token: 0x0400210E RID: 8462
	ParallelChunks,
	// Token: 0x0400210F RID: 8463
	ChunkBatches,
	// Token: 0x04002110 RID: 8464
	UseCacheForRegionPath,
	// Token: 0x04002111 RID: 8465
	FastCultures,
	// Token: 0x04002112 RID: 8466
	CityFastZonesGrowth,
	// Token: 0x04002113 RID: 8467
	FmodZones,
	// Token: 0x04002114 RID: 8468
	CityInfiniteResources,
	// Token: 0x04002115 RID: 8469
	CityFastConstruction,
	// Token: 0x04002116 RID: 8470
	CityFastUpgrades,
	// Token: 0x04002117 RID: 8471
	CityFastPopGrowth,
	// Token: 0x04002118 RID: 8472
	CityUnlimitedHouses,
	// Token: 0x04002119 RID: 8473
	CityUnlimitedZoneRange,
	// Token: 0x0400211A RID: 8474
	UnitsAlwaysFast,
	// Token: 0x0400211B RID: 8475
	OverlaySoundsAttached,
	// Token: 0x0400211C RID: 8476
	OverlaySounds,
	// Token: 0x0400211D RID: 8477
	OverlaySoundsActive,
	// Token: 0x0400211E RID: 8478
	OverlayActorCivs,
	// Token: 0x0400211F RID: 8479
	OverlayBoatTransport,
	// Token: 0x04002120 RID: 8480
	OverlayActorMobs,
	// Token: 0x04002121 RID: 8481
	OverlayCursorActor,
	// Token: 0x04002122 RID: 8482
	OverlayActorFavoritesOnly,
	// Token: 0x04002123 RID: 8483
	OverlayActorGroupLeaderOnly,
	// Token: 0x04002124 RID: 8484
	OverlayArmies,
	// Token: 0x04002125 RID: 8485
	OverlayCivBuildings,
	// Token: 0x04002126 RID: 8486
	OverlayTrees,
	// Token: 0x04002127 RID: 8487
	OverlayPlants,
	// Token: 0x04002128 RID: 8488
	OverlayOtherBuildings,
	// Token: 0x04002129 RID: 8489
	OverlayCity,
	// Token: 0x0400212A RID: 8490
	OverlayCityTasks,
	// Token: 0x0400212B RID: 8491
	OverlayKingdom,
	// Token: 0x0400212C RID: 8492
	BoatPassengerLines,
	// Token: 0x0400212D RID: 8493
	BuildingResidents,
	// Token: 0x0400212E RID: 8494
	Lovers,
	// Token: 0x0400212F RID: 8495
	ShowMoneyIcons,
	// Token: 0x04002130 RID: 8496
	ShowKingdomIcons,
	// Token: 0x04002131 RID: 8497
	DeadUnits,
	// Token: 0x04002132 RID: 8498
	DrawBadLinksDiag,
	// Token: 0x04002133 RID: 8499
	UseCameraAspect,
	// Token: 0x04002134 RID: 8500
	ScaleEffectEnabled,
	// Token: 0x04002135 RID: 8501
	LavaGlow,
	// Token: 0x04002136 RID: 8502
	TesterLibs,
	// Token: 0x04002137 RID: 8503
	ShowLayoutGroupGrid,
	// Token: 0x04002138 RID: 8504
	ShowHiddenStats,
	// Token: 0x04002139 RID: 8505
	OverrideUnitShooting,
	// Token: 0x0400213A RID: 8506
	OverrideUnitWalking,
	// Token: 0x0400213B RID: 8507
	IgnoreDamage,
	// Token: 0x0400213C RID: 8508
	DebugTooltipUI,
	// Token: 0x0400213D RID: 8509
	DebugTooltipActorAI,
	// Token: 0x0400213E RID: 8510
	DebugCityReproduction,
	// Token: 0x0400213F RID: 8511
	DebugPowerBarTooltipTaxonomy,
	// Token: 0x04002140 RID: 8512
	DebugPowerBarTooltipStartingCivMetas,
	// Token: 0x04002141 RID: 8513
	DebugPowerBarTooltipSpeciesTraits,
	// Token: 0x04002142 RID: 8514
	DebugTownPlans,
	// Token: 0x04002143 RID: 8515
	InspectObjectsOnClick,
	// Token: 0x04002144 RID: 8516
	BenchAiEnabled,
	// Token: 0x04002145 RID: 8517
	ArtForceSkinLeader,
	// Token: 0x04002146 RID: 8518
	ArtForceSkinKing,
	// Token: 0x04002147 RID: 8519
	ArtForceSkinWarrior,
	// Token: 0x04002148 RID: 8520
	ArtForceSkinBaby,
	// Token: 0x04002149 RID: 8521
	DebugFocusUnitTasks,
	// Token: 0x0400214A RID: 8522
	DebugUnitHotkeys,
	// Token: 0x0400214B RID: 8523
	DebugWindowHotkeys,
	// Token: 0x0400214C RID: 8524
	DebugMonolith,
	// Token: 0x0400214D RID: 8525
	DebugButton,
	// Token: 0x0400214E RID: 8526
	ControlledUnitsAttackRaycast,
	// Token: 0x0400214F RID: 8527
	GenerateNewMapOnMapLoadingError,
	// Token: 0x04002150 RID: 8528
	DebugIdleSounds
}
