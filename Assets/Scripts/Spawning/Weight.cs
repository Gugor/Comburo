using System.Collections.Generic;
using UnityEngine;

namespace Comburo
{
    public static class Weight
    {
        static public ISpawneable GetRandomWeighted(List<Weighted> weighteds)
        {
            if (weighteds.Count <= 0) { return null; }

            int randomWeight = Mathf.RoundToInt(UnityEngine.Random.Range(0, Weight.SumWeights(weighteds)));


            GameObject returnedWeighted;
            foreach (Weighted weighted in weighteds)
            {

                if (randomWeight < weighted.weight)
                {
                    returnedWeighted = weighted.spawneablePrefab;
                    Debug.Log("Next elemental to spawn:" + returnedWeighted.name);
                    return returnedWeighted.GetComponent<ISpawneable>();
                }
                randomWeight -= weighted.weight;
            }

            return null;
        }

        static public int SumWeights(List<Weighted> weighteds)
        {
            int sum = 0;
            foreach (Weighted weighted in weighteds)
            {
                sum += weighted.weight;
            }
            return sum;
        }
    }
}