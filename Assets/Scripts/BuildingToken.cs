using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingToken : MonoBehaviour, IBuffable
{
    [SerializeField] float _buffIncrease = .5f;
    public void Buff()
    {
        transform.localScale = new Vector3(
            transform.localScale.x + _buffIncrease,
            transform.localScale.y + _buffIncrease,
            transform.localScale.z + _buffIncrease);
    }

    public void Unbuff()
    {
        transform.localScale = new Vector3(
            transform.localScale.x - _buffIncrease,
            transform.localScale.y - _buffIncrease,
            transform.localScale.z - _buffIncrease);
    }
}
