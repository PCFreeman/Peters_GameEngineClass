using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCatalog : MonoBehaviour
{
    [System.Serializable]
    struct HeroInfo
    {
        public GameObject heroPrefab;
        public string heroName;
    }

    [SerializeField]
    private List<HeroInfo> heroList = new List<HeroInfo>();

    public static HeroCatalog Instance;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
    }
    public GameObject FindHeroByName(string name)
    {
        return heroList.Find(x => x.heroName == name).heroPrefab
;    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
