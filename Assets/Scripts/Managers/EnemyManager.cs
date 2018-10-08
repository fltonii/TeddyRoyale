using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    float timer = 0f;
    void FixedUpdate() {
        timer += Time.fixedDeltaTime;

        if(timer >= spawnTime){
            Spawn();
            timer = 0f;
        }

        if(ScoreManager.score % 100 == 0) {
            spawnTime -= 2.5f; 
        }
    }

    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }


}
