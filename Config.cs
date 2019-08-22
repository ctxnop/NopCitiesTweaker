#region References
using ColossalFramework.HTTP;
using ColossalFramework.Plugins;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
#endregion

namespace NopCitiesTweaker
{
	[Serializable]
	public class Config
	{
		public bool EnableAchievements { get; set; } = false;
		public bool DumpPrefabsData { get; set; } = false;
		public float ConstructionCostMultiplier { get; set; } = 1.0f;
		public float MaintenanceCostMultiplier { get; set; } = 1.0f;
		public float RelocationCostMultiplier { get; set; } = 1.0f;
		public float RefundMultiplier { get; set; } = 1.0f;

		public List<PrefabTweak> Tweaks { get; } = new List<PrefabTweak>();

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("{\n");
			sb.Append($"\t\"EnableAchivements\": \"{EnableAchievements}\",\n");
			sb.Append($"\t\"DumpPrefabsData\": \"{DumpPrefabsData}\",\n");
			sb.Append($"\t\"ConstructionCostMultiplier\": \"{ConstructionCostMultiplier}\",\n");
			sb.Append($"\t\"MaintenanceCostMultiplier\": \"{MaintenanceCostMultiplier}\",\n");
			sb.Append($"\t\"RelocationCostMultiplier\": \"{RelocationCostMultiplier}\",\n");
			sb.Append($"\t\"RefundMultiplier\": \"{RefundMultiplier}\"");

			if (Tweaks.Count > 0)
			{
				sb.Append(",\n\t\"Tweaks\": [\n");
				for(int i = 0; i < Tweaks.Count; ++i)
				{
					sb.Append($"\t\t{Tweaks[i]}");
					if (i + 1 < Tweaks.Count) sb.Append(",\n");
				}
			}

			sb.Append(",\n}");
			return sb.ToString();
		}

		public static Config FromHashtable(Hashtable ht)
		{
			Config cfg = new Config();
			
			if (ht.Contains(nameof(EnableAchievements))) cfg.EnableAchievements = (bool)ht[nameof(EnableAchievements)];
			if (ht.Contains(nameof(DumpPrefabsData))) cfg.DumpPrefabsData = (bool)ht[nameof(EnableAchievements)];
			if (ht.Contains(nameof(ConstructionCostMultiplier))) cfg.ConstructionCostMultiplier = (float)ht[nameof(ConstructionCostMultiplier)];
			if (ht.Contains(nameof(MaintenanceCostMultiplier))) cfg.MaintenanceCostMultiplier = (float)ht[nameof(MaintenanceCostMultiplier)];
			if (ht.Contains(nameof(RelocationCostMultiplier))) cfg.RelocationCostMultiplier = (float)ht[nameof(RelocationCostMultiplier)];
			if (ht.Contains(nameof(RefundMultiplier))) cfg.RefundMultiplier = (float)ht[nameof(RefundMultiplier)];

			if (ht.Contains(nameof(Tweaks)))
			{
				ArrayList tweaks = ht[nameof(Tweaks)] as ArrayList;
				foreach(object t in tweaks)
				{
					PrefabTweak pt = PrefabTweak.FromHashtable(t as Hashtable);
					cfg.Tweaks.Add(pt);
				}
			}

			return cfg;
		}
	}

	public static class ConfigLoader
	{
		public static readonly string ConfigPath;
		public static readonly string ConfigFile;
		private static Config _Config;
		static ConfigLoader()
		{
			ConfigPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Colossal Order/Cities_Skylines/Addons/Mods/NopCitiesTweaker";
			ConfigFile = ConfigPath + "/tweaks.json";
		}

		public static Config Config
		{
			get
			{
				if (_Config != null) return _Config;

				if (!Directory.Exists(ConfigPath))
					Directory.CreateDirectory(ConfigPath);

				if (File.Exists(ConfigFile))
				{
					string jsondata = File.ReadAllText(ConfigFile, Encoding.UTF8);
					//_Config = JsonUtility.FromJson<Config>(jsondata);
					_Config = Config.FromHashtable(JSON.JsonDecode(jsondata) as Hashtable);
				}
				else
				{
					_Config = new Config();
					File.WriteAllText(ConfigFile, JsonUtility.ToJson(_Config, true), Encoding.UTF8);
				}
				return _Config;
			}
		}
	}
}
