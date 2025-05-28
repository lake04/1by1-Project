using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventroy : MonoBehaviour
{
    public List<GunData> gunDatas;
    [SerializeField] private Image gunImages;
    private int curIndex = 0;
    [SerializeField] private GameObject slots;


    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
      

    }

    public void NextGun()
    {
        gunDatas = GunManager.Instance.GetEquippedGunList();
        if(curIndex> GunManager.Instance.slots.Count - 2)
        {
            return;
        }
       
        curIndex++;
        UpdateUI();
    }

    public void PrevGun()
    {
        gunDatas = GunManager.Instance.GetEquippedGunList();
        if (curIndex <= 0)
        {
            return;
        }
       
        curIndex--;
        UpdateUI();
    }

    private void UpdateUI()
    {
        gunImages.sprite = GunManager.Instance.slots[curIndex].GetComponentInChildren<Image>().sprite;
    }
}
