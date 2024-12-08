using UnityEngine;

public class CylinderFiller : MonoBehaviour
{

    [SerializeField] Renderer renderer;
    [SerializeField] MaterialPropertyBlock materialPropertyBlock;
    public float fillValue = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fillValue = 0.0f;
        materialPropertyBlock = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetFloat("_Fill", fillValue);
        renderer.SetPropertyBlock(materialPropertyBlock);
    }

    // Update is called once per frame
    public void ChangeFill()
    {
        fillValue += 0.1f;
        materialPropertyBlock.SetFloat("_Fill", fillValue);
        renderer.SetPropertyBlock(materialPropertyBlock);
    }
}
