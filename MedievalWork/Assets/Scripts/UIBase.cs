using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public abstract class UIBase : MonoBehaviour
    {
        protected static Transform _MenuObjects;
        protected static TextMeshProUGUI _Name;
        protected static Button _Exit;
        protected static Button _Next;
        protected static Button _Previous;
        protected Transform _Panel;

    }
}
