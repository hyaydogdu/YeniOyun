using UnityEditor;
using UnityEngine;

namespace Mygame
{
    [System.Serializable]
    [CustomEditor(typeof(E_ControllerBase), true)]
    public class E_ControllerBaseEditor : Editor
    {
        private void OnSceneGUI()
        {
            E_ControllerBase controller = (E_ControllerBase)target;
            Handles.color = Color.white;
            Handles.DrawWireArc(controller.transform.position, Vector3.forward, Vector3.right, 360, controller.radius);

            Vector3 viewAngle01 = DirectionFromAngle(controller.transform.eulerAngles.z, -controller.angle / 2);
            Vector3 viewAngle02 = DirectionFromAngle(controller.transform.eulerAngles.z, controller.angle / 2);

            Handles.DrawLine(controller.transform.position, controller.transform.position + viewAngle01 * controller.radius);
            Handles.DrawLine(controller.transform.position, controller.transform.position + viewAngle02 * controller.radius);

            Handles.color = Color.yellow;

            if (controller.canSeePlayer)
            {
                Handles.color = Color.green;
                Handles.DrawLine(controller.transform.position, controller.playerRef.transform.position);
            }
        }

        private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;

            return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0);
        }
    }
}
