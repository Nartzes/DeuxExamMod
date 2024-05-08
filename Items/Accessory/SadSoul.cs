using Terraria;
using System;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace DeuxExamMod.Items.Accessory
{
    public class SadSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sad Soul");
            Tooltip.SetDefault("It didn't want to go, but it was taken too soon.");
        }


        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ZephyrFish); // Copy the Defaults of the Zephyr Fish Item.

            Item.shoot = ModContent.ProjectileType<GhostDogProj>(); // "Shoot" your pet projectile.
            Item.buffType = ModContent.BuffType<GhostDogBuff>(); // Apply buff upon usage of the Item.
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                player.AddBuff(Item.buffType, 3600);
            }
            return true;
        }

    }
}