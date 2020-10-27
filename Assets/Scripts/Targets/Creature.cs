using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour, ITargetable, IDamageable
{
    [SerializeField] int _maxHealth = 1;
    private int _currentHealth;
    public int Health => _currentHealth;
    [SerializeField] int _strength = 1;
    public int Strength => _strength;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public virtual void Target()
    {
        Debug.Log("Creature has been targeted");
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        Debug.Log("Took damage. Remaining health: " + _currentHealth);
        if(_currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Debug.Log("Creature has been killed!");
        gameObject.SetActive(false);
    }
}
