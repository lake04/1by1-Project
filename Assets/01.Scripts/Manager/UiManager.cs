using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [SerializeField] private Slider hpSlider;
    [SerializeField] private Text hpText;
    [SerializeField] private Slider bulletSlider;
    [SerializeField] private Text bulletText;
    [SerializeField] private Canvas inventroy;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        hpSlider.value = Player.Instance.curHp / Player.Instance.maxHp;
        hpText.text = $"{Player.Instance.curHp} / {Player.Instance.maxHp}";

        bulletSlider.value = (float)GunManager.Instance.curBullet / GunManager.Instance.maxBullet;
        bulletText.text = $"{GunManager.Instance.curBullet} / {GunManager.Instance.maxBullet}";
    }
}
