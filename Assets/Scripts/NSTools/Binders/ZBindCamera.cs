using UnityEngine;

namespace NSTools.Binders
{
    public class ZBindCamera : MonoBehaviour
    {
        public Transform targetTransform;
        private Transform cameraTransform;

        void Awake()
        {
            cameraTransform = transform;
            Game.Data.Bind<Transform>("camera_target", SetCameraTarget);
        }

        private void SetCameraTarget(Transform newTarget)
        {
            if (newTarget == null) return;
            targetTransform = newTarget;
        }
    
        private void Update()
        {
            if (targetTransform == null) return;
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetTransform.position, 0.1f);
            cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation,targetTransform.rotation,0.1f);
        }
    }
}
