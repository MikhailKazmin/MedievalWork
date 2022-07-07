using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ResourcesItem
{
    [CreateAssetMenu(menuName = "Resources", fileName = "New Resource")]
    public class Data : ScriptableObject, IResources
    {
        public Sprite sprite;
        public ResourcesName resourcesName;
        public int Cost;
    }


    class NewCount
    {
        public string Count;



    }

    public enum Count
    {
        k = 10 ^ 3,
        M = 10 ^ 6,
        a = 10 ^ 9,
        b = 10 ^ 12,
        c = 10 ^ 15,
        d = 10 ^ 18,
        e = 10 ^ 21,
        f = 10 ^ 24
    }
}
