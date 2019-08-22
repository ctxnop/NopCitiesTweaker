# NopCitiesTweaker

This mod allow you to change many settings using a configuration file.

## Configuration

The configuration file path is: ```%LOCALAPPDATA%\Colossal Order\Cities_Skylines\Addons\Mods\NopCitiesTweaker\tweaks.json```

Using this file you can set global multipliers for construction, maintenance and relocation costs. You can also enable achievements.

Finally you can control any properties of any building using tweaking rules.

Hence you can tweak the game to your needs, making it easier or harder as you wish.

Here is an example of configuration file that enable many cheats:
```json
{
    "EnableAchievements": true,
	"ConstructionCostMultiplier": 0.1,
	"MaintenanceCostMultiplier": 0.1,
	"RelocationCostMultiplier": 0.1,
	"Tweaks": [
		{ "Pattern": "PlayerBuildingAI", "Field": "m_electricityConsumption", "Value": "0" },
		{ "Pattern": "PlayerBuildingAI", "Field": "m_waterConsumption", "Value": "0" },
		{ "Pattern": "PlayerBuildingAI", "Field": "m_sewageAccumulation", "Value": "0" },
		{ "Pattern": "PlayerBuildingAI", "Field": "m_garbageAccumulation", "Value": "0" },
		{ "Pattern": "PlayerBuildingAI", "Field": "m_fireHazard", "Value": "0" },
		{ "Pattern": "PlayerBuildingAI", "Field": "m_fireTolerance", "Value": "100" },
		{ "Pattern": "PowerPlantAI", "Field": "m_electricityProduction", "Value": "10" },
		{ "Pattern": "PowerPlantAI", "Field": "m_pollutionAccumulation", "Value": "0" },
		{ "Pattern": "PowerPlantAI", "Field": "m_pollutionRadius", "Value": "0" },
		{ "Pattern": "PowerPlantAI", "Field": "m_noiseRadius", "Value": "0" },
		{ "Pattern": "HospitalAI", "Field": "m_ambulanceCount", "Value": "2" },
		{ "Pattern": "HospitalAI", "Field": "m_patientCapacity", "Value": "10" },
		{ "Pattern": "HospitalAI", "Field": "m_curingRate", "Value": "10" },
		{ "Pattern": "HospitalAI", "Field": "m_healthCareAccumulation", "Value": "10" },
		{ "Pattern": "HospitalAI", "Field": "m_healthCareRadius", "Value": "10" },
		{ "Pattern": "PoliceStationAI", "Field": "m_policeCarCount", "Value": "2" },
		{ "Pattern": "PoliceStationAI", "Field": "m_jailCapacity", "Value": "10" },
		{ "Pattern": "PoliceStationAI", "Field": "m_policeDepartmentAccumulation", "Value": "10" },
		{ "Pattern": "PoliceStationAI", "Field": "m_policeDepartmentRadius", "Value": "10" },
		{ "Pattern": "PoliceStationAI", "Field": "m_noiseRadius", "Value": "0" },
		{ "Pattern": "PoliceStationAI", "Field": "m_noiseAccumulation", "Value": "0" },
		{ "Pattern": "SchoolAI", "Field": "m_studentCount", "Value": "100" },
		{ "Pattern": "SchoolAI", "Field": "m_educationAccumulation", "Value": "100" },
		{ "Pattern": "SchoolAI", "Field": "m_educationRadius", "Value": "10" },
		{ "Pattern": "FireStationAI", "Field": "m_fireTruckCount", "Value": "2" },
		{ "Pattern": "FireStationAI", "Field": "m_fireDepartmentAccumulation", "Value": "10" },
		{ "Pattern": "FireStationAI", "Field": "m_fireDepartmentRadius", "Value": "10" },
		{ "Pattern": "CemeteryAI", "Field": "m_hearseCount", "Value": "2" },
		{ "Pattern": "CemeteryAI", "Field": "m_corpseCapacity", "Value": "10" },
		{ "Pattern": "CemeteryAI", "Field": "m_burialRate", "Value": "10" },
		{ "Pattern": "CemeteryAI", "Field": "m_deathCareAccumulation", "Value": "10" },
		{ "Pattern": "CemeteryAI", "Field": "m_deathCareRadius", "Value": "10" },
		{ "Pattern": "CemeteryAI", "Field": "m_graveCount", "Value": "10" },
		{ "Pattern": "LandfillSiteAI", "Field": "m_garbageTruckCount", "Value": "3" },
		{ "Pattern": "LandfillSiteAI", "Field": "m_garbageCapacity", "Value": "10" },
		{ "Pattern": "LandfillSiteAI", "Field": "m_garbageConsumption", "Value": "10" },
		{ "Pattern": "LandfillSiteAI", "Field": "m_electricityProduction", "Value": "10" },
		{ "Pattern": "LandfillSiteAI", "Field": "m_pollutionAccumulation", "Value": "0" },
		{ "Pattern": "LandfillSiteAI", "Field": "m_pollutionRadius", "Value": "0" },
		{ "Pattern": "LandfillSiteAI", "Field": "m_noiseAccumulation", "Value": "0" },
		{ "Pattern": "LandfillSiteAI", "Field": "m_noiseRadius", "Value": "0" },
		{ "Pattern": "LandfillSiteAI", "Field": "m_collectRadius", "Value": "10" },
		{ "Pattern": "LandfillSiteAI", "Field": "m_materialProduction", "Value": "10" }
	]
}
```

### EnableAchievements (Default: false)

Allows achivements even with mods enabled.

### DumpPrefabsData (Default: false)

Dump prefabs data into a prefabs.csv file, placed in the same directory as the configuration file.

This let you check building categories, names and properties.

### ConstructionCostMultiplier (Default: 1.0)

Multiply the construction cost by this multiplier. Greater than 1.0 to increase the cost, lower than 1.0 to decrease.

### MaintenanceCostMultiplier (Default: 1.0)

Multiply the maintenance cost by this multiplier. Greater than 1.0 to increase the cost, lower than 1.0 to decrease.

### RelocationCostMultiplier (Default: 1.0)

Multiply the relocation cost by this multiplier. Greater than 1.0 to increase the cost, lower than 1.0 to decrease.

### Tweaks

Tweaking rule can set or modify any numeric or boolean properties or any building.

```json
{
	"Matcher": "Category",
	"Operator": "Mul",
	"Pattern": "",
	"Field": "",
	"Value": ""
}
```
Values shown above are the default one, thus the property can be omitted.

### Matcher (Default: Category)

```Matcher``` can be Category, Name or Regex.

### Operator (Default: Mul)

```Operator``` can be Mul, Add or Set.

### Pattern

If the matcher is ```Category```, then ```Pattern``` should be any building category name or parent category name.

Otherwise, the match is made using the building's name or a regex on the building's name.

### Field

The field to tweak. Refers to the prefabs.csv file to get fields names.
