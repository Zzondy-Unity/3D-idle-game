using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countText;
    private Outline outline;
    public ItemSO itemData;
    private Button btn;
    private Image icon; //Image? Sprite?

    public Inventory inventory;

    public int itemCount;
    public int slotIndex;
    public bool equipped;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        icon = GetComponent<Image>();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClickSlot);
    }

    public void outlineEnable()
    {
        outline.enabled = true;
    }
    public void outlineDisable()
    {
        outline.enabled = false;
    }

    public void Clear()
    {
        outlineDisable();
        itemData = null;
        itemCount = 0;
        countText.text = string.Empty;
        icon.gameObject.SetActive(false);
    }

    public void Set()
    {
        if(itemData != null)
        {
            icon.gameObject.SetActive (true);
            icon.sprite = itemData.icon;
            countText.text = itemCount == 0? string.Empty : itemCount.ToString();
            outline.enabled = equipped;
        }
        else
        {
            Clear();
        }
    }

    public void OnClickSlot()
    {
        inventory.SelectItemSlot(slotIndex);
    }

}
