using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DeuxExamModv1.Items
{
    public class FeelGoodJuice : ModItem
    {
        public override void SetDefaults()
        {
            Item.potion = true;
            Item.maxStack = 99;
            Item.value = 10020;
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