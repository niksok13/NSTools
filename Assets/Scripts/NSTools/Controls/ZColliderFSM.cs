using UnityEngine;

namespace NSTools.Controls
{
    public class ZColliderFSM : MonoBehaviour
    {
        public string key;
        private void OnCollisionEnter(Collision other) {
            if (!other.gameObject.CompareTag("Player")) return;
            Log.Trace($"{this} FSM.Signal({key})", gameObject);
            Game.Fsm.Signal(key);        
        }

        private void OnTriggerEnter(Collider other) {
            if (!other.gameObject.CompareTag("Player")) return;
            Log.Trace($"{this} FSM.Signal({key})", gameObject);
            Game.Fsm.Signal(key);         
        }
    
    }
}
