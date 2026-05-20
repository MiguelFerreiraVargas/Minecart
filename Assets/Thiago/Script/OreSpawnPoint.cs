using UnityEngine;

public class OreSpawnPoint : MonoBehaviour
{
    [System.Serializable]
    public class OreChance
    {
        public GameObject oreObject;

        [Range(0, 100)]
        public int chance;
    }

    [Header("Minérios possíveis")]
    public OreChance[] ores;

    [Header("Tempo de respawn")]
    public float respawnTime = 10f;

    [Header("Multiplicador de escala")]
    public float oreScaleMultiplier = 400f;

    private GameObject currentOre;

    void Start()
    {
        SpawnOre();
    }

    void SpawnOre()
    {
        // evita duplicar minério
        if (currentOre != null)
            return;

        int random = Random.Range(0, 100);

        int total = 0;

        foreach (OreChance ore in ores)
        {
            if (ore.oreObject == null)
                continue;

            total += ore.chance;

            if (random < total)
            {
                currentOre = Instantiate(
                    ore.oreObject,
                    transform.position,
                    transform.rotation
                );

                currentOre.SetActive(true);

                currentOre.transform.localScale *= oreScaleMultiplier;

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