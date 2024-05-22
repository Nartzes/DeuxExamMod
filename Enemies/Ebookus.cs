//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using Microsoft.Xna.Framework;
//using Terraria.GameContent.ItemDropRules;
//using DeuxExamMod.Items.Misc;  // Ensure the namespace matches where CollegeNote and BookMark are defined

//namespace DeuxExamMod.Enemies
//{
//    public class Ebookus : ModNPC
//    {
//        private int shootingCooldown = 60; // Cooldown for shooting, adjust as needed

//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("Ebookus");
//            Main.npcFrameCount[NPC.type] = 8; // The image has 8 frames for the NPC's animation
//        }

//        public override void SetDefaults()
//        {
//            NPC.width = 36;
//            NPC.height = 36;
//            NPC.damage = 45;
//            NPC.defense = 20;
//            NPC.lifeMax = 400;
//            NPC.HitSound = SoundID.NPCHit4;
//            NPC.DeathSound = SoundID.NPCDeath6;
//            NPC.value = 100f;
//            NPC.knockBackResist = 0.5f;
//            NPC.aiStyle = -1;
//            NPC.noGravity = true;
//            NPC.noTileCollide = true;
//        }

//        public override void AI()
//        {
//            NPC.TargetClosest(true);
//            Player target = Main.player[NPC.target];
//            if (!target.dead && target.active)
//            {
//                HoverAroundPlayer(target);
//                ShootAtPlayer(target);
//            }
//            HandleAnimation();
//        }

//        private void HoverAroundPlayer(Player player)
//        {
//            float desiredDistance = 200f; // Desired hover distance from the player
//            Vector2 moveDirection = player.Center - NPC.Center;
//            float currentDistance = moveDirection.Length();

//            Normalize direction vector only if we're not too close
//            if (currentDistance > 50f) // Avoid getting too close to the player
//            {
//                moveDirection.Normalize();
//                float distanceDifference = currentDistance - desiredDistance;

//                Move towards or away from the player based on the distance difference
//                NPC.velocity = (NPC.velocity * 0.9f) + moveDirection * distanceDifference * 0.1f;
//            }
//            else
//            {
//                If too close, move directly away quickly
//                moveDirection.Normalize();
//                NPC.velocity = -moveDirection * 4f;
//            }
//        }

//        private void ShootAtPlayer(Player player)
//        {
//            shootingCooldown--;
//            if (shootingCooldown <= 0)
//            {
//                Vector2 shootDirection = player.Center - NPC.Center;
//                shootDirection.Normalize();
//                shootDirection *= 10f; // Speed of the projectile

//                int projectileType = Mod.Find<ModProjectile>("EbookusProj").Type;
//                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, shootDirection, projectileType, NPC.damage / 2, 0, Main.myPlayer);

//                shootingCooldown = 60; // Reset cooldown
//            }
//        }

//        private void HandleAnimation()
//        {
//            NPC.frameCounter++;
//            if (NPC.frameCounter > 8) // Adjust the speed of the animation by changing this value
//            {
//                NPC.frameCounter = 0;
//                NPC.frame.Y = (NPC.frame.Y + NPC.frame.Height) % (Main.npcFrameCount[NPC.type] * NPC.frame.Height);
//            }
//        }

//        public override void OnKill()
//        {
//            Drop CollegeNote(common)
//            int itemType = ModContent.ItemType<CollegeNote>(); // Ensure you have the right namespace and class name here
//            Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), itemType);

//            Drop Bookmark(uncommon)
//            itemType = Mod.Find<ModItem>("BookMark").Type; // Ensure you have the right namespace and class name here
//            if (Main.rand.NextFloat() < 0.25f) // 25% chance to drop
//            {
//                Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), itemType);
//            }
//        }

//        public override float SpawnChance(NPCSpawnInfo spawnInfo)
//        {
//            bool isNightTime = !Main.dayTime;
//            bool hasDefeatedSkeletron = NPC.downedBoss3;
//            bool playerNearSurface = spawnInfo.Player.ZoneOverworldHeight;

//            if (isNightTime && hasDefeatedSkeletron && playerNearSurface)
//            {
//                return 1.0f; // 100% chance to spawn at night after Skeletron is defeated
//            }

//            return 0f;
//        }
//    }
//}




using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.ItemDropRules;
using DeuxExamMod.Items.Misc;  // Ensure the namespace matches where CollegeNote is defined
using DeuxExamMod.Items.Weapon.Melee;  // Ensure the namespace matches where BookMark is defined

namespace DeuxExamMod.Enemies
{
    public class Ebookus : ModNPC
    {
        private int shootingCooldown = 60; // Cooldown for shooting, adjust as needed

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ebookus");
            Main.npcFrameCount[NPC.type] = 8; // The image has 8 frames for the NPC's animation
        }

        public override void SetDefaults()
        {
            NPC.width = 36;
            NPC.height = 36;
            NPC.damage = 45;
            NPC.defense = 20;
            NPC.lifeMax = 400;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = 100f;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
        }

        public override void AI()
        {
            NPC.TargetClosest(true);
            Player target = Main.player[NPC.target];
            if (!target.dead && target.active)
            {
                HoverAroundPlayer(target);
                ShootAtPlayer(target);
            }
            HandleAnimation();
        }

        private void HoverAroundPlayer(Player player)
        {
            float desiredDistance = 200f; // Desired hover distance from the player
            Vector2 moveDirection = player.Center - NPC.Center;
            float currentDistance = moveDirection.Length();

            // Normalize direction vector only if we're not too close
            if (currentDistance > 50f) // Avoid getting too close to the player
            {
                moveDirection.Normalize();
                float distanceDifference = currentDistance - desiredDistance;

                // Move towards or away from the player based on the distance difference
                NPC.velocity = (NPC.velocity * 0.9f) + moveDirection * distanceDifference * 0.1f;
            }
            else
            {
                // If too close, move directly away quickly
                moveDirection.Normalize();
                NPC.velocity = -moveDirection * 4f;
            }
        }

        private void ShootAtPlayer(Player player)
        {
            shootingCooldown--;
            if (shootingCooldown <= 0)
            {
                Vector2 shootDirection = player.Center - NPC.Center;
                shootDirection.Normalize();
                shootDirection *= 10f; // Speed of the projectile

                int projectileType = Mod.Find<ModProjectile>("EbookusProj").Type;
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, shootDirection, projectileType, NPC.damage / 2, 0, Main.myPlayer);

                shootingCooldown = 60; // Reset cooldown
            }
        }

        private void HandleAnimation()
        {
            NPC.frameCounter++;
            if (NPC.frameCounter > 8) // Adjust the speed of the animation by changing this value
            {
                NPC.frameCounter = 0;
                NPC.frame.Y = (NPC.frame.Y + NPC.frame.Height) % (Main.npcFrameCount[NPC.type] * NPC.frame.Height);
            }
        }

        public override void OnKill()
        {
            // Drop CollegeNote (common)
            int itemType = ModContent.ItemType<CollegeNote>(); // Ensure you have the right namespace and class name here
            Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), itemType);

            // Drop Bookmark (uncommon)
            itemType = ModContent.ItemType<BookMark>(); // Ensure you have the right namespace and class name here
            if (Main.rand.NextFloat() < 0.25f) // 25% chance to drop
            {
                Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), itemType);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool isNightTime = !Main.dayTime;
            bool hasDefeatedSkeletron = NPC.downedBoss3;
            bool playerNearSurface = spawnInfo.Player.ZoneOverworldHeight;

            if (isNightTime && hasDefeatedSkeletron && playerNearSurface)
            {
                return 1.0f; // 100% chance to spawn at night after Skeletron is defeated
            }

            return 0f;
        }
    }
}
