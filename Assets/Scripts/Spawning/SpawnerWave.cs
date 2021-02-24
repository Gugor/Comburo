using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comburo
{
    public class SpawnerWave : SpawnerBase<WaveSpawnerManager>
    {
        #region Public Variables
        #endregion

        #region Private Variables
        #endregion

        #region Main Mathods
        public override void Initialize(int _indexID, WaveSpawnerManager _spm, Transform _target)
        {
            this.spawnerManager = _spm;
            this.target = _target;
            this.indexID = _indexID;
        }
        /// <summary>
        /// Spawns single ISpawneable object at this spawner
        /// </summary>
        /// <param name="go">Object to be spawned</param>
        /// <returns></returns>
        public override GameObject SpawnObject(ISpawneable go)
        {
            Debug.Log("Spawn object: " + go.iTransform.name);

            GameObject instance = Instantiate(go.iGameObject, transform.position, go.iTransform.rotation);

            instance.GetComponent<ISpawneable>().Initialize(name, target, spawnerManager.elementalsParent, transform.position, spawnerManager.spawnedPool);

            return instance;
        }
        #endregion

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (gizmosSO == null) { return; }
            //Gizmos.color = gizmosSO.color;
            //Gizmos.DrawCube(transform.position, new Vector3(gizmosSO.size, gizmosSO.size, gizmosSO.size));
            Gizmos.color = gizmosSO.color;
            Gizmos.DrawSphere(transform.position, gizmosSO.size);
        }
        #endif
    }
}
