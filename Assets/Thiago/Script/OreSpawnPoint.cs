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

    public OreChance[] ores;

    public float respawnTime = 10f;

    private GameObject currentOre;

    void Start()
    {
        SpawnRandomOre();
    }

    void SpawnRandomOre()
    {
        int random = Random.Range(0, 100);

        int total = 0;

        foreach (OreChance ore in ores)
        {
            total += ore.chance;

            if (random < total)
            {
                currentOre = ore.oreObject;

                currentOre.SetActive(true);

                currentOre.transform.position = transform.position;
                currentOre.transform.rotation = transform.rotation;

                Ore oreScript = currentOre.GetComponent<Ore>();

                if (oreScript != null)
                {
                    oreScript.spawnPoint = this;
                }

                return;
            }
        }
    }

    public void OreDestroyed()
    {
        if (currentOre != null)
        {
            currentOre.SetActive(false);
        }

        Invoke(nameof(SpawnRandomOre), respawnTime);
    }
}