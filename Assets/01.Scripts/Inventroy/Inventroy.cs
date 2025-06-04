using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventroy : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public List<GunData> gunDatas;
    [SerializeField] private Image gunImages;
    [SerializeField] private GameObject slots;
    private int curIndex = 0;

    private Item selectedItem;
    public GameObject[] partSlotButtons;
    [SerializeField] private GameObject slotsPrefbs;
    [SerializeField] private Transform[] slotsPos;
    [SerializeField] private GameObject inventroy;

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
        if (curIndex> GunManager.Instance.slots.Count - 2)
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
        gunDatas = GunManager.Instance.GetEquippedGunList();
        if (curIndex >= gunDatas.Count) return;

        GunData currentGun = gunDatas[curIndex];

        // 총기 이미지 설정
        gunImages.sprite = GunDatabase.Instance.sprites[currentGun.gunID];

        // 기존 슬롯 정리
        foreach (Transform slot in slotsPos)
        {
            foreach (Transform child in slot)
            {
                Destroy(child.gameObject);
            }
        }

        // 파츠 슬롯 UI 생성
        for (int i = 0; i < currentGun.parts; i++)
        {
            Instantiate(slotsPrefbs, slotsPos[i]);
        }

        // 슬롯 UI에 장착된 부품 반영
        for (int i = 0; i < currentGun.equippedParts.Count; i++)
        {
            Transform slot = slotsPos[i];
         
        }
    }

    //private void UpdateUI()
    //{
    //    Image currentSlot = GunManager.Instance.slots[curIndex];
    //    gunImages.sprite = currentSlot.GetComponentInChildren<Image>().sprite;



}
   

  



