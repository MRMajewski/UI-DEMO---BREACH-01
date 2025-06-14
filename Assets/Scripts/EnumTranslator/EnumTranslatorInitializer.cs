
using System.Collections.Generic;
using UnityEngine;

public class EnumTranslatorInitializer
{
    public static void RegisterAll()
    
        {
        EnumTranslator.Register(new Dictionary<ItemCategory, string>
        {
            { ItemCategory.Weapon, "Bro�" },
            { ItemCategory.Armor, "Pancerz" },
            { ItemCategory.Accessory, "Akcesorium" },
            { ItemCategory.Firearm, "Bro� palna" },
            { ItemCategory.Melee, "Bro� bia�a" },
            { ItemCategory.Civilian, "Cywilny" },
            { ItemCategory.Military, "Militarny" },
            { ItemCategory.Experimental, "Eksperymentalny" },
            { ItemCategory.Pistol, "Pistolet" },
            { ItemCategory.SMG, "SMG" },
            { ItemCategory.Rifle, "Karabin" },
            { ItemCategory.AssaultRifle, "Karabin szturmowy" },
            { ItemCategory.HeavyWeapon, "Bro� ci�ka" },
            { ItemCategory.Shotgun, "Strzelba" },
            { ItemCategory.Tool, "Narz�dzie" },
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
            { NeoSensoricsCategory.Shared, "Wsp�lna" },
            { NeoSensoricsCategory.Futuresight, "Futurispecja" },
            { NeoSensoricsCategory.Neospiritism, "Neospirytyzm" },
            { NeoSensoricsCategory.Biomancy, "Biomancja" }
        });

            EnumTranslator.Register(new Dictionary<PsionicsCategory, string>
        {
            { PsionicsCategory.Shared, "Wsp�lna" },
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
