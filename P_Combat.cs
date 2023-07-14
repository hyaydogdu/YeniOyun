using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Combat : MonoBehaviour
{
    public bool _canGiveDamage;

    public float _closeCombatDistance;

    public string _targetLayer;
    public LayerMask _seenableLayers;
    public RaycastHit2D raycastHit2D;

    [SerializeField] private GameObject _meleePoint;
    [SerializeField] private GameObject _SpellPrefab;

    public static Vector2 attackDir;

    #region BuiltIn Methods
    private void Start()
    {
        StartCoroutine(CloseCombatRoutine());
        Cursor.visible = false;
    }

    private void Update()
    {
        AttackDirCal(P_Controller._mouseposition, P_Controller._facingLeft);
        CloseCombat(ref P_Controller._shouldAttack);
        SpellCombat(ref P_Controller._shouldSpell);
    }
    #endregion

    #region Coroutines For Start Methods
    private IEnumerator CloseCombatRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            CloseCombatCheck();
        }
    }
    #endregion


    #region Calculation Methods
    private void AttackDirCal(Vector2 mousePos, bool facingLeft)
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(mousePos);
        if (pos.y > 0)
            pos /= 5;
        else
            pos /= 25;

        float yValue = pos.y;

        // print(yValue);

        float xValue = 1 - Mathf.Pow(yValue, 2);
        xValue = Mathf.Pow(xValue, 1 / 2);

        if (facingLeft)
            xValue = -xValue;

        attackDir = new Vector2(xValue, yValue);
        // print("attackdir : " + attackDir + "attackdirNormalized : " + attackDir.normalized);
        attackDir.Normalize();
        _meleePoint.transform.position = transform.position + (Vector3)attackDir;
    }

    #endregion


    #region Raycast Methods
    private void CloseCombatCheck()
    {
        raycastHit2D = Physics2D.Raycast(transform.position, transform.right, _closeCombatDistance, _seenableLayers);

        // print(attackDir * _closeCombatDistance);
        if (raycastHit2D)
        {
            GameObject rayObject = raycastHit2D.transform.gameObject;
            // print(rayObject);
            if (rayObject.layer == LayerMask.NameToLayer(_targetLayer))
            {
                _canGiveDamage = true;
            }
        }
        else
            _canGiveDamage = false;
    }
    #endregion

    #region Behaviour Methods
    private void CloseCombat(ref bool shouldAttack)
    {
        if (shouldAttack && _canGiveDamage)
        {
            shouldAttack = false;
            print("giveDamage");
            raycastHit2D.collider.gameObject.GetComponent<E_ControllerBase>().TakeDamage(50);
        }
        else
            shouldAttack = false;
    }

    private void SpellCombat(ref bool shouldSpell)
    {
        if (shouldSpell)
        {
            shouldSpell = false;
            Instantiate(_SpellPrefab, transform.position, Quaternion.Euler(0, 0, 0));
        }
    }
    #endregion
}
