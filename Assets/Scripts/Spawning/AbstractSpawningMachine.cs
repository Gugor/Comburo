using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Comburo.ScriptableObjects;

namespace Comburo
{

    public abstract class AbstractSpawningMachine
    {
        public virtual float RemainingTimeToSpawn { get; protected set; }
        //protected AbstractSpawnerManager<T> spawnerManager;

        public abstract void Init();


        public abstract void Use();

    }
}
