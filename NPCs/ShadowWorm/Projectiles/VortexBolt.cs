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
		// ((ModProjectile)this).DisplayName.SetDefault("Vortex Bolt");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 8;
		((ModProjectile)this).Projectile.height = 8;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.friendly = false;
		((ModProjectile)this).Projectile.penetrate = 2;
		((ModProjectile)this).Projectile.timeLeft = 70;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Magic;
		((ModProjectile)this).Projectile.aiStyle = 1;
		((ModProjectile)this).Projectile.alpha = 255;
		base.AIType = 14;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreAI()
	{
		float num = 1f - (float)((ModProjectile)this).Projectile.alpha / 255f;
		num *= ((ModProjectile)this).Projectile.scale;
		Lighting.AddLight(((ModProjectile)this).Projectile.Center, 0.1f * num, 0.2f * num, 0.4f * num);
		((ModProjectile)this).Projectile.rotation = (float)Math.Atan2(((ModProjectile)this).Projectile.velocity.Y, ((ModProjectile)this).Projectile.velocity.X) + 1.57f;
		((ModProjectile)this).Projectile.velocity = ((ModProjectile)this).Projectile.velocity.RotatedBy(Math.PI / 50.0);
		int num2 = Dust.NewDust(((ModProjectile)this).Projectile.Center, 4, 4, ((ModProjectile)this).Mod.Find<ModDust>("ShadowDustPurple").Type, 0f, 0f, 0, default(Color), 1.8f);
		Main.dust[num2].velocity = ((ModProjectile)this).Projectile.velocity;
		Main.dust[num2].noGravity = true;
		return true;
	}
}
