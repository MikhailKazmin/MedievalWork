using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ell.BaseScripts;

namespace Ell.Workers
{
    interface IWorker : IObjectLogicsGame
    {
        IEnumerator Move();
    }
}
