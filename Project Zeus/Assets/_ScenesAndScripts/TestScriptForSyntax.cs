using UnityEngine;

public class TestScriptForSyntax : MonoBehaviour
{

    #region References

    [SerializeField] GameObject testGO;

    #endregion

    #region Variables

    private int testInt;

    #endregion


    #region Unity Build In

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    #region Colliders

    private void OnTriggerEnter(Collider other)
    {
        
    }

    #endregion

    #region Buttons



    #endregion

    #endregion



    #region Custom Functions()

    private void TestCustomFunction() // This is a test description
    {
        
    }

    private void TestCustomFunction2()
    {
        /// <summary>
        /// This is step 1 explanation.
        /// Now you can read step 2 in line 2.
        /// 
        /// Great right?
        /// </summary>


    }


    #region GetSet Functions()



    #endregion

    #endregion
}
