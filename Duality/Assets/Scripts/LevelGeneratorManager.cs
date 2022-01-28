using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGeneratorManager : MonoBehaviour
{
    public static LevelGeneratorManager Instance { get; set; }

    public List<LevelGenerator> generators = new List<LevelGenerator>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singeleton duplicated!");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }


        foreach (LevelGenerator levelGenerator in GetComponentsInChildren<LevelGenerator>())
        {
            if (!generators.Contains(levelGenerator))
                generators.Add(levelGenerator);
        }

        generators = generators.OrderByDescending(x => x.minHeight).ToList();
    }


    public LevelGenerator GetActiveLevelGenerator(float height)
    {
        foreach (LevelGenerator generator in generators)
        {
            if(generator.minHeight <= height)
                return generator;
        }
        throw new System.Exception("NO LEVEL GENERATORS!!");
    }
}
