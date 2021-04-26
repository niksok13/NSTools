using UnityEngine;

namespace NSTools
{
    public abstract class BasePrefs
    {
        protected BasePrefs(){
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("prefs_data","{}"),this);
        }
    
        public void Save(){
            PlayerPrefs.SetString("prefs_data",JsonUtility.ToJson(this));
        }

        /// TODO - Encrypt
    }
}