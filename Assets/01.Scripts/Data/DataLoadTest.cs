using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class DataLoadTest : MonoBehaviour
{
    void Start()
    {
        UnityGoogleSheet.LoadFromGoogle<int, GunData.Data>((list, map) => {
            list.ForEach(x => {
                Debug.Log(x.ammoPerShot);
            });
        }, true);

    }
}
