using System.Collections;
using System.Collections.Generic;
using Comburo.ScriptableObjects;
using UnityEngine;

namespace Comburo
{
    [CreateAssetMenu(fileName ="Comburo", menuName ="Comburo/Data/SpawnerManagerData")]
    public class SpawnerManagerData : ScriptableObject
    {
        [Range(0f, 10f)]
        public float elementalsSpeed;
        [Range(0f, 10f)]
        public float spawningDelay; //Absolute speed to be modified by the spawning mode.
        [Header("Random parameters")]
        public int minTimeToChangeMode = 10;
        public int maxTimeToChangeMode = 20;
    }
}