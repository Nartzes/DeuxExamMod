using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DeuxExamMod.Items.Accessories
{
    public class Pillow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pillow");
            Tooltip.SetDefault("You look sleepy, I can fix that...");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = ItemRarityID.Purple;

        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Passively give an 8% damage boost
            player.GetDamage(DamageClass.Generic) += 0.08f; // Increases generic damage by 8%

            // Check player's movement for HP regen rate
            if (player.velocity.X == 0 && player.velocity.Y == 0)
            {
                // Higher regen rate when standing still
                player.lifeRegen += 5000; // HP per minute
            }
            else
            {
                // Lower regen rate while moving
                player.lifeRegen += 500; // HP per minute
            }
        }
    }
}