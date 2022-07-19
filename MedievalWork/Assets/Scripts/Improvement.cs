using UnityEditor;
using UnityEngine;

namespace Ell.BaseScripts
{
    [CreateAssetMenu(menuName = "Improvements", fileName = "New Improvement")]
    public class Improvement : ScriptableObject
    {
        public Sprite Icon;
        public string Name;
        public string LVL;
        public string Count;
        public int[] Cost;

    }
}