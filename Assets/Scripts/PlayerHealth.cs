using UnityEngine;
using Mirror;

public class PlayerHealth : NetworkBehaviour
{
    public float maxHealth = 100f;
    [SyncVar] public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (!isServer) return;
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            RpcRespawn();
        }
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            transform.position = new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));
        }
    }
}