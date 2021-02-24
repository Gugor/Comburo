using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comburo.Utilities
{
    public class ErrorHandler : MonoBehaviour
    {
        private static ErrorHandler _instance;

        public static ErrorHandler Instance { get => _instance; }
        // Start is called before the first frame update
        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }

        
    }
}
