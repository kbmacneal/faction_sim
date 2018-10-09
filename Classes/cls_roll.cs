using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace faction_sim.Classes
{

    public class roller
    {
       public static List<int> RollKeeps(string base_roll)
        {            
            // int num_dice = Convert.ToInt32(base_roll[0]);

            int num_dice = 1;
            // base_roll[0] = num_dice++.ToString();

            base_roll = num_dice++.ToString() + base_roll.Substring(1,base_roll.Length-1);
            return Roll(base_roll).OrderBy(x=>x).Take(num_dice).ToList();
        }
        
        public static List<int> Roll(string roll)
        {
            List<int> dice_results = new List<int>();

            DiceBag db = new DiceBag();
            char[] dice_splits = {'d','D'};
            if (roll.Contains('-') || roll.Contains('+'))
            {
                char[] splits = new char[] { '+', '-' };
                string[] two_parts = roll.Split(splits);
                int mod = 0;
                if (roll.Contains('-'))
                {
                    mod = -1 * Convert.ToInt32(two_parts[1]);
                }
                else
                {
                    mod = Convert.ToInt32(two_parts[1]);
                }
                string[] parameters = two_parts[0].Split(dice_splits);
                uint num_dice = 1;
                int dice_sides = 2;

                if (parameters[0] != "")
                {
                    num_dice = Convert.ToUInt32(parameters[0]);
                    dice_sides = Convert.ToInt32(parameters[1]);
                }
                else
                {
                    dice_sides = Convert.ToInt32(parameters[1]);
                }

                DiceBag.Dice dice = (DiceBag.Dice)System.Enum.Parse(typeof(DiceBag.Dice), dice_sides.ToString());

                dice_results = db.RollQuantity(dice, num_dice);

                dice_results.Add(mod);
            }
            else
            {
                string[] parameters = roll.Split(dice_splits);
                uint num_dice = 1;
                int dice_sides = 2;

                if (parameters[0] != "")
                {
                    num_dice = Convert.ToUInt32(parameters[0]);
                    dice_sides = Convert.ToInt32(parameters[1]);
                }
                else
                {
                    dice_sides = Convert.ToInt32(parameters[1]);
                }

                DiceBag.Dice dice = (DiceBag.Dice)System.Enum.Parse(typeof(DiceBag.Dice), dice_sides.ToString());

                dice_results = db.RollQuantity(dice, num_dice);

            }

            return dice_results;

        }
    }

}