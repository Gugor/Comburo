using UnityEngine;


namespace Comburo
{
    public class PlayerMovement : MonoBehaviour
    {
        public Joystick joystick;

        private Player player;

        private float zAxis;
        private float xAxis;

        private Rigidbody rb;


        private void Awake()
        {
            player = GetComponent<Player>();
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            xAxis = joystick.Horizontal;
            zAxis = joystick.Vertical;
        }

        private void FixedUpdate()
        {
            //Move player with joystick
            rb.position += new Vector3(xAxis * player.speed * Time.deltaTime,0,zAxis * player.speed * Time.deltaTime);

            //Rotate player with joystick
            float angle = Mathf.Atan2(joystick.Direction.x,-joystick.Direction.y) * Mathf.Rad2Deg ;
            //Debug.Log(joystick.Direction.x);
            if (joystick.Direction.x != 0)
            {
                rb.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, -angle,transform.rotation.z),20f * Time.fixedDeltaTime);
            }
        }
    }
}

