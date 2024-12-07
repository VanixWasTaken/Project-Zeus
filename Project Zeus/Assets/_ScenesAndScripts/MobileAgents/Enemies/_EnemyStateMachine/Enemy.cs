using UnityEngine;

public class Enemy : MonoBehaviour
{

    #region Variables

    public int health = 100;

    #endregion


    #region Unity Build In

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    #endregion



    #region Custom Functions()

    public void TakeDamage(int _damage)
    {
        health -= _damage;
    }

    #endregion
}
