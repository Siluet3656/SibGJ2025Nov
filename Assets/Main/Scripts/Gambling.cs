using UnityEngine;

namespace Main.Scripts
{
    public class Gambling : MonoBehaviour
    {
        [SerializeField] private AudioSource _norm;
        [SerializeField] private AudioSource _rare;
        [SerializeField] private AudioSource _curse;
        
        private int _luck;

        private void Awake()
        {
            G.Gambling = this;

            _luck = 0;
        }

        public void Gamba()
        {
            int rand = Random.Range(0, 100);

            // Базовые пороги (без удачи)
            float junkChance = 23f;
            float normalChance = 70f;
            float rareChance = 6f;
            float cursedChance = 1f; // фиксированный

            // Применяем удачу (0–100): чем выше удача, тем меньше мусора и обычных предметов, больше редких
            float luckFactor = _luck / 100f;

            // junk уменьшается максимум до 10%
            junkChance = Mathf.Lerp(23f, 10f, luckFactor);

            // rare увеличивается максимум до 30%
            rareChance = Mathf.Lerp(6f, 25f, luckFactor);

            // normal пересчитывается, чтобы сумма была 100
            normalChance = 100f - (junkChance + rareChance + cursedChance);

            // Безопасность
            junkChance = Mathf.Clamp(junkChance, 0f, 100f);
            normalChance = Mathf.Clamp(normalChance, 0f, 100f);
            rareChance = Mathf.Clamp(rareChance, 0f, 100f);

            // Определяем редкость
            if (rand < junkChance)
            {
                Junk();
            }
            else if (rand < junkChance + normalChance)
            {
                Normal();
            }
            else if (rand < junkChance + normalChance + rareChance)
            {
                Rare();
            }
            else
            {
                if (luckFactor > 0.5f)
                    Cursed();
            }
            
            //Debug.Log($"Junk: {junkChance}, Normal: {normalChance}, Rare: {rareChance}, Cursed: {cursedChance}");
        }

        private void Cursed()
        {
            _curse.Play();
            
            int rand = Random.Range(0, 100); // Reclaimed Memory Chip 0-5 (+500% income buy doubles your debts ) // Synthetic Luck Token (Time slow/speed up each 10 sec) 6 - 100
            
            if (rand <= 5)
            {
                G.Passives.DoubleDebt();
            }
            else
            {
                UpdateLuckTocken(1);
            }

            string name = rand > 50 ? LocalizationManager.Instance.Translate("CurseName1") : LocalizationManager.Instance.Translate("CurseName2");
            
            G.MailSystem.ReceiveMail(
                name,
                LocalizationManager.Instance.Translate("Curse_Subj"),
                LocalizationManager.Instance.Translate("Curse_Message")
            );

        }

        private void Rare()
        {
            _rare.Play();
            
            int rand = Random.Range(0, 100); // Corporate Mantra Chip(Mult boost chance) 0-50 / Productivity Halo(Random chance to auto-click 5x) 51-85
                                             // Focus Stickers(reduces shop cost) 86-99 / Team Spirit Bonus (+1% for every 100 clicks) 99-100
                                                               
            if (rand <= 50)
            {
                UpdateRandomBoostChancePercent(1);
            }
            else if (rand <= 85)
            {
                UpdateHalo(1); 
            }
            else if (rand <= 96)
            {
                UpdateFocus(1);  
            }
            else
            {
                UpdateIncomeForHungretClicks(1);
            }
        }

        private void Normal()
        {
            _norm.Play();
            
            int rand = Random.Range(0, 100); // Coffee(Income) 0-80 / EnergyDrink(Rate) 81-85 / Clover(Luck) 86-100
            
            if (rand <= 72)
            {
                int percent = Random.Range(10, 80);
                
                float fPercent = percent / 100f;
                
                UpdateCoffeeEfficiencyAmount(fPercent);
            }
            else if (rand <= 85)
            {
                int percent = Random.Range(10, 40);
                
                float fPercent = percent / 100f;

                UpdateErgonomicClickerAmount(fPercent);
            }
            else
            {
                AddLuck();
            }
        }

        private void Junk()
        {
            //Popup.Instance.AddText("Junk", transform.position, Color.red);
        }
        
        private void UpdateCoffeeEfficiencyAmount(float percentage)
        {
            G.Passives.UpdateIncomeBoost(percentage);
        }
        
        private void UpdateErgonomicClickerAmount(float percentage)
        {
            G.Passives.UpdateRateBoost(percentage);
        }

        private void AddLuck()
        {
            if (_luck >= 100) return;
            
            _luck++;
            G.Passives.UpdateLuck(_luck);
        }
        
        private void UpdateRandomBoostChancePercent(int chance)
        {
            G.Passives.UpdateRandomBoostPercent(chance);
        }

        private void UpdateHalo(int chance)
        {
            G.Passives.UpdateAutoClick(chance);
        }
        
        private void UpdateFocus(int i)
        {
            G.Passives.UpdateDiscount(i);
        }
        
        private void UpdateIncomeForHungretClicks(int i)
        {
            G.Passives.UpdateHungretClicks(i);
        }
        
        
        private void UpdateLuckTocken(int i)
        {
            G.Passives.UpdateLuckToken(i);
        }
    }
}
