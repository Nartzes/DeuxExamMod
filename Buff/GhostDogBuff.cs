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
        { // This method gets called every frame your buff is active on your player.
            bool unused = false;
            player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref unused, ModContent.ProjectileType<GhostDogProj>());
        }
    }
}
