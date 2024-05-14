using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<WeaponController> weaponSlots = new List<WeaponController>(6);
    public int[] weaponLevels = new int[6];
    public List<Image> weaponUISlots = new List<Image>(6);
    public List<PassiveItem> passiveItemSlots = new List<PassiveItem>(6);
    public int[] passiveItemsLevels = new int[6];
    public List<Image> passiveItemUISlots = new List<Image>(6);
    [System.Serializable]
    public class WeaponUpgrade
    {
        public GameObject initialWeapon;
        public WeaponScriptableObject weapondData;
    }
    [System.Serializable]
    public class PassiveItemUpgrade
    {
        public GameObject initialPassiveItem;
        public PassiveItemScriptableObject passiveItemData;
    }
    [System.Serializable]
    public class UpgradeUI
    {
        public Text upgradeNameDisplay;
        public Text upgradeDescriptionDisplay;
        public Image upgradeIcon;
        public Button upgradeButton;
    }

    public List<WeaponUpgrade> weaponUpgradeOptions = new List<WeaponUpgrade>();
    public List<PassiveItemUpgrade> passiveItemUpgradesOptions = new List<PassiveItemUpgrade>();
    public List<UpgradeUI> upgradeUIOptions = new List<UpgradeUI>();

    PlayerStats player;
    void Start()
    {
        player = GetComponent<PlayerStats>();
    }
    public void AddWeapon(int slotIndex, WeaponController weapon) //Añade un arma a un arreglo
    {
        weaponSlots[slotIndex] = weapon;
        weaponLevels[slotIndex] = weapon.weaponData.Level;
        weaponUISlots[slotIndex].enabled = true;
        weaponUISlots[slotIndex].sprite = weapon.weaponData.Icon;

        if(GameManager.instance != null && GameManager.instance.choosingUpgrades)
        {
            GameManager.instance.EndLevelUp();
        }
    }

    public void AddPasssiveItem(int slotIndex, PassiveItem passiveitem) //Añade un objeto pasivo a un arreglo
    {
        passiveItemSlots[slotIndex] = passiveitem;
        passiveItemsLevels[slotIndex] = passiveitem.passiveItemData.Level;
        passiveItemUISlots[slotIndex].enabled = true;
        passiveItemUISlots[slotIndex].sprite = passiveitem.passiveItemData.Icon;

        if(GameManager.instance != null && GameManager.instance.choosingUpgrades)
        {
            GameManager.instance.EndLevelUp();
        }
    }

    public void LevelUpWeapon(int slotIndex)
    {
        if(weaponSlots.Count > slotIndex)
        {
            WeaponController weapon = weaponSlots[slotIndex];
            if(!weapon.weaponData.NextLevelPrefab)
            {
                Debug.LogError("No hay siguiente nivel: " + weapon.name);
            }
            GameObject upgradedWeapon = Instantiate(weapon.weaponData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedWeapon.transform.SetParent(transform);
            AddWeapon(slotIndex, upgradedWeapon.GetComponent<WeaponController>());
            Destroy(weapon.gameObject);
            weaponLevels[slotIndex] = upgradedWeapon.GetComponent<WeaponController>().weaponData.Level;

            if(GameManager.instance != null && GameManager.instance.choosingUpgrades)
            {
                GameManager.instance.EndLevelUp();
            }
        }
    }

    public void LevelUpPassiveItem(int slotIndex)
    {
         if(passiveItemSlots.Count > slotIndex)
        {
            PassiveItem passiveItem = passiveItemSlots[slotIndex];
            if(!passiveItem.passiveItemData.NextLevelPrefab)
            {
                Debug.LogError("No hay siguiente nivel: " + passiveItem.name);
            }
            GameObject upgradedPassiveItem = Instantiate(passiveItem.passiveItemData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedPassiveItem.transform.SetParent(transform);
            AddPasssiveItem(slotIndex, upgradedPassiveItem.GetComponent<PassiveItem>());
            Destroy(passiveItem.gameObject);
            passiveItemsLevels[slotIndex] = upgradedPassiveItem.GetComponent<PassiveItem>().passiveItemData.Level;

            if(GameManager.instance != null && GameManager.instance.choosingUpgrades)
            {
                GameManager.instance.EndLevelUp();
            }
        }
    }

    void ApplyUpgradeOptions()
    {
        foreach (var upgradeOptions in upgradeUIOptions)
        {
            int upgradeType = Random.Range(1, 3);
            if (upgradeType == 1)
            {
                WeaponUpgrade chosenweaponUpgrade = weaponUpgradeOptions[Random.Range(0, weaponUpgradeOptions.Count)];

                if(chosenweaponUpgrade != null)
                {
                    bool newWeapon = false;
                    for (int i = 0; i < weaponSlots.Count; i++)
                    {
                        if(weaponSlots[i] != null && weaponSlots[i].weaponData == chosenweaponUpgrade.weapondData)
                        {
                            newWeapon = false;
                            if (!newWeapon)
                            {
                                upgradeOptions.upgradeButton.onClick.AddListener(() => LevelUpWeapon(i));
                                upgradeOptions.upgradeDescriptionDisplay.text = chosenweaponUpgrade.weapondData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Description;
                                upgradeOptions.upgradeNameDisplay.text = chosenweaponUpgrade.weapondData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Name;
                            }
                            break;
                        }
                        else
                        {
                            newWeapon = true;
                        }
                    }
                    if (newWeapon)
                    {
                        upgradeOptions.upgradeButton.onClick.AddListener(() => player.SpawnWeapon(chosenweaponUpgrade.initialWeapon));
                        upgradeOptions.upgradeDescriptionDisplay.text = chosenweaponUpgrade.weapondData.Description;
                        upgradeOptions.upgradeNameDisplay.text = chosenweaponUpgrade.weapondData.Name;
                    }

                    upgradeOptions.upgradeIcon.sprite = chosenweaponUpgrade.weapondData.Icon;
                }
            }
            else if (upgradeType == 2)
            {
                PassiveItemUpgrade chosenPassiveItemUpgrade = passiveItemUpgradesOptions[Random.Range(0, passiveItemUpgradesOptions.Count)];

                if (chosenPassiveItemUpgrade != null)
                {
                    bool newPassiveItem = false;
                    for (int i = 0; i < passiveItemSlots.Count; i++)
                    {
                        if (passiveItemSlots[i] != null && passiveItemSlots[i].passiveItemData == chosenPassiveItemUpgrade.passiveItemData)
                        {
                            newPassiveItem = false;

                            if(!newPassiveItem)
                            {
                                upgradeOptions.upgradeButton.onClick.AddListener(() => LevelUpPassiveItem(i));
                                upgradeOptions.upgradeDescriptionDisplay.text = chosenPassiveItemUpgrade.passiveItemData.NextLevelPrefab.GetComponent<PassiveItem>().passiveItemData.Description;
                                upgradeOptions.upgradeNameDisplay.text = chosenPassiveItemUpgrade.passiveItemData.NextLevelPrefab.GetComponent<PassiveItem>().passiveItemData.Name;
                            }
                            break;
                        }
                        else
                        {
                            newPassiveItem = true;
                        }
                    }
                    if (newPassiveItem)
                    {
                        upgradeOptions.upgradeButton.onClick.AddListener(() => player.SpawnPassiveItem(chosenPassiveItemUpgrade.initialPassiveItem));
                        upgradeOptions.upgradeDescriptionDisplay.text = chosenPassiveItemUpgrade.passiveItemData.Description;
                        upgradeOptions.upgradeNameDisplay.text = chosenPassiveItemUpgrade.passiveItemData.Name;
                    }

                    upgradeOptions.upgradeIcon.sprite = chosenPassiveItemUpgrade.passiveItemData.Icon;
                }
            }
        }
    }

    void RemoveUpgradesOptions()
    {
        foreach (var upgradeOption in upgradeUIOptions)
        {
            upgradeOption.upgradeButton.onClick.RemoveAllListeners();
        }
    }

    public void RemoveAndApplyUpgrades()
    {
        RemoveUpgradesOptions();
        ApplyUpgradeOptions();
    }
}
