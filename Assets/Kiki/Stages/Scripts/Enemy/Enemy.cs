using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public GameObject footprintPrefab;   // ��ӡ�� Prefab
    public float moveSpeed = 5f;         // ���˵��ƶ��ٶ�
    public float footprintLifetime = 10f; // ��ӡ����������
    public float slowdownFactor = 0.5f;   // ��������
    public float fixedYPosition = 0f;    // �̶��� Y ��λ��

    private Rigidbody rb; // �ο� Rigidbody ���

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // ��ȡ Rigidbody ���
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on the Enemy object.");
            enabled = false;
            return;
        }

        Debug.Log("Enemy script initialized. Starting movement coroutine...");
        StartCoroutine(MoveRandomly());
    }

    private IEnumerator MoveRandomly()
    {
        Debug.Log("MoveRandomly coroutine started");

        while (true)
        {
            Vector3 targetPosition = GetRandomPosition();
            targetPosition.y = fixedYPosition; // ȷ��Ŀ��λ�õ� Y ��ֵ�̶�
            Debug.Log("Moving towards target position: " + targetPosition);

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                Vector3 direction = (targetPosition - transform.position).normalized;
                direction.y = 0; // ȷ�������� Y ���ϲ����ƶ�

                // ���� Rigidbody ���ٶ�
                rb.velocity = direction * moveSpeed;

                // ������ӡ
                CreateFootprint();

                // �ȴ�һ��ʱ���ټ����ƶ�
                yield return new WaitForSeconds(0.5f); // ���Ը�����Ҫ����ʱ��
            }

            // ����Ŀ��λ�ú�ֹͣ�ƶ�
            rb.velocity = Vector3.zero;
            Debug.Log("Reached target position. Waiting before next move.");
            yield return new WaitForSeconds(Random.Range(2, 5)); // ����ȴ�ʱ��
        }
    }

    private Vector3 GetRandomPosition()
    {
        // ����һ�������λ�ã����� Y ��̶���ָ��λ��
        float x = Random.Range(-10f, 10f);
        float z = Random.Range(-10f, 10f);
        Vector3 randomPosition = new Vector3(x, fixedYPosition, z);
        Debug.Log("Generated random position: " + randomPosition);
        return randomPosition;
    }

    private void CreateFootprint()
    {
        if (footprintPrefab != null)
        {
            // ʵ������ӡ��������������
            GameObject footprint = Instantiate(footprintPrefab, transform.position, Quaternion.identity);
            Destroy(footprint, footprintLifetime);
            Debug.Log("Footprint created at position: " + transform.position);
        }
        else
        {
            Debug.LogWarning("Footprint prefab is not assigned!");
        }
    }
}
