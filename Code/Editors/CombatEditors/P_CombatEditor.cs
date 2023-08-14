using UnityEngine;
using UnityEditor;

namespace Mygame
{
    [System.Serializable]
    [CustomEditor(typeof(P_Combat))]
    public class P_CombatEditor : Editor
    {
        private void OnSceneGUI()
        {
            P_Combat controller = (P_Combat)target;
            if (controller._canGiveDamage)
                Handles.color = Color.green;
            else
                Handles.color = Color.red;

            if (controller.raycastHit2D)
                Handles.DrawLine(controller.transform.position, controller.raycastHit2D.point);
        }

    }
}
