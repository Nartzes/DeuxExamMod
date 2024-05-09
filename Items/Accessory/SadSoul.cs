using DeuxExamMod.Projectiles;
using DeuxExamMod.Buff;
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
            Item.buffType = Mod.Find<ModBuff>("GhostDogBuff").Type; // Apply buff upon usage of the Item.
            Item.shoot = Mod.Find<ModProjectile>("GhostDogProj").Type; // "Shoot" your pet projectile.

        }


        public override void SetDefaults()
        {
            Item.damage = 0;
            Item.accessory = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.noMelee = true;
            Item.width = 30;
            Item.height = 30;
            Item.value = Item.sellPrice(platinum: 1);
            Item.rare = ItemRarityID.Pink;
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