using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public GameObject footprintPrefab;   // 脚印的 Prefab
    public float moveSpeed = 5f;         // 敌人的移动速度
    public float footprintLifetime = 10f; // 脚印的生命周期
    public float slowdownFactor = 0.5f;   // 减速因子
    public float fixedYPosition = 0f;    // 固定的 Y 轴位置

    private Rigidbody rb; // 参考 Rigidbody 组件

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // 获取 Rigidbody 组件
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
            targetPosition.y = fixedYPosition; // 确保目标位置的 Y 轴值固定
            Debug.Log("Moving towards target position: " + targetPosition);

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                Vector3 direction = (targetPosition - transform.position).normalized;
                direction.y = 0; // 确保敌人在 Y 轴上不会移动

                // 设置 Rigidbody 的速度
                rb.velocity = direction * moveSpeed;

                // 创建脚印
                CreateFootprint();

                // 等待一段时间再继续移动
                yield return new WaitForSeconds(0.5f); // 可以根据需要调整时间
            }

            // 到达目标位置后停止移动
            rb.velocity = Vector3.zero;
            Debug.Log("Reached target position. Waiting before next move.");
            yield return new WaitForSeconds(Random.Range(2, 5)); // 随机等待时间
        }
    }

    private Vector3 GetRandomPosition()
    {
        // 生成一个随机的位置，并将 Y 轴固定在指定位置
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
            // 实例化脚印并设置生命周期
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
