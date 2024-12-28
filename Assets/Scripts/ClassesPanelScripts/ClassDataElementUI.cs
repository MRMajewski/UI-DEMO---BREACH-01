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
 //   [SerializeField]
 //   private Image classIcon;
    public void Initialize(ClassData data)
    {
        classNameText.text = data.itemName;
        classDescriptionText.text = data.description;
        subclassesDescriptionText.text = data.subclassesDescription;
       // classIcon.sprite = data.classIcon;

        // £¹czenie umiejêtnoœci w jeden tekst
        skillsDescriptionText.text = string.Join(", ", data.skills);
    }
}
