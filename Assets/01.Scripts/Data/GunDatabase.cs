using System.Collections.Generic;
using UnityEngine;

public class GunDatabase : MonoBehaviour
{
    public static GunDatabase Instance { get; private set; }
    public List<Sprite> sprites = new List<Sprite>();

    public List<GunData> allGunData = new();
    private Dictionary<int, GunData> gunById = new();
    public AnimationClip[] gunAnim;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
 

    public void Initialize(List<GunData> dataList)
    {
        allGunData = dataList;
        gunById.Clear();

        foreach (var data in allGunData)
        {
            gunById[data.gunID] = data;
        }

        Debug.Log($"총기 데이터 {allGunData.Count}개 로드됨");
    }

    public GunData GetGunByID(int id)
    {
        return gunById.TryGetValue(id, out var gun) ? gun : null;
    }

    public List<GunData> GetAllGuns()
    {
        return new List<GunData>(allGunData);
    }

    public List<GunData> GetRandomGuns(int count)
    {
        List<GunData> result = new();
        List<GunData> copy = new(allGunData);

        for (int i = 0; i < count; i++)
        {
            if (copy.Count == 0) break;
            int rand = Random.Range(0, copy.Count);
            result.Add(copy[rand]);
            copy.RemoveAt(rand);
        }

        return result;
    }
}
