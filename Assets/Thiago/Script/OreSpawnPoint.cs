using UnityEngine;

public class OreSpawnPoint : MonoBehaviour
{
    [System.Serializable]
    public class OreChance
    {
        public GameObject orePrefab;

        [Range(0, 100)]
        public int chance;
    }

    public OreChance[] ores;

    public float respawnTime = 10f;

    private GameObject currentOre;

    void Start()
    {
        SpawnOre();
    }

    void SpawnOre()
    {
        if (currentOre != null)
            return;

        int random = Random.Range(0, 100);

        int total = 0;

        foreach (OreChance ore in ores)
        {
            if (ore.orePrefab == null)
                continue;

            total += ore.chance;

            if (random < total)
            {
                currentOre = Instantiate(
                    ore.orePrefab,
                    transform.position,
                    transform.rotation
                );

                Ore oreScript = currentOre.GetComponent<Ore>();

                if (oreScript != null)
                {
                    oreScript.spawnPoint = this;
                }

                return;
            }
        }
    }

    public void RespawnOre()
    {
        currentOre = null;

        Invoke(nameof(SpawnOre), respawnTime);
    }
}