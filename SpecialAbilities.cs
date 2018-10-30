using UnityEngine;
using UnityEngine.UI;

namespace WarQuest.Characters
{
    public class SpecialAbilities : MonoBehaviour
    {
        [SerializeField] RawImage energyBar;
        [SerializeField] float maxEnergyPoints = 100f;
        [SerializeField] float regenPointsPerSecond = 1f;
        [SerializeField] AbilityConfig[] abilities;
        [SerializeField] AudioClip outOfEnergy;
        [SerializeField] Text energyTextAmount = null;

        float currentEnergyPoints = 0f;
        AudioSource audioSource;

        float energyAsPercent
        {
          get { return CurrentEnergyPoints / MaxEnergyPoints; }
        }

        public float MaxEnergyPoints
        {
            get{ return maxEnergyPoints; }
            set{ maxEnergyPoints = value;
                UpdateEnergyBar();
            }
        }

        public float CurrentEnergyPoints
        {
            get { return currentEnergyPoints; }
            set { currentEnergyPoints = value;
                UpdateEnergyBar();
            }
        }

        void Start()
        {
            CurrentEnergyPoints = MaxEnergyPoints;
            audioSource = GetComponent<AudioSource>();
            AttachInitialAbilities();
            UpdateEnergyBar();
        }

        void Update()
        {
           if(CurrentEnergyPoints < MaxEnergyPoints)
            {
                RegenerateEnergy();
            }
            if (CurrentEnergyPoints > MaxEnergyPoints)
            {
                CurrentEnergyPoints = MaxEnergyPoints;
            }
        }

        public void SetEergyBarText()
        {
            if (GetComponent<PlayerControl>())
            {
                var EnergyB = GameObject.Find("Environment/Game Canvas/EnergyBar");
                var EnergyT = GameObject.Find("Environment/Game Canvas/EnergyText");
                energyBar = EnergyB.GetComponent<RawImage>();
                energyTextAmount = EnergyT.GetComponent<Text>();
            }
        }
      
        public void ConsumeEnergy(float amount)
        {
            float newEnergyPoints = MaxEnergyPoints - amount;
            CurrentEnergyPoints = Mathf.Clamp(newEnergyPoints, 0, MaxEnergyPoints);
            UpdateEnergyBar();
        }

        public void RegenerateEnergy()
        {
            if (!IsInvoking("UpdateEnergyBar") && CurrentEnergyPoints < MaxEnergyPoints)
            {
                var pointsToAdd = regenPointsPerSecond;
                CurrentEnergyPoints = Mathf.Clamp(CurrentEnergyPoints + pointsToAdd, 0, MaxEnergyPoints);
                Invoke("UpdateEnergyBar", 1f);
            }
            else if (CurrentEnergyPoints >= MaxEnergyPoints)
            {
                CancelInvoke();
            }

           
        }

        public int GetNumberOfAbilities()
        {
            return abilities.Length;
        }

        void UpdateEnergyBar()
        {
            if (energyBar)
            {
                float xValue = -(energyAsPercent / 2f) - 0.5f;
                energyBar.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
                energyTextAmount.text = (CurrentEnergyPoints.ToString() + "/" + MaxEnergyPoints.ToString());
            }
        }

        public void AttachInitialAbilities()
        {
            for (int abilityIndex = 0; abilityIndex < abilities.Length; abilityIndex++)
            {
                abilities[abilityIndex].AttachAbilityTo(gameObject);
            }
        }

        public void AttemptSpecialAbility(int abilityIndex, GameObject target = null)
        {
            //   var energyComponent = GetComponent<SpecialAbilities>();
            if (gameObject.GetComponent<HealthSystem>().healthAsPercentage > Mathf.Epsilon)
            {
                var energyCost = abilities[abilityIndex].GetEnergyCost();
                if (energyCost <= CurrentEnergyPoints)
                {
                    ConsumeEnergy(energyCost);
                    abilities[abilityIndex].Use(target);
                }
                else
                {
                    if (!audioSource.isPlaying)
                    {
                        audioSource.PlayOneShot(outOfEnergy);
                    }
                }
            }
        }

    }
}
