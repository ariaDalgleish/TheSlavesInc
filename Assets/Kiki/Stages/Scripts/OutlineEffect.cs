using UnityEngine;

public class OutlineEffect : MonoBehaviour
{
    public Material outlineMaterial;  // 脚本中引用的轮廓材质
    private Material originalMaterial;  // 物体原始的材质
    private Renderer rend;  // 物体的Renderer组件

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalMaterial = rend.material;  // 获取物体的原始材质
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 确保Tag匹配
        {
            rend.material = outlineMaterial;  // 设置轮廓材质
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 确保Tag匹配
        {
            rend.material = originalMaterial;  // 恢复原始材质
        }
    }
}
