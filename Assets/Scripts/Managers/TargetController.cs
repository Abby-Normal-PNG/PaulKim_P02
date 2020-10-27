using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    //TODO build a more structured connection
    public static ITargetable CurrentTarget;
    [SerializeField] Date _objectToTarget = null;

    private void Start()
    {
        //Debug.Log("Checking " + _objectToTarget.name + " for targetable");
        ITargetable possibleTarget = _objectToTarget.GetComponent<ITargetable>();
        if (possibleTarget != null)
        {
            Debug.Log("New Target Acquired!");
            CurrentTarget = possibleTarget;
            _objectToTarget.Target();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Debug.Log("Checking " + _objectToTarget.name + " for targetable");
            ITargetable possibleTarget = _objectToTarget.GetComponent<ITargetable>();
            if(possibleTarget != null)
            {
                Debug.Log("New Target Acquired!");
                CurrentTarget = possibleTarget;
                _objectToTarget.Target();
            }
        }
    }
}
