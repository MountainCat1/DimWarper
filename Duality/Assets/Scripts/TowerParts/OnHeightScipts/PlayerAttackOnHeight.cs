using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace TowerParts.OnHeightScipts
{
    public class PlayerAttackOnHeight : OnHeightBehaviour
    {
        [SerializeField] private BossTrap bossTrap;
        [SerializeField] private PlayerAttack playerAttackPrefab;
        protected override void Action()
        {
            var go = Instantiate(
                playerAttackPrefab, 
                PlayerController.Instance.transform.position, 
                Quaternion.identity);

            var script = go.GetComponent<PlayerAttack>();
            
            script.StartAttack(bossTrap.SpawnedBoss.Kill, bossTrap.SpawnedBoss.gameObject);

            bossTrap.SpawnedBoss.Center();
        }
    }
}