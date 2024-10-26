using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [System.Serializable]
    public class FruitIcons
    {
        public Sprite icon;
        public string key;
    }

    public static class FruitIconsExtension
    {
        public static Sprite GetIcon(this List<FruitIcons> list, string key)
        {
            foreach (var item in list)
            {
                if (item.key == key)
                {
                    return item.icon;
                }
            }

            return null;
        }
    }
}

        