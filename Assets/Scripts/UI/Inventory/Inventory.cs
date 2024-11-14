using GDS;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Slot[] slots;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private GameObject BG;

    [SerializeField] private Button UseButton;
    [SerializeField] private Button EquipButton;

    private int selectedItemIndex;

    private int curEquipIndex;



    private void Start()
    {
        Init();
        CharacterManager.Instance.Player.AddItem += GetItem;
        CharacterManager.Instance.Player.Input.playerActions.Inventory.started += ToggleInventory;

        UseButton.onClick.AddListener(UseBtn);
        EquipButton.onClick.AddListener(EquipBtn);
    }

    public void Init()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotIndex = i;
            slots[i].inventory = this;
            slots[i].Clear();
        }
        BG.SetActive(false);
    }

    private void SelectedReset()
    {
        selectedItemIndex = -1;
    }

    public void ToggleInventory(InputAction.CallbackContext context)
    {
        SelectedReset();
        InventoryUpdate();
        bool isOpen = !IsOpen();
        BG.SetActive(isOpen);
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = isOpen ? 0 : 1;
    }

    private bool IsOpen()
    {
        return BG.activeInHierarchy;
    }

    private void InventoryUpdate()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Set();
        }
    }

    public void SelectItemSlot(int index)
    {
        itemNameText.text = string.Empty;
        selectedItemIndex = index;
        if (slots[index].itemData == null) return;

        itemNameText.text = slots[index].itemData.itemName;
    }

    public void GetItem(ItemSO data)
    {
        Slot slot = GetItemSlot(data);
        if (slot == null)
        {
            Slot emptySlot = GetEmptySlot();    //빈 슬롯이 없는경우는 일단 상정하지 않겠음
            emptySlot.itemData = data;
            emptySlot.itemCount++;
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
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemData == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    private Slot GetItemSlot(ItemSO data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemData == data)
            {
                return slots[i];
            }
        }
        return null;
    }

    public void UseBtn()
    {
        if (slots[selectedItemIndex].itemData is PotionSO potionData)
        {
            switch (potionData.type)
            {
                case PotionType.HP:
                    CharacterManager.Instance.Player.HealthSystem.ChangeHealth(potionData.value);
                    break;
                case PotionType.Speed:
                case PotionType.Attack:
                    Debug.Log("미완성");
                    break;

            }
            RemoveItem();
        }
        InventoryUpdate();
        SelectItemSlot(selectedItemIndex);
    }

    private void RemoveItem()
    {
        slots[selectedItemIndex].itemCount--;
        slots[selectedItemIndex].Set();
    }

    public void EquipBtn()
    {
        if (slots[selectedItemIndex].itemData is WeaponSO weaponData)
        {
            ResetEquippedSlotOutline();

            EquipManager.Instance.EquipItem(weaponData);
            slots[selectedItemIndex].outlineEnable();
        }
    }

    private void ResetEquippedSlotOutline()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].equipped == true)
            {
                slots[i].outlineDisable();
            }
        }
    }
}
