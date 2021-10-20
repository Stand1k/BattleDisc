using UnityEngine;

namespace BattleDisc
{
    public class MathUtils
    {
        public static Vector3 BounceDirection(Vector3 direction, Transform transform)
        {
            float normalAngle = (transform.eulerAngles.y) * Mathf.Deg2Rad;
            Vector3 normalDir = new Vector3(Mathf.Cos(normalAngle), 0f, Mathf.Sin(normalAngle));

            return Vector3.Reflect(direction, normalDir);
        }
    }
}