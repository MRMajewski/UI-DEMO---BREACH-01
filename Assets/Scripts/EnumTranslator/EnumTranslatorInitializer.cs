using System.Collections.Generic;
using UnityEngine;

public class EnumTranslatorInitializer
{
    public static void RegisterAll()
    
        {
        EnumManager.Register(new Dictionary<ItemCategory, string>
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

            EnumManager.Register(new Dictionary<NeoScienceCategory, string>
        {
            { NeoScienceCategory.Neophysics, "Neofizyka" },
            { NeoScienceCategory.Neolinguistics, "Neolingwistyka" },
            { NeoScienceCategory.Neosensorics, "Neosensoryka" },
            { NeoScienceCategory.Psionics, "Psionika" }
        });

            EnumManager.Register(new Dictionary<NeoPhysicsCategory, string>
        {
            { NeoPhysicsCategory.Graviturgics, "Grawiturgia" },
            { NeoPhysicsCategory.Chronoturgics, "Chronoturgia" },
            { NeoPhysicsCategory.Energodynamics, "Energodynamika" },
            { NeoPhysicsCategory.NeoOptics, "Neooptyka" },
            { NeoPhysicsCategory.NeoAcoustics, "Neoakustyka" },
            { NeoPhysicsCategory.Other, "Inne" }
        });

            EnumManager.Register(new Dictionary<NeoSensoricsCategory, string>
        {
            { NeoSensoricsCategory.Shared, "Wsp�lna" },
            { NeoSensoricsCategory.Futuresight, "Futurispecja" },
            { NeoSensoricsCategory.Neospiritism, "Neospirytyzm" },
            { NeoSensoricsCategory.Biomancy, "Biomancja" }
        });

            EnumManager.Register(new Dictionary<PsionicsCategory, string>
        {
            { PsionicsCategory.Shared, "Wsp�lna" },
            { PsionicsCategory.Telekinesis, "Telekineza" },
            { PsionicsCategory.Pyrokinesis, "Pyrokineza" },
            { PsionicsCategory.Psychokinesis, "Psychokineza" }
        });

            EnumManager.Register(new Dictionary<TypesCategory, string>
        {
            { TypesCategory.Talent, "Talent" },
            { TypesCategory.Discipline, "Dyscyplina" }
        });

            EnumManager.Register(new Dictionary<SavingThrowType, string>
        {
            { SavingThrowType.Strength, "Si�a" },
            { SavingThrowType.Dexterity, "Zr�czno��" },
            { SavingThrowType.Constitution, "Budowa" },
            { SavingThrowType.Intelligence, "Inteligencja" },
            { SavingThrowType.Senses, "Zmys�y" },
            { SavingThrowType.Charisma, "Charyzma" }
        });
        EnumManager.Register(new Dictionary<ReductionDiceType, string>
        {
            { ReductionDiceType.d8, "k8" },
            { ReductionDiceType.d10, "k10" },
            { ReductionDiceType.d12, "k12" }
        });
        EnumManager.Register(new Dictionary<AbilityType, string>
        {
            { AbilityType.Acrobatics, "Akrobatyka" },
            { AbilityType.Athletics, "Atletyka" },
            { AbilityType.Biology, "Biologia" },
            { AbilityType.Empathy, "Empatia" },
            { AbilityType.Insight, "Intuicja" },
            { AbilityType.Medicine, "Medycyna" },
            { AbilityType.Neoscience, "Neonauka" },
            { AbilityType.Perception, "Percepcja" },
            { AbilityType.Precision, "Precyzja" },
            { AbilityType.Survival, "Przetrwanie" },
            { AbilityType.Stealth, "Skradanie" },
            { AbilityType.Investigation, "�ledztwo" },
            { AbilityType.Technology, "Technologia" },
            { AbilityType.Intimidation, "Zastraszanie" }
        });

        Debug.Log(EnumManager.GetTotalTranslationCount());
    }
}
