using UnityEngine;

namespace Comburo
{
    public class NemesisElemental : Enemy
    {
        public ElementalType[] elementalVariantsTypes;
        public float speedModifier;
        private Grow playerGrow;

        public override void Initialize(string name, Transform parent, float spawninigSpeed, Vector3 spawningPosition)
        {
            base.Initialize(name,parent,spawninigSpeed, spawningPosition);
            //spawnerManager.nemesis.Add(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            Kill(other);
        }

        public void setVariantType()
        {

            int index = Random.Range(0,elementalVariantsTypes.Length-1);

            Debug.Log("Variants: " + elementalVariantsTypes.Length + " /> " + index);

            transform.localScale = new Vector3(elementalVariantsTypes[index].size, elementalVariantsTypes[index].size, elementalVariantsTypes[index].size);
            speedModifier = elementalVariantsTypes[index].speedModifier;
        }

        public override void Desapear()
        {
            
            base.Desapear();
        }


        public override void Kill(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerGrow = other.GetComponent<Grow>();
                if (playerGrow != null)
                    playerGrow.player.Die();

            }
        }

        public override void Move()
        {
            transform.Translate(Vector3.forward);
        }
    }


}
