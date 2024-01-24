using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGameJam
{
    public interface IInitializable
    {
        IEnumerator Initialize();
    }
}