using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comburo
{
    public class StickPosition : MonoBehaviour
    {
        public Player player;

        void Update()
        {
            transform.position = player.transform.position;
        }
    }
}

