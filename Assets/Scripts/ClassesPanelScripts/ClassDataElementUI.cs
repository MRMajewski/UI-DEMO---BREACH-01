using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        classNameText.text = data.itemName;
        classDescriptionText.text = data.description;
        subclassesDescriptionText.text = data.subclassesDescription;

        // £¹czenie umiejêtnoœci w jeden tekst
        skillsDescriptionText.text = string.Join(", ", data.skills);
    }
}
