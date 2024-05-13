using DeuxExamMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DeuxExamMod.Buff
{
    public class GhostDogBuff : ModBuff
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Old Yeller");
            Description.SetDefault("After that, I couldn’t do enough for Old Yeller.");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            bool unused = false;
            player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref unused, Mod.Find<ModProjectile>("GhostDogProj").Type);
        }
    }
}
