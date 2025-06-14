
using System.Collections.Generic;
using UnityEngine;

public class EnumTranslatorInitializer
{
    public static void RegisterAll()
    
        {
        EnumTranslator.Register(new Dictionary<ItemCategory, string>
        {
            { ItemCategory.Weapon, "Broñ" },
            { ItemCategory.Armor, "Pancerz" },
            { ItemCategory.Accessory, "Akcesorium" },
            { ItemCategory.Firearm, "Broñ palna" },
            { ItemCategory.Melee, "Broñ bia³a" },
            { ItemCategory.Civilian, "Cywilny" },
            { ItemCategory.Military, "Militarny" },
            { ItemCategory.Experimental, "Eksperymentalny" },
            { ItemCategory.Pistol, "Pistolet" },
            { ItemCategory.SMG, "SMG" },
            { ItemCategory.Rifle, "Karabin" },
            { ItemCategory.AssaultRifle, "Karabin szturmowy" },
            { ItemCategory.HeavyWeapon, "Broñ ciê¿ka" },
            { ItemCategory.Shotgun, "Strzelba" },
            { ItemCategory.Tool, "Narzêdzie" },
            { ItemCategory.Other, "Inne" }
        });

            EnumTranslator.Register(new Dictionary<NeoScienceCategory, string>
        {
            { NeoScienceCategory.Neophysics, "Neofizyka" },
            { NeoScienceCategory.Neolinguistics, "Neolingwistyka" },
            { NeoScienceCategory.Neosensorics, "Neosensoryka" },
            { NeoScienceCategory.Psionics, "Psionika" }
        });

            EnumTranslator.Register(new Dictionary<NeoPhysicsCategory, string>
        {
            { NeoPhysicsCategory.Graviturgics, "Grawiturgia" },
            { NeoPhysicsCategory.Chronoturgics, "Chronoturgia" },
            { NeoPhysicsCategory.Energodynamics, "Energodynamika" },
            { NeoPhysicsCategory.NeoOptics, "Neooptyka" },
            { NeoPhysicsCategory.NeoAcoustics, "Neoakustyka" },
            { NeoPhysicsCategory.Other, "Inne" }
        });

            EnumTranslator.Register(new Dictionary<NeoSensoricsCategory, string>
        {
            { NeoSensoricsCategory.Shared, "Wspólna" },
            { NeoSensoricsCategory.Futuresight, "Futurispecja" },
            { NeoSensoricsCategory.Neospiritism, "Neospirytyzm" },
            { NeoSensoricsCategory.Biomancy, "Biomancja" }
        });

            EnumTranslator.Register(new Dictionary<PsionicsCategory, string>
        {
            { PsionicsCategory.Shared, "Wspólna" },
            { PsionicsCategory.Telekinesis, "Telekineza" },
            { PsionicsCategory.Pyrokinesis, "Pyrokineza" },
            { PsionicsCategory.Psychokinesis, "Psychokineza" }
        });

            EnumTranslator.Register(new Dictionary<TypesCategory, string>
        {
            { TypesCategory.Talent, "Talent" },
            { TypesCategory.Discipline, "Dyscyplina" }
        });

        Debug.Log(EnumTranslator.GetTotalTranslationCount());
    }
}
