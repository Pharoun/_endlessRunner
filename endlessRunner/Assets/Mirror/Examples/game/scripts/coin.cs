using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.Examples.game
{
    public class coin : NetworkBehaviour
    {
        [ServerCallback]
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                GetComponent<AudioSource>().Play();
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                StartCoroutine(waitBeforeEnabling());
            }
        }

        IEnumerator waitBeforeEnabling()
        {
            yield return new WaitForSeconds(10);
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
