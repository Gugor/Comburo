using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comburo
{
    public abstract class SpawnerBase<T> : MonoBehaviour
    {
        #region Public variables
        public int indexID = 0;
        public GameObject nextToSpawn;
        public Transform target;
        public ScriptableObjects.GizmosSO gizmosSO;
        public bool canSpawn;
        public T spawnerManager;
        public SpawningModeManager spawningModeManager;
        #endregion

        #region Protected Variables

        #endregion

        #region Builtin Methods

        #endregion

        #region Main Mehtods
        #endregion
        #region Abstract Methods
        public abstract GameObject SpawnObject(ISpawneable go);
        public abstract void Initialize(int indexID, T spawnerManager, Transform _target);
        #endregion
    }
}
