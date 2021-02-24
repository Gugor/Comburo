using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comburo
{
    public interface IPowerable
    {
        void SetInUI();
        void Use();
        void Extinguish();
    }
}
