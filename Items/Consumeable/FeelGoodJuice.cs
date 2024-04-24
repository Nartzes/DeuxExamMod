using Humanizer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DeuxExamMod.Items.Consumeable
{
    public class FeelGoodJuice : ModItem // Consumeable ID Value starts with 10060
    {
        public override void SetDefaults()
        {
            Item.potion = true;
            Item.maxStack = 99;
            Item.value = 10010;
            Item.rare = 2;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = 9;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
        }

        public override bool? UseItem(Player player)
        {
            player.AddBuff(BuffID.Ironskin, 7200);
            player.AddBuff(BuffID.Wrath, 7200);
            player.AddBuff(BuffID.Slow, 2000);
            return true;
        }
    }
}