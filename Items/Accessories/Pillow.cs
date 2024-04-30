using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Humanizer;


namespace DeuxExamMod.Items.Accessories
{
    public class Pillow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pilow ");
            Tooltip.SetDefault("you look sleep i can fix thaaat");
        }
        public override void SetDefaults() 
        {
            Item.width = 20;
            Item.height = 20;

            Item.accessory = true;
        }
        public override void UpdateAccesory(Player player,bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += 0.25f;
        }
    }
}
