using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AVA.Combat
{
    public class PlayerStats : CharacterStats
    {
        //Temporal forma de poner los stats mientras encuentro como poner diercto el tipo Stat desde el inspector xd
        public float health = 0;
        public float damage = 0;
        public float speed = 0; 
        public float defense = 0;
        public float attackSpeed = 0;


        private void Awake()
        {
            stats = new Dictionary<StatType, Stat>();
            
            //Forma temporal de inicializar los stats
            if (health > 0)
            {
                AddStat(StatType.Health, health);
            }
            if (damage > 0)
            {
                AddStat(StatType.Damage, damage);
            }
            if (speed > 0)
            {
                AddStat(StatType.Speed, speed);
            }
            if (defense > 0)
            {
                AddStat(StatType.Defense, defense);
            }
            if (attackSpeed > 0)
            {
                AddStat(StatType.AttackSpeed, attackSpeed);
            }
        }
    }   
}

