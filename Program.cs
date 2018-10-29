//TODO

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using faction_sim.Classes;
using faction_sim.Classes.Assets;
using faction_sim.Classes.Factions;
using Newtonsoft.Json;

namespace faction_sim
{
    public class result
    {
        public Classes.Assets.Asset asset { get; set; }
        public int iterations { get; set; }
        public int avg_damage { get; set; }
        public string chance_of_death { get; set; }
        public string hit_chance { get; set; }
        public int avg_counter_damage_taken { get; set; }
        public int avg_damage_per_swing { get; set; }
        public int total_damage { get; set; }
        public int total_deaths { get; set; }
        public int total_successes { get; set; }
        public int average_faction_atk_damage { get; set; }
        public int average_faction_def_damage { get; set; }
        public string chance_of_kill { get; set; }
        public string attacker_average_faction_damage { get; set; }
        public string hit_chance_less_death_chance { get; set; }
        public string average_total_stack_damage { get; set; }
    }

    public class round
    {
        public Asset attacking_asset { get; set; }
        public Asset defending_asset { get; set; }
        public bool atk_success { get; set; }
        public int atk_roll { get; set; }
        public int def_roll { get; set; }
        public int damage { get; set; }
        public int counter_damage { get; set; }
    }

    public class run_options
    {
        public int attacking_id { get; set; }
        public List<int> attacking_assets { get; set; }
        public int defending_id { get; set; }
        public List<int> defending_assets { get; set; }
        public int iterations { get; set; }

        public run_options()
        {
            this.attacking_assets = new List<int>();
            this.defending_assets = new List<int>();
        }
    }

    class Program
    {
        public static Random rand = new Random();
        public static run_options _runoptions = new run_options();
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                //     List<Asset> assets = Asset.FromJson(System.IO.File.ReadAllText("assets.json")).ToList();
                // List<Faction> factions = Faction.FromJson(System.IO.File.ReadAllText("factions.json")).ToList();

                // assets.ForEach(e=> Classes.Assets.Asset.InsertAsset(e));
                // factions.ForEach(e=> Classes.Factions.Faction.InsertFaction(e));
                Console.WriteLine("-f for a file input, -i for an interactive input. after input file specify full output location");
                return;
            }

            if (args[0] == "-f")
            {
                List<List<round>> results = new List<List<round>>();

                _runoptions = Newtonsoft.Json.JsonConvert.DeserializeObject<run_options>(System.IO.File.ReadAllText(args[1]));

                // _runoptions = Newtonsoft.Json.JsonConvert.DeserializeObject<run_options>(System.IO.File.ReadAllText("options.json"));

                string out_location = args[3];
                // string out_location = "options_results.json";

                int[] atk_assets = _runoptions.attacking_assets.Select(e => e).ToArray();

                int[] def_assets = _runoptions.defending_assets.Select(e => e).ToArray();

                List<Asset> attacking_assets = initialize_assets(atk_assets);

                List<Asset> defending_assets = initialize_assets(def_assets);

                for (int i = 0; i < _runoptions.iterations; i++)
                {

                    attacking_assets.ForEach(e=>e.resetHP());
                    defending_assets.ForEach(e=>e.resetHP());
                    var result = run_sim(Faction.GetFaction(_runoptions.attacking_id), Faction.GetFaction(_runoptions.defending_id), attacking_assets, defending_assets);
                    results.Add(result);
                }

                var stats = get_results(results, attacking_assets, _runoptions.iterations);

                System.IO.File.WriteAllText(out_location, Newtonsoft.Json.JsonConvert.SerializeObject(stats, Formatting.Indented));
                return;
            }

            if (args[0] == "i")
            {
                display_options();
            }

        }

        private static bool reroll_same_or_other(int raw_a, int raw_b)
        {
            bool reroll_same = false;

            int value_a = 10 - raw_a;
            int value_b = raw_b - 1;

            if (value_a > value_b) reroll_same = true;

            return reroll_same;
            //Value A = 10 minus the unmodified Houses Minor roll
            //Value B = unmodified Other Faction roll minus 1

            // If value A > value B, use the Book of Secrets to reroll our die.
            // If value A <= value B, use the Book of Screts to reroll the other faction's die.
        }

        private static void display_options()
        {
            Dictionary<int, string> options = new Dictionary<int, string> { { 1, "Set Attacking Faction" },
                { 2, "Add Attacking Faction Asset" },
                { 3, "Set Defending Faction" },
                { 4, "Add Defending Faction Asset" },
                { 5, "Set Iteration Count" },
                { 6, "Run" }
            };

            Console.Clear();

            // List<Asset> assets = Asset.FromJson(System.IO.File.ReadAllText("assets.json")).ToList();
            List<Asset> assets = Asset.GetAsset();
            // List<Faction> factions = Faction.FromJson(System.IO.File.ReadAllText("factions.json")).ToList();
            List<Faction> factions = Faction.GetFaction();
            Faction attacking_faction = factions.FirstOrDefault(e => e.Id == _runoptions.attacking_id);
            Faction defending_faction = factions.FirstOrDefault(e => e.Id == _runoptions.defending_id);

            List<Asset> attacking_assets = assets.Where(e => _runoptions.attacking_assets.Contains((int)e.Id)).ToList();
            List<Asset> defending_assets = assets.Where(e => _runoptions.defending_assets.Contains((int)e.Id)).ToList();

            Console.WriteLine("Attacking Faction");
            if (attacking_faction != null)
            {
                Console.WriteLine(attacking_faction.FactionName);
            }

            Console.WriteLine("Attacking Assets");
            Console.WriteLine(string.Join(", ", attacking_assets.Select(e => e.Name)));

            Console.WriteLine("");

            Console.WriteLine("Defending Faction");
            if (defending_faction != null)
            {
                Console.WriteLine(defending_faction.FactionName);
            }

            Console.WriteLine("Defending Assets");
            Console.WriteLine(string.Join(", ", defending_assets.Select(e => e.Name)));

            Console.WriteLine("");

            Console.WriteLine("Interations:");
            Console.WriteLine(_runoptions.iterations.ToString());

            Console.WriteLine("");

            foreach (var item in options)
            {
                Console.WriteLine(item.Key.ToString() + ": " + item.Value);
            }

            Console.WriteLine("Select Option:");
            int selection = Int32.Parse(Console.ReadLine());

            switch (selection)
            {
                case 1:
                    Console.WriteLine("Enter the Faction Name:");
                    string faction = Console.ReadLine();

                    _runoptions.attacking_id = Convert.ToInt32(factions.FirstOrDefault(e => e.FactionName == faction).Id);

                    display_options();
                    break;
                case 2:
                    Console.WriteLine("Enter the Asset Name:");
                    string asset = Console.ReadLine();

                    _runoptions.attacking_assets.Add(Convert.ToInt32(assets.FirstOrDefault(e => e.Name == asset).Id));

                    display_options();

                    break;
                case 3:
                    Console.WriteLine("Enter the Faction Name:");
                    string fac = Console.ReadLine();

                    _runoptions.defending_id = Convert.ToInt32(factions.FirstOrDefault(e => e.FactionName == fac).Id);

                    display_options();
                    break;
                case 4:
                    Console.WriteLine("Enter the Asset Name:");
                    string ass = Console.ReadLine();

                    _runoptions.defending_assets.Add(Convert.ToInt32(assets.FirstOrDefault(e => e.Name == ass).Id));

                    display_options();
                    break;
                case 5:
                    Console.WriteLine("Enter iterations:");

                    _runoptions.iterations = Convert.ToInt32(Console.ReadLine());

                    display_options();
                    break;
                case 6:
                    List<List<round>> results = new List<List<round>>();

                    int[] atk_assets = _runoptions.attacking_assets.Select(e => e).ToArray();

                    int[] def_assets = _runoptions.defending_assets.Select(e => e).ToArray();

                    List<Asset> at_assets = initialize_assets(atk_assets);

                    List<Asset> de_assets = initialize_assets(def_assets);

                    for (int i = 0; i < _runoptions.iterations; i++)
                    {
                        var result = run_sim(Faction.GetFaction(_runoptions.attacking_id), Faction.GetFaction(_runoptions.defending_id), at_assets, de_assets);
                        results.Add(result);
                    }

                    var stats = get_results(results, at_assets, _runoptions.iterations);

                    System.IO.File.WriteAllText("stats.json", Newtonsoft.Json.JsonConvert.SerializeObject(stats, Formatting.Indented));
                    break;
                default:
                    break;
            }
        }

        private static List<result> get_results(List<List<round>> results, List<Asset> assets, int iterations)
        {
            // System.IO.File.WriteAllText("results.json", JsonConvert.SerializeObject(results));
            List<result> rtner = new List<result>();

            int total_attacker_damage = 0;

            foreach (var asset in assets)
            {

                List<round> result_set = new List<round>();
                
                results.ForEach(e=> e.Where(f=>f.attacking_asset.instance_discriminator == asset.instance_discriminator).ToList().ForEach(g=>result_set.Add(g)));

                System.IO.File.WriteAllText("result_set.json",JsonConvert.SerializeObject(result_set));

                int total_damage = result_set.Select(e=>e.damage).Sum();
                int total_successes = result_set.Where(e=>e.atk_success).Count();
                int total_deaths = result_set.Where(e=>e.attacking_asset.Hp == 0).Count();
                int total_counter = result_set.Select(e=>e.counter_damage).Sum();
                int atk_faction_damage = result_set.Select(e=>e.damage).Sum();
                total_attacker_damage += atk_faction_damage;

                int total_kills = result_set.Where(e=>e.defending_asset != null).Where(e=>e.defending_asset.Hp == 0).Count();
                int def_faction_damage = result_set.Select(e=>e.counter_damage).Sum();
                int attacker_direct_damage = result_set.Where(e=>e.defending_asset == null).Select(e=>e.damage).Sum();

                int round_damage = 0;

                results.ForEach(e=> e.Where(f=>f.attacking_asset.instance_discriminator == asset.instance_discriminator).ToList().ForEach(g=> round_damage += g.damage));

                if (asset.Name == "Treachery")
                {
                    continue;
                }
                result result = new result();
                result.asset = asset;

                if (total_successes != 0)
                {
                    result.avg_damage = total_damage / total_successes;
                }
                else
                {
                    result.avg_damage = 0;
                }

                if (total_successes != 0)
                {
                    double doub_direct = (double)attacker_direct_damage / (double)total_successes;
                    result.attacker_average_faction_damage = string.Format("{0:N6}", doub_direct);
                }
                else
                {
                    result.avg_damage = 0;
                }

                result.total_damage = total_damage;
                result.total_deaths = total_deaths;
                result.total_successes = total_successes;
                double doub_death = (double)total_deaths / (double)iterations;
                result.chance_of_death = string.Format("{0:N6}", doub_death);
                double doub_hit = (double)total_successes / (double)iterations;

                result.hit_chance_less_death_chance = string.Format("{0:N6}", ((double)total_successes / (double)iterations) - ((double)total_deaths / (double)iterations));
                result.hit_chance = string.Format("{0:N6}", doub_hit);
                result.iterations = iterations;
                if (iterations == total_successes)
                {
                    result.avg_counter_damage_taken = 0;
                }
                else
                {
                    result.avg_counter_damage_taken = total_counter / (iterations - total_successes);
                }

                result.avg_damage_per_swing = total_damage / iterations;
                result.average_faction_atk_damage = atk_faction_damage / iterations;
                result.average_faction_def_damage = def_faction_damage / iterations;
                

                if (total_successes > 0)
                {
                    double doub_kill = (double)total_kills / (double)total_successes;
                    result.chance_of_kill = string.Format("{0:N6}", doub_kill);
                }
                else
                {
                    result.chance_of_kill = ".000000";
                }
                rtner.Add(result);

            }

            double average_faction_dmg = (double)rtner.Select(e=>e.avg_damage).Average() * (double)assets.Count();

            rtner.ForEach(e=>e.average_total_stack_damage = string.Format("{0:N6}", average_faction_dmg));

            return rtner;

        }

        private static Dictionary<Classes.Factions.Faction, List<Int32>> initialize_stacks(int attacking_id, int defending_id, string[] attacking_ass, string[] defending_ass)
        {
            Dictionary<Classes.Factions.Faction, List<Int32>> rtner = new Dictionary<Classes.Factions.Faction, List<Int32>>();

            List<Classes.Factions.Faction> combatants = initialize_factions(attacking_id, defending_id);

            List<Int32> attacking_assets = get_ids(attacking_ass);

            List<Int32> defending_assets = get_ids(defending_ass);

            rtner.Add(combatants[0], attacking_assets);

            rtner.Add(combatants[1], defending_assets);

            return rtner;
        }

        private static List<round> run_sim(Faction attacking_faction, Faction defending_faction, List<Asset> attackers, List<Asset> defenders)
        {
            List<round> results = new List<round>();

            foreach (Asset attacker in attackers)
            {
                var atk = attacker;
                List<Asset> eligible_defenders = defenders.Where(e => e.Hp > 0).ToList();

                if (atk.AttackStats == "None") continue;

                if (eligible_defenders.Count == 0)
                {
                    round result = run_round(atk, null, attacking_faction, defending_faction);

                    results.Add(result);
                }
                else
                {
                    Asset rand_defender = eligible_defenders.ToArray()[rand.Next(eligible_defenders.Count())];
                    round result = run_round(atk, rand_defender, attacking_faction, defending_faction);

                    results.Add(result);
                }

            }

            return results;
        }

        private static round run_round(Asset attacker, Asset defender, Faction atk_faction, Faction def_faction)
        {
            round rnd = new round();

            rnd.attacking_asset = attacker;

            rnd.defending_asset = defender;

            if (defender == null)
            {
                string[] vs_roll = attacker.AttackStats.Split("v");

                long atk_mod = (long)helpers.GetPropValue(atk_faction, short_to_long[vs_roll[0]]);

                string atk_roll = "1d10+" + atk_mod.ToString();

                int atk_result = roller.Roll(atk_roll).Sum();
                rnd.atk_roll = atk_result;

                rnd.atk_success = true;

                rnd.damage = roller.Roll(attacker.AttackDice).Sum();

                rnd.counter_damage = 0;

                return rnd;
            }
            else
            {
                string[] vs_roll = attacker.AttackStats.Split("v");

                long atk_mod = (long)helpers.GetPropValue(atk_faction, short_to_long[vs_roll[0]]);

                long def_mod = (long)helpers.GetPropValue(def_faction, short_to_long[vs_roll[1]]);

                int atk_result = 0;
                int def_result = 0;

                string atk_roll = calculate_diceroll(atk_faction, short_to_long[vs_roll[0]]) + "+" + atk_mod.ToString();

                string def_roll = calculate_diceroll(def_faction, short_to_long[vs_roll[1]]) + "+" + def_mod.ToString();

                if (attacker.AttackerExtraDice)
                {
                    atk_roll = add_dice(atk_roll);
                    atk_result = roller.RollKeeps(atk_roll).Sum();
                    rnd.atk_roll = atk_result;
                }
                else
                {
                    if (atk_faction.PMax)
                    {
                        atk_result = roller.RollKeeps(atk_roll).Sum();
                        rnd.atk_roll = atk_result;
                    }
                    else
                    {
                        atk_result = roller.Roll(atk_roll).Sum();
                        rnd.atk_roll = atk_result;
                    }

                }

                if (defender.DefenderExtraDice)
                {
                    def_roll = add_dice(def_roll);
                    def_result = roller.RollKeeps(def_roll).Sum();
                    rnd.def_roll = def_result;
                }
                else
                {
                    if (def_faction.PMax)
                    {
                        def_result = roller.RollKeeps(def_roll).Sum();
                        rnd.def_roll = def_result;
                    }
                    else
                    {
                        def_result = roller.Roll(def_roll).Sum();
                        rnd.def_roll = def_result;
                    }

                }

                if (atk_result < def_result && attacker.AttackerReroll)
                {
                    if (reroll_same_or_other(atk_result, def_result))
                    {
                        atk_result = roller.Roll(calculate_diceroll(atk_faction, short_to_long[vs_roll[0]]) + "+" + atk_mod.ToString()).Sum();
                        rnd.atk_roll = atk_result;
                    }
                    else
                    {
                        def_result = roller.Roll(calculate_diceroll(def_faction, short_to_long[vs_roll[1]]) + "+" + def_mod.ToString()).Sum();
                    }
                }

                if (atk_result >= def_result && defender.DefenderReroll)
                {

                    if (reroll_same_or_other(def_result, atk_result))
                    {
                        def_result = roller.Roll(calculate_diceroll(def_faction, short_to_long[vs_roll[1]]) + "+" + def_mod.ToString()).Sum();
                        rnd.def_roll = def_result;
                    }
                    else
                    {
                        atk_result = roller.Roll(calculate_diceroll(atk_faction, short_to_long[vs_roll[0]]) + "+" + atk_mod.ToString()).Sum();
                        rnd.atk_roll = atk_result;
                    }
                }

                if (atk_result >= def_result)
                {
                    rnd.atk_success = true;

                    rnd.damage = roller.Roll(attacker.AttackDice).Sum();
                    if (rnd.damage >= defender.Hp)
                    {
                        rnd.damage = Convert.ToInt32(defender.Hp);
                        defender.Hp = 0;
                    }
                    else
                    {
                        defender.Hp = defender.Hp - rnd.damage;
                    }

                    rnd.counter_damage = 0;
                }

                if (def_result >= atk_result)
                {
                    rnd.atk_success = false;
                    rnd.damage = 0;
                    if (defender.Counterattack == "None")
                    {
                        rnd.counter_damage = 0;
                    }
                    else
                    {
                        rnd.counter_damage = roller.Roll(defender.Counterattack).Sum();
                        attacker.Hp = attacker.Hp - rnd.counter_damage;
                    }
                }

                if (def_result == atk_result)
                {
                    rnd.atk_success = true;
                }

                return rnd;
            }

        }

        private static List<int> get_ids(string[] assets)
        {
            List<int> rtner = new List<int>();

            assets.ToList().ForEach(e => rtner.Add(Convert.ToInt32(e)));

            return rtner;
        }

        private static List<Classes.Assets.Asset> initialize_assets(int[] ids)
        {
            List<Classes.Assets.Asset> rtner = new List<Classes.Assets.Asset>();

            List<Classes.Assets.Asset> master_list = Asset.GetAsset();

            foreach (int id in ids)
            {
                Asset asset = new Asset();
                asset = master_list.First(f => f.Id == id);
                rtner.Add(asset);
            }

            return rtner;
        }

        private static List<Classes.Factions.Faction> initialize_factions(int faction_atk, int faction_defend)
        {
            List<Classes.Factions.Faction> rtner = new List<Classes.Factions.Faction>();

            List<Classes.Factions.Faction> master_list = Faction.GetFaction();

            rtner.Add(master_list.First(e => e.Id == faction_atk));
            rtner.Add(master_list.First(e => e.Id == faction_defend));

            return rtner;
        }

        private static string calculate_diceroll(Faction faction, string roll_stat)
        {
            int num_dice = 1;

            if (faction.PMax && roll_stat == "Cunning")
            {
                num_dice++;
            }

            return num_dice.ToString() + "d10";
        }

        private static string add_dice(string diceroll)
        {
            string[] roll = diceroll.Split("d");

            if (roll.Length == 2)
            {
                int num_dice = Convert.ToInt32(roll[0]) + 1;

                return string.Concat(num_dice, "d", roll[1]);
            }
            else
            {
                return string.Concat(2, diceroll);
            }
        }

        private static Dictionary<string, string> short_to_long = new Dictionary<string, string> { { "C", "Cunning" },
            { "W", "Wealth" },
            { "F", "Force" }
        };

    }
}