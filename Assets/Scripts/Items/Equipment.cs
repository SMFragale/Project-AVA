using System.Collections.Generic;
using UnityEngine;

using AVA.Combat;

namespace AVA.Item{
    
    [CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
    public class Equipment : Item
    {
        public List<StatModifier> modifiers;

        public EquipSlot equipSlot;

        public override void Use()
        {
            base.Use();

            //Must equip item, luego implementaremos la forma de equip items..

            //Return not implemented exception
            throw new System.NotImplementedException();
            
        }
    }

    public enum EquipSlot{ Head, Chest, Legs, Weapon, Feet}

}