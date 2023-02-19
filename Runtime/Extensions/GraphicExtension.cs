using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Mud
{
    public static partial class GraphicExtension
    {
        public static void FadeAlpha(this Graphic graphic, float targetAlpha, float duration, bool ignoreTimeScale)
        {
            Color fixedColor = graphic.color;
            fixedColor.a = 1;
            graphic.color = fixedColor;
            graphic.CrossFadeAlpha(0f, 0f, true);
            graphic.CrossFadeAlpha(targetAlpha, duration, ignoreTimeScale);
        }

        public static IEnumerator FadeAlphaCoroutine(this Graphic graphic, float targetAlpha, float duration)
        {
            graphic.FadeAlpha(targetAlpha, duration, false);
            yield return new WaitForSeconds(duration);
        }
    }
}