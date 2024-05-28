using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
   CharacterScriptableObject characterData;

   //Los estats in game de cada jugador
   float currentHealth;
   float currentRecovery;
   float currentMoveSpeed;
   float currentMight;
   float currentProjectileSpeed;
   float currentMagnet;

   //Las regiones son para poder acoplar partes del código
   #region Current Stats Properties

   float CurrentHealth
   {
      get {return currentHealth;}
      set
      {
         if(currentHealth != value)
         {
            if(GameManager.instance != null)
            {
               GameManager.instance.currentHealthDisplay.text = "Vida: " + currentHealth;
            }
            currentHealth = value;
         }
      }
   }
      public float CurrentRecovery
   {
      get {return currentRecovery;}
      set
      {
         if(currentRecovery != value)
         {
            if(GameManager.instance != null)
            {
               GameManager.instance.currentrecoveryDisplay.text = "Recuperación: " + currentRecovery;
            }
            currentRecovery = value;
         }
      }
   }
      public float CurrentMoveSpeed
   {
      get {return currentMoveSpeed;}
      set
      {
         if(currentMoveSpeed != value)
         {
            if(GameManager.instance != null)
            {
               GameManager.instance.currentMoveSpeedDisplay.text = "Velocidad de movimiento: " + currentMoveSpeed;
            }
            currentMoveSpeed = value;
         }
      }
   }
      public float CurrentMight
   {
      get {return currentMight;}
      set
      {
         if(currentMight != value)
         {
            if(GameManager.instance != null)
            {
               GameManager.instance.currentMightDisplay.text = "Poder: " + currentMight;
            }
            currentMight = value;
         }
      }
   }
      public float CurrentProjectileSpeed
   {
      get {return currentProjectileSpeed;}
      set
      {
         if(currentProjectileSpeed != value)
         {
            if(GameManager.instance != null)
            {
               GameManager.instance.currentProjectileSpeedDisplay.text = "Velocidad de Proyectil: " + currentProjectileSpeed;
            }
            currentProjectileSpeed = value;
         }
      }
   }
      public float CurrentMagnet
   {
      get {return currentMagnet;}
      set
      {
         if(currentMagnet != value)
         {
            if(GameManager.instance != null)
            {
               GameManager.instance.currentMagnetDisplay.text = "Imán: " + currentMagnet;
            }
            currentMagnet = value;
         }
      }
   }

   #endregion


   //Experiencia del jugador
   [Header("Experience Level")]
   public int experience = 0; //La experiencia del jugador
   public int level = 0; //El nivel del jugador
   public int experienceCap; //El máximo de experiencia del jugador
   
   [System.Serializable] //Para que sea visible en el inspector de Unity, sin eso no será visible

   public class LevelRange
   {
    public int startLevel;
    public int endLevel;
    public int experienceCapIncrease;
   }

   //Para darle los frames de invencibilidad a el jugador
   [Header("I-Frames")]
   public float invincibilityDuration;
   float invincibilityTimer;
   bool isInvincible;

   public List<LevelRange> levelRanges;
   InventoryManager inventory;
   public int weaponIndex;
   public int passiveItemIndex;

   [Header("UI")]
   public Image healthBar;
   public Image expBar;
   public Text leveltext;
   //public GameObject secondWeaponTest;
   //public GameObject firstPassiveItemTest, secondPassiveItemTest;
   public AudioClip hitmarket;

   void Awake()
   {
      characterData = CharacterSelector.GetData();
      CharacterSelector.instance.DestroySingleton();

      inventory = GetComponent<InventoryManager>();

      CurrentHealth = characterData.MaxHealth;
      CurrentRecovery = characterData.Recovery;
      CurrentMoveSpeed = characterData.MoveSpeed;
      CurrentMight = characterData.Might;
      CurrentProjectileSpeed = characterData.ProjectileSpeed;
      CurrentMagnet = characterData.Magnet;

      //Spawnear el arma principal
      SpawnWeapon(characterData.Startingweapon);
      //SpawnWeapon(secondWeaponTest);
      //SpawnPassiveItem(firstPassiveItemTest);
      //SpawnPassiveItem(secondPassiveItemTest);
   }

   void Start()
   {
      experienceCap = levelRanges[0].experienceCapIncrease; //Porque en cuanto empieza el juego sube de nivel el jugador
      GameManager.instance.currentHealthDisplay.text = "Vida: " + currentHealth;
      GameManager.instance.currentrecoveryDisplay.text = "Recuperación: " + currentRecovery;
      GameManager.instance.currentMoveSpeedDisplay.text = "Velocidad de movimiento: " + currentMoveSpeed;
      GameManager.instance.currentMightDisplay.text = "Poder: " + currentMight;
      GameManager.instance.currentProjectileSpeedDisplay.text = "Velocidad de Proyectil: " + currentProjectileSpeed;
      GameManager.instance.currentMagnetDisplay.text = "Imán: " + currentMagnet;

      GameManager.instance.AssignChosenCharacterUI(characterData);

      UpdateHealthBar();
      UpdateExpBar();
      UpdateLevelText();
   }

   void Update() 
   {
      if(invincibilityTimer > 0)
      {
         invincibilityTimer -= Time.deltaTime;
      }
      //Como el contador llegó a 0, pasa directamente a esta
      else if (isInvincible)
      {
         isInvincible = false;
      }

      Recover();
   }

   public void IncreaseExperience(int amount)
   {
      experience += amount;
      LevelUpChecker();
      UpdateExpBar();
   }

   void LevelUpChecker()
   {
      if (experience >= experienceCap)
      {
         level++;
         experience -= experienceCap;
         int experienceCapIncrease   = 0;
         foreach (LevelRange range in levelRanges)
         {
            if(level >= range.startLevel && level <= range.endLevel)
            {
               experienceCapIncrease = range.experienceCapIncrease;
               break;
            }  
         }
         experienceCap += experienceCapIncrease;

         UpdateLevelText();

         GameManager.instance.StartLeveUp();
      }
   }

   void UpdateExpBar()
   {
      expBar.fillAmount = (float)experience / experienceCap;
   }

   void UpdateLevelText()
   {
      leveltext.text = "NIVEL " + level.ToString();
   }

   public void TakeDamage(float dmg)
   {
      //Los segundos de invencibilidad
      if(!isInvincible)
      {
         SoundController.Instance.PlaySound(hitmarket);
         CurrentHealth -= dmg;
         invincibilityTimer = invincibilityDuration;
         isInvincible = true;
         if(CurrentHealth <= 0)
         {
            Kill();
         }

         UpdateHealthBar();
      }
   }

   void UpdateHealthBar()
   {
      healthBar.fillAmount = currentHealth / characterData.MaxHealth; //Se llama esta función que hicimos en start y take damage
   }

   public void Kill()
   {
      if(!GameManager.instance.isGameOver)
      {
         GameManager.instance.AssignLevelReachedUI(level);
         GameManager.instance.AssignChosenWeaponsAndPassiveItemsUI(inventory.weaponUISlots, inventory.passiveItemUISlots);
         GameManager.instance.GameOver();
      }
   }

   public void RestoreHealth(float amount)
   {
      //Solo so hay vida que sanar la poción lo hace
      if(CurrentHealth < characterData.MaxHealth)
      {
         CurrentHealth += amount;
         //Si sobrepasa, se regula volviendo a su máxima vida
         if(CurrentHealth > characterData.MaxHealth)
         {
            CurrentHealth = characterData.MaxHealth;
         }
      }
   }

   void Recover()
   {
      if(CurrentHealth < characterData.MaxHealth)
      {
         CurrentHealth += CurrentRecovery * Time.deltaTime;
         if(CurrentHealth > characterData.MaxHealth)
         {
            CurrentHealth = characterData.MaxHealth;
         }
      }
   }

   public void SpawnWeapon(GameObject weapon)
   {
      //Si el inventario está lleno, se returna la función
      if(weaponIndex >= inventory.weaponSlots.Count - 1)
      {
         Debug.LogError("El inventario está lleno");
         return;
      }
      //Spawnea el arma inicial
      GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
      spawnedWeapon.transform.SetParent(transform);
      inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>()); //Añadimos el arma a su inventario, así, el arma inicial ocupa el 1er espacio

      weaponIndex++;
   }

   public void SpawnPassiveItem(GameObject passiveItem)
   {
      //Si el inventario está lleno, se returna la función
      if(passiveItemIndex >= inventory.passiveItemSlots.Count - 1)
      {
         Debug.LogError("El inventario está lleno");
         return;
      }
      //Spawnea el objeto pasivo inicial
      GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
      spawnedPassiveItem.transform.SetParent(transform);
      inventory.AddPasssiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>()); //Añadimos el arma a su inventario, así, el arma inicial ocupa el 1er espacio

      passiveItemIndex++;
   }
}
