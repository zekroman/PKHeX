﻿using System;
using System.Collections.Generic;

namespace PKHeX.Core
{
    public sealed class EncounterStatic7 : EncounterStatic, IRelearn
    {
        public override int Generation => 7;
        public IReadOnlyList<int> Relearn { get; set; } = Array.Empty<int>();

        protected override bool IsMatchLocation(PKM pkm)
        {
            if (EggLocation == Locations.Daycare5 && Relearn.Count == 0 && pkm.RelearnMove1 != 0) // Gift Eevee edge case
                return false;
            return base.IsMatchLocation(pkm);
        }

        protected override bool IsMatchForm(PKM pkm, DexLevel evo)
        {
            if (SkipFormCheck)
                return true;

            if (FormConverter.IsTotemForm(Species, Form, Generation))
            {
                var expectForm = pkm.Format == 7 ? Form : FormConverter.GetTotemBaseForm(Species, Form);
                return expectForm == evo.Form;
            }

            return Form == evo.Form || Legal.IsFormChangeable(Species, Form, pkm.Format);
        }

        public static EncounterStatic7 GetVC1(int species, int metLevel)
        {
            bool mew = species == (int)Core.Species.Mew;
            return new EncounterStatic7
            {
                Species = species,
                Gift = true, // Forces Poké Ball
                Ability = Legal.TransferSpeciesDefaultAbility_1.Contains(species) ? 1 : 4, // Hidden by default, else first
                Shiny = mew ? Shiny.Never : Shiny.Random,
                Fateful = mew,
                Location = Locations.Transfer1,
                Level = metLevel,
                Version = GameVersion.RBY,
                FlawlessIVCount = mew ? 5 : 3,
            };
        }

        public static EncounterStatic7 GetVC2(int species, int metLevel)
        {
            bool mew = species == (int)Core.Species.Mew;
            bool fateful = mew || species == (int)Core.Species.Celebi;
            return new EncounterStatic7
            {
                Species = species,
                Gift = true, // Forces Poké Ball
                Ability = Legal.TransferSpeciesDefaultAbility_2.Contains(species) ? 1 : 4, // Hidden by default, else first
                Shiny = mew ? Shiny.Never : Shiny.Random,
                Fateful = fateful,
                Location = Locations.Transfer2,
                Level = metLevel,
                Version = GameVersion.GSC,
                FlawlessIVCount = fateful ? 5 : 3
            };
        }
    }
}
