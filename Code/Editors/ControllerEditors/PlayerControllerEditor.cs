using UnityEngine;
using UnityEditor;

namespace Mygame
{
    [System.Serializable]
    [CustomEditor(typeof(P_Controller))]
    public class PlayerControllerEditor : Editor
    {
        private void OnSceneGUI()
        {
            P_Controller controller = (P_Controller)target;
            Handles.color = Color.cyan;
            //Ground Check
            Handles.DrawWireCube(controller.transform.position - controller.transform.up * controller._castDistanceForGround, controller._boxsize);
            //Wall Check
            Handles.DrawWireArc(controller.wallCheck.position, Vector3.forward, Vector3.right, 360, controller.wallCheckRadius);
        }
    }
}
