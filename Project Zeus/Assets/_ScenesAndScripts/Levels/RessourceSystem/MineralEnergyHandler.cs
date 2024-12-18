using NUnit.Framework;
using UnityEngine;

public class MineralEnergyHandler : MonoBehaviour
{

    #region Variables

    [Header("Game Design Variables")]
    public int remainingEnergy = 100;

    #endregion



    #region Unity Build In

    private void Update()
    {
        if (remainingEnergy <= 0)
        {
            Destroy(gameObject);
        }
    }

    #endregion

}
