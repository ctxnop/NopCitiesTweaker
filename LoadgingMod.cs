#region References
using ColossalFramework;
using ColossalFramework.Plugins;
using ICities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
#endregion

namespace NopCitiesTweaker
{
	public class LoadingMod
		: LoadingExtensionBase
	{
		#region Variables
		#endregion

		#region Properties
		#endregion

		#region Constructors
		#endregion

		#region Methods
		public override void OnLevelLoaded(LoadMode mode)
		{
			base.OnLevelLoaded(mode);

			if (ConfigLoader.Config.EnableAchievements)
			{
				DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, "Enabling achievements!");
				Singleton<SimulationManager>.instance.m_metaData.m_disableAchievements = SimulationMetaData.MetaBool.False;
			}

			if (ConfigLoader.Config.DumpPrefabsData)
			{
				DumpPrefabsData();
			}

			DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, $"Tweaks rules loaded: {ConfigLoader.Config.Tweaks?.Count}");
			ApplyTweaks();
		}

		private void DumpPrefabsData()
		{
			SortedDictionary<string, string> prefabs = new SortedDictionary<string, string>();
			int count = PrefabCollection<BuildingInfo>.PrefabCount();
			for (uint i = 0; i < count; ++i)
			{
				BuildingInfo bi = PrefabCollection<BuildingInfo>.GetPrefab(i);
				if (bi.m_buildingAI != null)
				{
					Type type = bi.m_buildingAI.GetType();
					string category = type.FullName;
					string name = PrefabCollection<BuildingInfo>.PrefabName(i);

					foreach(FieldInfo fi in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
					{
						if (
							fi.FieldType.FullName == typeof(bool).FullName ||
							fi.FieldType.FullName == typeof(int).FullName ||
							fi.FieldType.FullName == typeof(float).FullName)
						{
							prefabs.Add($"{category};{name};{fi.Name}", $"{fi.GetValue(bi.m_buildingAI)}");
						}
					}
				}
			}

			using(StreamWriter sw = new StreamWriter(ConfigLoader.ConfigPath + "/prefabs.csv", false, Encoding.UTF8))
			{
				foreach(KeyValuePair<string, string> kvp in prefabs)
				{
					sw.WriteLine($"{kvp.Key};{kvp.Value}");
				}
			}
		}

		private void ApplyTweaks()
		{
			if (ConfigLoader.Config.Tweaks.Count == 0) return;

			int count = PrefabCollection<BuildingInfo>.PrefabCount();
			for (uint i = 0; i < count; ++i)
			{
				BuildingInfo bi = PrefabCollection<BuildingInfo>.GetPrefab(i);
				if (bi.m_buildingAI != null)
				{
					foreach(PrefabTweak pt in ConfigLoader.Config.Tweaks)
					{
						pt.Apply(i, bi.m_buildingAI);
					}
				}
			}
		}
		#endregion

	}
}
