using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletSpriteSet
{
    public GunKind gunKind;
    public Sprite[] elementSprites;
}

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance { get; private set; }
    
    public List<BulletSpriteSet> bulletSpriteSets;

    private Dictionary<GunKind, Sprite[]> bulletSpriteDict;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeDictionary();
        }
        
    }

    private void InitializeDictionary()
    {
        bulletSpriteDict = new Dictionary<GunKind, Sprite[]>();
        foreach (var set in bulletSpriteSets)
        {
            bulletSpriteDict[set.gunKind] = set.elementSprites;
        }
    }

    public Sprite GetBulletSprite(GunKind kind, Element element)
    {
        if (bulletSpriteDict.TryGetValue(kind, out var sprites))
        {
            int elementIndex = (int)element;
            if (elementIndex >= 0 && elementIndex < sprites.Length)
            {
                return sprites[elementIndex];
            }
            else
            {
                Debug.Log("없음");
            }
        }
        else
        {
            Debug.Log("총 종류 없음");
        }

        return null;
    }
}
