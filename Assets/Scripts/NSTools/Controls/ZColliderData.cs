using UnityEngine;

namespace NSTools.Controls
{
    public class ZColliderData : MonoBehaviour
    {
        public string key, value;
        private void OnCollisionEnter(Collision other) {
            if (!other.gameObject.CompareTag("Player")) return;
            Log.Trace($"{this} FSM.Signal({key})", gameObject);
            Game.Data.Set(key,value);        
        }

        private void OnTriggerEnter(Collider other) {
            if (!other.gameObject.CompareTag("Player")) return;
            Log.Trace($"{this} FSM.Signal({key})", gameObject);
            Game.Data.Set(key,value);        
        }
    
    }
}