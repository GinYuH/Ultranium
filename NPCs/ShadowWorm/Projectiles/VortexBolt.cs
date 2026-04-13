using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowWorm.Projectiles;

public class VortexBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Vortex Bolt");
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		Projectile.width = 8;
		Projectile.height = 8;
		Projectile.hostile = true;
		Projectile.friendly = false;
		Projectile.penetrate = 2;
		Projectile.timeLeft = 70;
		Projectile.tileCollide = false;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.aiStyle = 1;
		Projectile.alpha = 255;
		base.AIType = 14;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreAI()
	{
		float num = 1f - (float)Projectile.alpha / 255f;
		num *= Projectile.scale;
		Lighting.AddLight(Projectile.Center, 0.1f * num, 0.2f * num, 0.4f * num);
		Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
		Projectile.velocity = Projectile.velocity.RotatedBy(Math.PI / 50.0);
		int num2 = Dust.NewDust(Projectile.Center, 4, 4, Mod.Find<ModDust>("ShadowDustPurple").Type, 0f, 0f, 0, default(Color), 1.8f);
		Main.dust[num2].velocity = Projectile.velocity;
		Main.dust[num2].noGravity = true;
		return true;
	}
}
