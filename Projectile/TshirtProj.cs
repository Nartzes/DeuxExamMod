//using Terraria;
//using System;
//using Terraria.ID;
//using Terraria.ModLoader;
//using Microsoft.Xna.Framework;


//namespace DeuxExamMod.Projectile
//{
//    public class TshirtProj : ModProjectile
//    {

//        public override void SetDefaults()
//        {
//            Projectile.arrow = true;
//            Projectile.width = 10;
//            Projectile.height = 10;
//            Projectile.aiStyle = ProjAIStyleID.Arrow;
//            Projectile.friendly = true;
//            Projectile.DamageType = DamageClass.Ranged;
//            AIType = ProjectileID.WoodenArrowFriendly;
//        }

//        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
//        {
//            target.AddBuff(70, 180, true);

//        }

//        public override void AI()
//        {
//            int dust2 = Dust.NewDust(Projectile.Center, 1, 1, 139, 0f, 0f, 0, default(Color), 1f);
//            Main.dust[dust2].velocity *= 3f;
//            Main.dust[dust2].scale = (float)Main.rand.Next(80, 115) * 0.013f;
//            Main.dust[dust2].noGravity = true;
//        }
//    }
//} 

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DeuxExamMod.Projectile
{
    public class TshirtProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 600;
            Projectile.light = 0.75f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Bullet;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(3))
            {
                // Replace 'Fire' with a valid DustID, for example DustID.FlameBurst
                int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.FlameBurst, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
                Main.dust[dust].velocity *= 0.5f;
            }
        }
    }
}
