using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellData : ScriptableObject
{
    public NeoScienceCategory neoScienceCategory;

    public string spellName;

    [TextArea(1, 10)]
    public string spellDescription;
}

//public enum NeoScienceCategory
//{
//    Neophysics,
//    Neolinguistics,
//    Neosensorics,
//    Psionics
//}

//public enum NeoPhysicsCategory
//{
//    Grawiturgia,
//    Chronoturgia,
//    Energodynamika,
//    Neooptyka,
//    Neoakustyka,
//    Inne
//}
//public enum NeoSensoricsCategory
//{
//    Wsp�lna,
//    Futurispecja,
//    Neospirytyzm,
//    Biomancja
//}
//public enum PsionicsCategory
//{
//    Wsp�lna,
//    Telekineza,
//    Pyrokineza,
//    Psychokineza
//}

//public enum TypesCategory
//{
//    Talent,
//    Dyscyplina
//}

