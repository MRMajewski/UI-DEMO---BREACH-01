using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnumTranslator
{
    private static readonly Dictionary<Type, Dictionary<Enum, string>> allTranslations = new();

    public static IReadOnlyDictionary<System.Type, Dictionary<Enum, string>> GetAllTranslations()
    {
        return allTranslations;
    }
    public static int GetTotalTranslationCount()
    {
        return allTranslations.Values.Sum(innerDict => innerDict.Count);
    }

    public static void Register<T>(Dictionary<T, string> newEntries) where T : Enum
    {
        var type = typeof(T);

        if (!allTranslations.ContainsKey(type))
        {
            allTranslations[type] = new Dictionary<Enum, string>();
        }

        var dict = allTranslations[type];

        foreach (var pair in newEntries)
        {
            if (dict.TryGetValue(pair.Key, out var existingValue))
            {
                if (existingValue != pair.Value)
                {
                    Debug.Log($"EnumTranslator: Nadpisano t³umaczenie dla {type.Name}.{pair.Key} z '{existingValue}' na '{pair.Value}'");
                    dict[pair.Key] = pair.Value;
                }
            }
            else
            {
                dict[pair.Key] = pair.Value;
            }
        }
    }

    public static string GetName(Enum value)
    {
        var type = value.GetType();
        if (allTranslations.TryGetValue(type, out var dict) && dict.TryGetValue(value, out var result))
        {
            return result;
        }

        return value.ToString(); // fallback
    }
}
public enum NeoScienceCategory
{
    Neophysics,
    Neolinguistics,
    Neosensorics,
    Psionics
}

public enum NeoPhysicsCategory
{
    Graviturgics,
    Chronoturgics,
    Energodynamics,
    NeoOptics,
    NeoAcoustics,
    Other
}
public enum NeoSensoricsCategory
{
    Shared,
    Futuresight,
    Neospiritism,
    Biomancy
}

public enum PsionicsCategory
{
    Shared,
    Telekinesis,
    Pyrokinesis,
    Psychokinesis
}

public enum TypesCategory
{
    Talent,
    Discipline
}
public enum ItemCategory
{
    Weapon,
    Armor,
    Accessory,
    Firearm,
    Melee,
    Civilian,
    Military,
    Experimental,
    Pistol,
    SMG,
    Rifle,
    AssaultRifle,
    HeavyWeapon,
    Shotgun,
    Tool,
    Other
}
