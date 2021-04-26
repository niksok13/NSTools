using UnityEngine;
using UnityEngine.EventSystems;

namespace NSTools.Controls
{
    public enum JoystickState
    {
        Press,
        Move,
        Release
    }
    public struct JoystickSignal
    {
        public Vector2 value;
        public JoystickState state;
    }
    public class ZJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public float sensitivity = 1;
        
        private JoystickSignal joyValue = new JoystickSignal();
        public void OnPointerDown(PointerEventData eventData)
        {
            joyValue.state = JoystickState.Press;
            joyValue.value = Vector2.zero;
            Game.Fsm.Signal(joyValue);
        }

        public void OnDrag(PointerEventData eventData)
        {
            joyValue.state = JoystickState.Move;
            joyValue.value += eventData.delta * sensitivity / Screen.dpi;
            joyValue.value = Vector2.ClampMagnitude(joyValue.value,1);
            Game.Fsm.Signal(joyValue);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            joyValue.state = JoystickState.Release;
            joyValue.value += eventData.delta * sensitivity / Screen.dpi;
            joyValue.value = Vector2.ClampMagnitude(joyValue.value,1);
            Game.Fsm.Signal(joyValue);
        }

    }
}
