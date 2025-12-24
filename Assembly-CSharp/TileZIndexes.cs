using System;

// Token: 0x02000084 RID: 132
public enum TileZIndexes
{
	// Token: 0x04000432 RID: 1074
	nothing,
	// Token: 0x04000433 RID: 1075
	pit_deep_ocean,
	// Token: 0x04000434 RID: 1076
	pit_close_ocean,
	// Token: 0x04000435 RID: 1077
	pit_shallow_waters,
	// Token: 0x04000436 RID: 1078
	deep_ocean,
	// Token: 0x04000437 RID: 1079
	close_ocean,
	// Token: 0x04000438 RID: 1080
	shallow_waters,
	// Token: 0x04000439 RID: 1081
	border_pit,
	// Token: 0x0400043A RID: 1082
	border_water,
	// Token: 0x0400043B RID: 1083
	border_water_runup,
	// Token: 0x0400043C RID: 1084
	wall_evil,
	// Token: 0x0400043D RID: 1085
	wall_order,
	// Token: 0x0400043E RID: 1086
	wall_ancient,
	// Token: 0x0400043F RID: 1087
	wall_wild,
	// Token: 0x04000440 RID: 1088
	wall_green,
	// Token: 0x04000441 RID: 1089
	wall_iron,
	// Token: 0x04000442 RID: 1090
	wall_light,
	// Token: 0x04000443 RID: 1091
	ice,
	// Token: 0x04000444 RID: 1092
	sand,
	// Token: 0x04000445 RID: 1093
	snow_sand,
	// Token: 0x04000446 RID: 1094
	soil_low,
	// Token: 0x04000447 RID: 1095
	soil_high,
	// Token: 0x04000448 RID: 1096
	wasteland_low,
	// Token: 0x04000449 RID: 1097
	wasteland_high,
	// Token: 0x0400044A RID: 1098
	lava0,
	// Token: 0x0400044B RID: 1099
	lava1,
	// Token: 0x0400044C RID: 1100
	lava2,
	// Token: 0x0400044D RID: 1101
	lava3,
	// Token: 0x0400044E RID: 1102
	swamp_low,
	// Token: 0x0400044F RID: 1103
	desert_low,
	// Token: 0x04000450 RID: 1104
	road,
	// Token: 0x04000451 RID: 1105
	swamp_high,
	// Token: 0x04000452 RID: 1106
	field,
	// Token: 0x04000453 RID: 1107
	fuse,
	// Token: 0x04000454 RID: 1108
	biomass_low,
	// Token: 0x04000455 RID: 1109
	biomass_high,
	// Token: 0x04000456 RID: 1110
	pumpkin_low,
	// Token: 0x04000457 RID: 1111
	pumpkin_high,
	// Token: 0x04000458 RID: 1112
	cybertile_low,
	// Token: 0x04000459 RID: 1113
	cybertile_high,
	// Token: 0x0400045A RID: 1114
	grass_low,
	// Token: 0x0400045B RID: 1115
	grass_high,
	// Token: 0x0400045C RID: 1116
	corruption_low,
	// Token: 0x0400045D RID: 1117
	corruption_high,
	// Token: 0x0400045E RID: 1118
	enchanted_low,
	// Token: 0x0400045F RID: 1119
	enchanted_high,
	// Token: 0x04000460 RID: 1120
	mushroom_low,
	// Token: 0x04000461 RID: 1121
	mushroom_high,
	// Token: 0x04000462 RID: 1122
	birch_low,
	// Token: 0x04000463 RID: 1123
	birch_high,
	// Token: 0x04000464 RID: 1124
	maple_low,
	// Token: 0x04000465 RID: 1125
	maple_high,
	// Token: 0x04000466 RID: 1126
	rocklands_low,
	// Token: 0x04000467 RID: 1127
	rocklands_high,
	// Token: 0x04000468 RID: 1128
	garlic_low,
	// Token: 0x04000469 RID: 1129
	garlic_high,
	// Token: 0x0400046A RID: 1130
	flower_low,
	// Token: 0x0400046B RID: 1131
	flower_high,
	// Token: 0x0400046C RID: 1132
	celestial_low,
	// Token: 0x0400046D RID: 1133
	celestial_high,
	// Token: 0x0400046E RID: 1134
	singularity_low,
	// Token: 0x0400046F RID: 1135
	singularity_high,
	// Token: 0x04000470 RID: 1136
	clover_low,
	// Token: 0x04000471 RID: 1137
	clover_high,
	// Token: 0x04000472 RID: 1138
	paradox_low,
	// Token: 0x04000473 RID: 1139
	paradox_high,
	// Token: 0x04000474 RID: 1140
	solitude_low,
	// Token: 0x04000475 RID: 1141
	solitude_high,
	// Token: 0x04000476 RID: 1142
	savanna_low,
	// Token: 0x04000477 RID: 1143
	savanna_high,
	// Token: 0x04000478 RID: 1144
	jungle_low,
	// Token: 0x04000479 RID: 1145
	jungle_high,
	// Token: 0x0400047A RID: 1146
	infernal_low,
	// Token: 0x0400047B RID: 1147
	infernal_high,
	// Token: 0x0400047C RID: 1148
	desert_high,
	// Token: 0x0400047D RID: 1149
	crystal_low,
	// Token: 0x0400047E RID: 1150
	crystal_high,
	// Token: 0x0400047F RID: 1151
	candy_low,
	// Token: 0x04000480 RID: 1152
	candy_high,
	// Token: 0x04000481 RID: 1153
	lemon_low,
	// Token: 0x04000482 RID: 1154
	lemon_high,
	// Token: 0x04000483 RID: 1155
	grass_flowers,
	// Token: 0x04000484 RID: 1156
	grass_forest_flowers,
	// Token: 0x04000485 RID: 1157
	permafrost_low,
	// Token: 0x04000486 RID: 1158
	permafrost_high,
	// Token: 0x04000487 RID: 1159
	tumor_low,
	// Token: 0x04000488 RID: 1160
	tumor_high,
	// Token: 0x04000489 RID: 1161
	landmine,
	// Token: 0x0400048A RID: 1162
	water_bomb,
	// Token: 0x0400048B RID: 1163
	tnt_timed,
	// Token: 0x0400048C RID: 1164
	tnt,
	// Token: 0x0400048D RID: 1165
	fireworks,
	// Token: 0x0400048E RID: 1166
	hills,
	// Token: 0x0400048F RID: 1167
	snow_hills,
	// Token: 0x04000490 RID: 1168
	mountains,
	// Token: 0x04000491 RID: 1169
	snow_block,
	// Token: 0x04000492 RID: 1170
	summit,
	// Token: 0x04000493 RID: 1171
	snow_summit,
	// Token: 0x04000494 RID: 1172
	grey_goo
}
