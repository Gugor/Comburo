using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comburo.ScriptableObjects
{

    public abstract class AbstractSpawningModeSO : ScriptableObject
    {
        [Tooltip("This mode is affected by the global difficulty of the game or not")]
        public bool difficultyIsGlobal = true;
        [Tooltip("How objects are going to be sapwned through time. Paralel: All at once or Secuence: one at a time.")]
        public SpawningMachineType spawningMachineType;
        [Tooltip("The spawner you want to start spawning from. From 0 to 45.")]
        [Range(0, 45)]
        public int startingSpawner;
        [Range(1, 46)]
        public int offsetEveryOf;
        [Tooltip("Tick this if you want to take in account the max life time when using this mode")]
        public bool useMaxLifeTime = true;
        [Tooltip("The maximum time this mode can be used for spawning.")]
        [Range(1f, 100.0f)]
        public float maxLifeTime;
        [Tooltip("Modifies the global spawning speed")]
        [Range(0.1f, 50.0f)]
        public float timeBetweenSpawnings = 1.0f;
        [Range(0.1f, 5.0f)]
        public float elementalSpeedModifier = 1.0f;

    }
}
