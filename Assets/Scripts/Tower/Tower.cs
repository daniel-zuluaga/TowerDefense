using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public enum TowerTargetPriority
    {
        First,
        Close,
        Strong
    }

    [Header("Info")]
    public float range;
    private List<Enemy> curEnemiesInRange = new List<Enemy>();
    private Enemy curEnemy;

    public TowerTargetPriority targetPriority;
    public bool rotateTowardsTarget;

    [Header("Attacking")]
    public float attackRate;
    private float lastAttackTime;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPos;

    public int projectileDamage;
    public float projectileSpeed;

    void Update()
    {
        if(Time.time - lastAttackTime > attackRate)
        {
            lastAttackTime = Time.time;
            curEnemy = GetEnemy();

            if (curEnemy != null)
                Attack();
        }
    }

    Enemy GetEnemy()
    {
        curEnemiesInRange.RemoveAll(x => x == null);

        if (curEnemiesInRange.Count == 0)
            return null;

        if (curEnemiesInRange.Count == 1)
            return curEnemiesInRange[0];

        switch (targetPriority)
        {
            case TowerTargetPriority.First:
            {
                return curEnemiesInRange[0];
            }

            case TowerTargetPriority.Close:
            {
                Enemy closest = null;
                float dist = 99;

                for(int x = 0; x < curEnemiesInRange.Count; x++)
                {
                    float d = (transform.position - curEnemiesInRange[x].transform.position).sqrMagnitude;

                    if(d < dist)
                    {
                        closest = curEnemiesInRange[x];
                        dist = d;
                    }
                }

                return closest;

            }

            case TowerTargetPriority.Strong:
            {
                Enemy Strongest = null;
                int strongestHealth = 0;

                foreach(Enemy enemy in curEnemiesInRange)
                {
                    if(enemy.health > strongestHealth)
                    {
                        Strongest = enemy;
                        strongestHealth = enemy.health;
                    }
                }

                return Strongest;
            }

        }

        return null;
    }

    // attacks the curEnemy
    void Attack()
    {
        if(rotateTowardsTarget)
        {
            transform.LookAt(curEnemy.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        GameObject proj = Instantiate(projectilePrefab, projectileSpawnPos.position, Quaternion.identity);
        proj.GetComponent<Projectiles>().Initialized(curEnemy, projectileDamage, projectileSpeed);
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            curEnemiesInRange.Add(other.GetComponent<Enemy>());
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            curEnemiesInRange.Remove(other.GetComponent<Enemy>());
        }
    }

}
