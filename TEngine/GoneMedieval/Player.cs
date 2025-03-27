using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.GoneMedieval
{
    public class Player
    {
        public string Name { get; set; } = "Red";
        public int Health { get; set; } = 10;
        public int MaxHealth { get; set; } = 10;
        public int Level { get; set; } = 1;
        public int Experience { get; set; } = 0;
 
        public int CriticalChance { get; private set; } = 1; // 1% chance to deal double damage
        public double CriticalDamage { get; private set; } = 2; // Multiplier for critical damage
        public int Dexterity { get; private set; } = 1; // Weapon skill
        public int Strength { get; private set; } = 1; // Strength requirements for weapon/armor usage
        public int Intelligence { get; private set; } = 1; // Intelligence requirements for spell usage
        public int Wisdom { get; private set; } = 1; // Wisdom requirements for spell usage
        public int Vitality { get; private set; } = 1; // Health increase per level
        public int Mana { get; private set; } = 0; // Mana pool for spell usage

        public Player()
        {

        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
        }

        public void Heal(int amount)
        {
            Health += amount;
            if (Health > MaxHealth) Health = MaxHealth;
        }

        public void GainExperience(int exp)
        {
            Experience += exp;
            // Check for level up
            while (Experience >= GetExperienceToNextLevel())
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            Experience -= GetExperienceToNextLevel();
            MaxHealth += 1 * Vitality; // Increase max health on level up
            Health = MaxHealth; // Restore health to max
            EventManager.Instance.Trigger("LEVEL");
        }

        private int GetExperienceToNextLevel()
        {
            return Level * 100; // Example formula for experience needed to level up
        }

        public bool UpgradeCriticalChance()
        {
            if (CriticalChance >= 100) return false; // Max critical chance reached
            CriticalChance += 2; // Increase critical chance by 2% on level up
            return true;
        }

        public bool UpgradeCriticalDamage()
        {
            CriticalDamage += .2; // Increase critical damage on level up
            return true;
        }

        public bool UpgradeDexterity()
        {
            if (Dexterity >= 10) return false; // Max dexterity reached
            Dexterity += 1; // Increase dexterity by 1 on level up
            return true;
        }

        public bool UpgradeStrength()
        {
            if(Strength >= 10) return false; // Max strength reached

            Strength += 1; // Increase strength by 1 on level up
            return true;
        }

        public bool UpgradeIntelligence()
        {
            if (Intelligence >= 10) return false; // Max intelligence reached
            Intelligence += 1; // Increase intelligence by 1 on level up
            return true;
        }

        public bool UpgradeWisdom()
        {
            if (Wisdom >= 10) return false; // Max wisdom reached
            Wisdom += 1; // Increase wisdom by 1 on level up
            return true;
        }

        public bool UpgradeVitality()
        {
            if (Vitality >= 10) return false; // Max vitality reached
            Vitality += 1; // Increase vitality by 1 on level up
            return true;
        }

        public bool UpgradeMana()
        {
            Mana += 1; // Increase mana by 1 on level up
            return true;
        }
    }
}
