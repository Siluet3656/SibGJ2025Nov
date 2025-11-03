using UnityEngine;

namespace Main.Scripts
{
    public class Gambling : MonoBehaviour
    {
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
            float junkChance = 30f;
            float normalChance = 50f;
            float rareChance = 14f;
            float cursedChance = 6f; // фиксированный

            // Применяем удачу (0–100): чем выше удача, тем меньше мусора и обычных предметов, больше редких
            float luckFactor = _luck / 100f;

            // junk уменьшается максимум до 10%
            junkChance = Mathf.Lerp(30f, 10f, luckFactor);

            // rare увеличивается максимум до 30%
            rareChance = Mathf.Lerp(14f, 30f, luckFactor);

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
                Cursed();
            }
            
            //Debug.Log($"Junk: {junkChance}, Normal: {normalChance}, Rare: {rareChance}, Cursed: {cursedChance}");
        }

        private void Cursed()
        {
            int rand = Random.Range(0, 100); // Reclaimed Memory Chip 0-50 (+500% income buy doubles your debts ) // Synthetic Luck Token (Time slow/speed up each 10 sec) 51 - 100
            
            if (rand <= 50)
            {
                // Reclaimed Memory Chip 0-50 (+500% income buy doubles your debts )
            }
            else
            {
                // Synthetic Luck Token  (Time slow/speed up each 10 sec)
            }
        }

        private void Rare()
        {
            int rand = Random.Range(0, 100); // Corporate Mantra Chip(Mult boost chance) 0-50 / Productivity Halo(Random chance to auto-click 5x) 51-85
                                             // Focus Stickers(reduces shop cost) 86-90 / Team Spirit Bonus (+1% for every 100 clicks) 91-100
                                                               
            if (rand <= 50)
            {
                UpdateRandomBoostChancePercent(1);
            }
            else if (rand <= 85)
            {
                UpdateHalo(1); 
            }
            else if (rand <= 90)
            {
                UpdateFocus(1);  
            }
            else
            { Debug.Log($"Team Spirit Bonus "); 
                // Team Spirit Bonus (+1% for every 100 clicks) 
            }
        }

        private void Normal()
        {
            int rand = Random.Range(0, 100); // Coffee(Income) 0-80 / EnergyDrink(Rate) 81-85 / Clover(Luck) 86-100
            
            if (rand <= 80)
            {
                int percent = Random.Range(10, 50);
                
                float fPercent = percent / 100f;
                
                UpdateCoffeeEfficiencyAmount(fPercent);
            }
            else if (rand <= 86)
            {
                int percent = Random.Range(10, 30);
                
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
    }
}
