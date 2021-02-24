using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comburo
{
    public interface ISpawneable
    {
        int SpawningWeight { get; set; }
        Transform iTransform { get; }
        GameObject iGameObject { get; }

        void Initialize(string name, Transform target, Transform parent, Vector3 spawningPosition,SpawnedPoolManager poolToBeSpawned);
    }
}
