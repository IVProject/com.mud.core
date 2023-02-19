using UnityEngine;

namespace Mud
{
    public static partial class TransformExtension
    {
        public static void LookTowards(this Transform transform, Vector3 to)
        {
            var direction = (to - transform.position).normalized;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
        }

        public static void LookTowards(this Transform transform, Transform to) => LookTowards(transform, to.position);

        public static bool IsLookAt(this Transform transform, Transform target, float accuracy = 0.05f)
        {
            var direction = target.position - transform.position;
            var turnAmount = Quaternion.FromToRotation(transform.forward, direction).y;
            
            return Mathf.Abs(turnAmount) < accuracy;
        }
    }
}