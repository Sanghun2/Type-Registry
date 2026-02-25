using System;
using System.Collections.Generic;
using UnityEngine;

public class TypeRegistry<Tin, Tout>
{
    private Dictionary<Type, Tout> typeDict = new();


    public void Register<TType>(Tout target) where TType : Tin {
        var targetType = typeof(TType);
        if (!typeDict.ContainsKey(targetType)) {
            Debug.Log($"new ({targetType}) registry registered");
        }

        typeDict[targetType] = target;
    }
    public void Unregister<TType>() where TType : Tin {
        typeDict.Remove(typeof(TType));
    }

    public bool TryGet(Tin targetType, out Tout result){
        if (typeDict.TryGetValue(targetType.GetType(), out result)) {
            return true;
        }

        return false;
    }
}
