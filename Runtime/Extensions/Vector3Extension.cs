using UnityEngine;

namespace Mud
{
    public static partial class Vector3Extension
    {
        public static Vector2 ToVector2AsXZ(this Vector3 vector) => new Vector2(vector.x, vector.z);
        public static Vector2 ToVector2AsXY(this Vector3 vector) => new Vector2(vector.x, vector.y);
        public static Vector2 ToVector2AsYZ(this Vector3 vector) => new Vector2(vector.y, vector.z);
    }
}