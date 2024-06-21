using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManigingExtensions
{
    public static Character GetCharacterData(this DataManager manager, int dataId)
    {
        var loadedCharacterList = manager.LoadedCharacterList;
        if (loadedCharacterList.Count == 0
            || loadedCharacterList.ContainsKey(dataId) == false)
        {
            return null;    
        }

        return loadedCharacterList[dataId];
    }

    //public static Skill GetSkillData(this DataManager manager, string dataClassName)
    //{
    //    var loadedSkillList = manager.LoadedSkillList;
    //    if (loadedSkillList.Count == 0
    //        || loadedSkillList.ContainsKey(dataClassName) == false)
    //    {
    //        return null;
    //    }

    //    return loadedSkillList[dataClassName];
    //}

    //public static Buff GetBuffData(this DataManager manager, string dataClassName)
    //{
    //    var loadedBuffList = manager.LoadedBuffList;
    //    if (loadedBuffList.Count == 0
    //       || loadedBuffList.ContainsKey(dataClassName) == false)
    //    {
    //        return null;
    //    }

    //    return loadedBuffList[dataClassName];
    //}

    //public static string GetSkillName(this DataManager manager, string dataClassName)
    //{
    //    var skillData = manager.GetSkillData(dataClassName);
    //    return (skillData != null) ? skillData.Name : string.Empty;
    //}

    //public static string GetBuffDescription(this DataManager manager, string dataClassName)
    //{
    //    var buffData = manager.GetBuffData(dataClassName);
    //    string desc = string.Empty;
    //    if(buffData != null)
    //    {
    //        desc = string.Format(buffData.Description, buffData.BuffValues);
    //    }
    //    return desc;
    //}
    public static Item GetIteminfo(this DataManager manager, string dataClassName)
    {

        var ItemList = manager.LoadedItemList;
        if (ItemList.Count == 0 || ItemList.ContainsKey(dataClassName) == false)
        {
            return null;
        }
        return ItemList[dataClassName];
    }
}
