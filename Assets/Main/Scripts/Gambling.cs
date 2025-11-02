using UnityEngine;

namespace Main.Scripts
{
    public class Gambling : MonoBehaviour
    {
        private void Awake()
        {
            G.Gambling = this;
        }

        public void Gamba()
        {
            int rand = Random.Range(0, 100);

            if (rand < 25)
            {
                int percent = Random.Range(0, 50);
                
                float fPercent = percent / 100f;
                
                UpdateCoffeeEfficiencyAmount(fPercent);
            }
            else if (rand <= 50)
            {
                int percent = Random.Range(0, 50);
                
                float fPercent = percent / 100f;

                UpdateErgonomicClickerAmount(fPercent);
            }
            else if (rand <= 90)
            {
                UpdateRandomBoostChancePercent(1);
            }
            else
            {
                Junk();
            }
        }

        public void Junk()
        {
            //Popup.Instance.AddText("Junk", transform.position, Color.red);
        }
        
        public void UpdateCoffeeEfficiencyAmount(float percentage)
        {
            G.Passives.UpdateIncomeBoost(percentage);
        }
        
        public void UpdateErgonomicClickerAmount(float percentage)
        {
            G.Passives.UpdateRateBoost(percentage);
        }

        public void UpdateRandomBoostChancePercent(int chance)
        {
            G.Passives.UpdateRandomBoostPercent(chance);
        }
    }
}
