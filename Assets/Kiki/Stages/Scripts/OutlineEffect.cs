using UnityEngine;

public class OutlineEffect : MonoBehaviour
{
    public Material outlineMaterial;  // �ű������õ���������
    private Material originalMaterial;  // ����ԭʼ�Ĳ���
    private Renderer rend;  // �����Renderer���

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalMaterial = rend.material;  // ��ȡ�����ԭʼ����
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ȷ��Tagƥ��
        {
            rend.material = outlineMaterial;  // ������������
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // ȷ��Tagƥ��
        {
            rend.material = originalMaterial;  // �ָ�ԭʼ����
        }
    }
}
