using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class showinitem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Text_SkillName;

    private string DataClassName;
    private void Start()
    {
       
    }

    public void SetUI(string skillClassName)
    {
        DataClassName= skillClassName;

        var skillData = DataManager.Inst.GetIteminfo(DataClassName);
        if (skillData != null)
        {
            Text_SkillName.text = skillData.Name;
            //var path = $"Textures/SkillIcons/{skillData.IconName}";
            //Image_Icon.sprite = Resources.Load<Sprite>(path);
        }
    }


    public void OnClick_OpenTooltip(string skillClassName)
    {
         DataClassName= Text_SkillName.text;
        var skillData = DataManager.Inst.GetIteminfo(DataClassName);
        if (skillData == null)
            return;

        Debug.Log(skillData.Desc);
        Debug.Log(skillData.Price);
        Debug.Log(skillData.DataClassName);
        Debug.Log(skillData.Name);
    }
}
