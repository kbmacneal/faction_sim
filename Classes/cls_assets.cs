using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace faction_sim.Classes.Assets
{


    public partial class Asset
    {
        public string Name { get; set; }
        public long hp { get; set; }
        public long max_hp { get; set; } = 0;
        public string Attack { get; set; }
        public string Counterattack { get; set; }
        public long Id { get; set; }
        public string AttackStats { get; set; }
        public string AttackDice { get; set; }
        public bool DefenderReroll { get; set; }
        public bool AttackerReroll { get; set; }
        public bool AttackerExtraDice { get; set; }
        public bool DefenderExtraDice { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public Factions.Faction owner { get; set; }

        public int instance_discriminator { get; set; } = faction_sim.Program.rand.Next(0, Int32.MaxValue - 5);

        public Asset()
        {
            this.max_hp = this.hp;
        }

        public void resetHP()
        {
            this.hp = this.max_hp;
        }
    }


}
