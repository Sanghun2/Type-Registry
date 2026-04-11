using System;
using System.Collections.Generic;
using UnityEngine;

namespace BilliotGames
{
    public class TypeRegistry<Tin, Tout>
    {
        private Dictionary<Type, Tout> typeDict = new();

        public void Register<TType>(Tout target) where TType : Tin {
            var targetType = typeof(TType);
            if (typeDict.ContainsKey(targetType)) {
                Debug.Log($"({targetType}) registry replaced");
            }

            typeDict[targetType] = target;
        }
        public void Register(Type targetType, Tout target) {
            if (!typeof(Tin).IsAssignableFrom(targetType)) {
                Debug.LogError($"<color=red>{targetType} is not assignable from {typeof(Tin)}</color>");
                return;
            }
            typeDict[targetType] = target;
        }
        public void Unregister<TType>() where TType : Tin {
            typeDict.Remove(typeof(TType));
        }

        public bool TryGet(Tin targetType, out Tout result) {
            if (typeDict.TryGetValue(targetType.GetType(), out result)) {
                return true;
            }

            Debug.LogError($"<color=red>type ({typeof(Tin)}) is not exist</color>");
            return false;
        }
        public bool TryGet<TKey, TOut>(out TOut result)
            where TKey : Tin
            where TOut : Tout {
            if (typeDict.TryGetValue(typeof(TKey), out var found) && found is TOut typed) {
                result = typed;
                return true;
            }
            result = default;
            Debug.LogError($"<color=red>type ({typeof(TKey)}) is not exist</color>");
            return false;
        }
    }
}
