using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public enum ReductionDiceType
{
    k8,
    k10,
    k12
}
public enum SavingThrowType
{
    Si³a,
    Zrêcznoœæ,
    Budowa,
    Inteligencja,
    Zmys³y,
    Charyzma
}

public enum AbilityType
{
    Akrobatyka,
    Atletyka,
    Biologia,
    Empatia,
    Intuicja,
    Medycyna,
    Neonauka,
    Percepcja,
    Precyzja,
    Przetrwanie,
    Skradanie,
    Œledztwo,
    Technologia,
    Zastraszanie
}











public class ClassesDataBase : MonoBehaviour
{
    [System.Serializable]
    public struct ReductionDiceDescription
    {
        public ReductionDiceType type;
        [TextArea(1, 5)]
        public string description;
    }

    [System.Serializable]
    public struct SavingThrowDescription
    {
        public SavingThrowType type;
        [TextArea(1, 5)]
        public string description;
    }

    [System.Serializable]
    public struct AbilityDescription
    {
        public AbilityType type;
        [TextArea(1, 5)]
        public string description;
    }

    // Listy opisów dla poszczególnych enumów
    public List<ReductionDiceDescription> reductionDiceDescriptions = new List<ReductionDiceDescription>();
    public List<SavingThrowDescription> savingThrowDescriptions = new List<SavingThrowDescription>();
    public List<AbilityDescription> abilityDescriptions = new List<AbilityDescription>();

    public List<ClassData> AllClasses = new List<ClassData>();

    // Metoda do pobierania opisu ReductionDiceType
    public string GetReductionDiceDescription(ReductionDiceType type)
    {
        var description = reductionDiceDescriptions.FirstOrDefault(d => d.type == type).description;
        return string.IsNullOrEmpty(description) ? "Brak opisu dla tego typu" : description;
    }

    // Metoda do pobierania opisu SavingThrowType
    public string GetSavingThrowDescription(SavingThrowType type)
    {
        var description = savingThrowDescriptions.FirstOrDefault(d => d.type == type).description;
        return string.IsNullOrEmpty(description) ? "Brak opisu dla tego typu" : description;
    }

    // Metoda do pobierania opisu AbilityType
    public string GetAbilityDescription(AbilityType type)
    {
        var description = abilityDescriptions.FirstOrDefault(d => d.type == type).description;
        return string.IsNullOrEmpty(description) ? "Brak opisu dla tej zdolnoœci" : description;
    }

}
