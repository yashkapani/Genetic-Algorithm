using UnityEngine;
using System.Collections;

namespace AISandbox {
    [RequireComponent(typeof (IActor))]
    public class PlayerController : MonoBehaviour {
        private IActor _actor;

		private void Awake() {
            _actor = GetComponent<IActor>();
        }

        private void FixedUpdate() {

            // Read the inputs.
			float y1_axis = Input.GetAxis("LeftTread");
			float y2_axis = Input.GetAxis("RightTread");


            // Pass all parameters to the character control script.
            _actor.SetInput( y1_axis,y2_axis );
        }
    }
}