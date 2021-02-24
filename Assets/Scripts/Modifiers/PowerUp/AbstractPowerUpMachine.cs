using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;

namespace Comburo
{
    public abstract class AbstractPowerUpMachine : MonoBehaviour
    {
        [Header("Dependencies")]
        public PowerUpManager powerUpManager;
        public SpawnerManager spawnerManager;
        [Space(10)]

        [Header("Settings")]
        [Range(1f, 15f)]
        public int useTime;
        [Space(10)]
        public List<PathCreator> paths;
        [Space(10)]
        [HideInInspector] public bool isActive; // Is walking in the scene but not being used.
        [HideInInspector] public bool isBeingUsed; //Player has pressed button and powerup is running


        private PathFollower pathFollower;

        public abstract void Set();
        public abstract IEnumerator Use();
        public abstract void Extinguish();

        public virtual PathCreator GetRandomPath()
        {
            int randIndex = Mathf.RoundToInt(Random.Range(0, paths.Count));
            return paths[randIndex];
        }

        private void Awake()
        {
            pathFollower = GetComponent<PathFollower>();
        }
    }
}