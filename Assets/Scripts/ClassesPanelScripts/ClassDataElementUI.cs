using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassDataElementUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI classNameText;
    [SerializeField]
    private TextMeshProUGUI classDescriptionText;
    [SerializeField]
    private TextMeshProUGUI subclassesDescriptionText;
    [SerializeField]
    private TextMeshProUGUI skillsDescriptionText;

    public void Initialize(ClassData data)
    {
        classNameText.text = data.className;
        classDescriptionText.text = "<size=150%>Opis:</size><br>"+data.classDescription;
        subclassesDescriptionText.text = "<size=150%>Subklasy:</size><br>" + string.Join("<br>", data.subclasses);
        skillsDescriptionText.text = "<size=150%>Umiejêtnoœci:</size><br>" + string.Join("<br>", data.classSkills);
    }
}
