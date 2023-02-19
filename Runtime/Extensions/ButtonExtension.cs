using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mud
{
    public static partial class ButtonExtension
    {
        public static void Click(this Button button)
        {
            var eventData = new PointerEventData(EventSystem.current);
            ExecuteEvents.Execute(button.gameObject, eventData, ExecuteEvents.pointerEnterHandler);
            ExecuteEvents.Execute(button.gameObject, eventData, ExecuteEvents.submitHandler);
        }
    }
}