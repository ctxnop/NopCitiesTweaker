#region References
using ColossalFramework.Plugins;
using System;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
#endregion

namespace NopCitiesTweaker
{
	[Serializable]
	public class PrefabTweak
	{
		public MatchPatternKind Matcher { get; set; } = MatchPatternKind.Category;
		public ValueOperation Operator { get; set; } = ValueOperation.Mul;
		public string Pattern { get; set; }
		public string Field { get; set; }
		public string Value { get; set; }

		private bool IsMatch(uint index, BuildingAI bld)
		{
			switch(Matcher)
			{
				case MatchPatternKind.Category:
					Type t = bld.GetType();
					while(true)
					{
						if (string.CompareOrdinal(Pattern, t.FullName) == 0) return true;
						if (string.CompareOrdinal(t.FullName, typeof(BuildingAI).FullName) == 0) return false;
						t = t.BaseType;
					}

				case MatchPatternKind.Name:
					return string.CompareOrdinal(Pattern, PrefabCollection<BuildingInfo>.PrefabName(index)) == 0;
				
				case MatchPatternKind.Regex:
					return Regex.IsMatch(PrefabCollection<BuildingInfo>.PrefabName(index), Pattern);
			}

			return false;
		}

		public void Apply(uint index, BuildingAI bld)
		{
			if (IsMatch(index, bld))
			{
				string name = PrefabCollection<BuildingInfo>.PrefabName(index);
				FieldInfo fld = bld.GetType().GetField(Field, BindingFlags.Public | BindingFlags.Instance);
				if (fld == null)
				{
					DebugOutputPanel.AddMessage(PluginManager.MessageType.Error, $"{name} doesn't have a '{Field}' field!");
					return;
				}
				object oldValue = null;
				object newValue = null;

				switch(Operator)
				{
					case ValueOperation.Add:
						oldValue = fld.GetValue(bld);
						if (typeof(int).FullName == fld.FieldType.FullName)
							newValue = (int)oldValue + int.Parse(Value);
						else if (typeof(float).FullName == fld.FieldType.FullName)
							newValue = (float)oldValue + float.Parse(Value);
						else throw new Exception($"Operation 'Add' not supported on field type: {fld.FieldType.FullName}");
						break;
					case ValueOperation.Mul:
						oldValue = fld.GetValue(bld);
						if (typeof(int).FullName == fld.FieldType.FullName)
							newValue = (int)((int)oldValue * float.Parse(Value));
						else if (typeof(float).FullName == fld.FieldType.FullName)
							newValue = (int)((float)oldValue * float.Parse(Value));
						else throw new Exception($"Operation 'Mul' not supported on field type: {fld.FieldType.FullName}");
						break;
					case ValueOperation.Set:
						if (typeof(int).FullName == fld.FieldType.FullName)
							newValue = int.Parse(Value);
						else if (typeof(float).FullName == fld.FieldType.FullName)
							newValue = float.Parse(Value);
						else if (typeof(bool).FullName == fld.FieldType.FullName)
							newValue = bool.Parse(Value);
						else throw new Exception($"Operation 'Set' not supported on field type: {fld.FieldType.FullName}");
						break;
					default:
						throw new Exception("Unkown operation!");
				}
				fld.SetValue(bld, newValue);
			}
		}

		public override string ToString()
		{
			return $"{{\"Matcher\": \"{Matcher}\", \"Operator\": \"{Operator}\", \"Pattern\": \"{Pattern}\", \"Field\": \"{Field}\", \"Value\": \"{Value}\" }}";
		}

		public static PrefabTweak FromHashtable(Hashtable ht)
		{
			PrefabTweak pt = new PrefabTweak();
			if (ht.Contains(nameof(Matcher))) pt.Matcher = (MatchPatternKind)Enum.Parse(typeof(MatchPatternKind), ht[nameof(Matcher)] as string, true);
			if (ht.Contains(nameof(Operator))) pt.Operator = (ValueOperation)Enum.Parse(typeof(ValueOperation), ht[nameof(Operator)] as string, true);
			if (ht.Contains(nameof(Pattern))) pt.Pattern = ht[nameof(Pattern)] as string;
			if (ht.Contains(nameof(Field))) pt.Field = ht[nameof(Field)] as string;
			if (ht.Contains(nameof(Value))) pt.Value = ht[nameof(Value)] as string;
			return pt;
		}
	}
}
