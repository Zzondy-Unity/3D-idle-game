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
        itemData = null;
        itemCount = 0;
        countText.text = string.Empty;
        icon.sprite = null;
    }

    public void Set()
    {
        if(itemData != null)
        {
            if (itemCount == 0)
                Clear();
            else
            {
                icon.gameObject.SetActive(true);
                icon.sprite = itemData.icon;
                countText.text = itemCount == 0 ? string.Empty : itemCount.ToString();
                outline.enabled = equipped;
            }
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
