using GDS;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Slot[] slots;
    [SerializeField] private TextMeshProUGUI itemNameText;

    private ItemSO selectedItemData;
    private int selectedItemIndex;

    private int curEquipIndex;


    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        CharacterManager.Instance.Player.AddItem += GetItem;
    }

    public void Init()
    {
        for(int i  = 0; i < slots.Length; i++)
        {
            slots[i].slotIndex = i;
            slots[i].Clear();
        }
    }

    private void SelectedReset()
    {
        selectedItemData = null;
        selectedItemIndex = -1;
    }

    public void ToggleInventory()
    {
        SelectedReset();
        bool isOpen = IsOpen();
        gameObject.SetActive(!isOpen);
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = isOpen ? 0 : 1;
    }

    private bool IsOpen()
    {
        return gameObject.activeInHierarchy;
    }

    private void InventoryUpdate()
    {
        for(int i = 0;i < slots.Length; i++)
        {
            slots[i].Set();
        }
    }

    public void SelectItemSlot(int index)
    {
        itemNameText.text = string.Empty;
        if (slots[index].itemData == null) return;

        selectedItemIndex = index;
        selectedItemData = slots[index].itemData;
        itemNameText.text = slots[index].itemData.itemName;
    }

    public void GetItem(ItemSO data)
    {
        Slot slot = GetItemSlot(data);
        if(slot == null)
        {
            Slot emptySlot = GetEmptySlot();    //빈 슬롯이 없는경우는 일단 상정하지 않겠음
            emptySlot.itemData = data;
            emptySlot.Set();
        }
        else
        {
            slot.itemCount++;
            slot.Set();
        }
        InventoryUpdate();
    }

    private Slot GetEmptySlot()
    {
        for(int i =0;  i < slots.Length; i++)
        {
            if(slots[i].itemData == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    private Slot GetItemSlot(ItemSO data)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if( slots[i].itemData == data)
            {
                return slots[i];
            }
        }
        return null;
    }

    public void UseBtn()
    {
        if (slots[selectedItemIndex].itemData is IUsable usable)
        {
            usable.Use();   //추후 포션 의외의 다른 사용아이템이 생길때를 대비해 인터페이스 사용
        }
    }

    public void EquipBtn()
    {
        if (slots[selectedItemIndex].itemData is WeaponSO weaponData)
        {

        }
    }

    //아이템 획득
    //아이템 사용
    //아이템 장착


}
