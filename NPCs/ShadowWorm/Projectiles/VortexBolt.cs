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
		((ModProjectile)this).DisplayName.SetDefault("Vortex Bolt");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 8;
		((ModProjectile)this).projectile.height = 8;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.penetrate = 2;
		((ModProjectile)this).projectile.timeLeft = 70;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.magic = true;
		((ModProjectile)this).projectile.aiStyle = 1;
		((ModProjectile)this).projectile.alpha = 255;
		base.aiType = 14;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreAI()
	{
		float num = 1f - (float)((ModProjectile)this).projectile.alpha / 255f;
		num *= ((ModProjectile)this).projectile.scale;
		Lighting.AddLight(((ModProjectile)this).projectile.Center, 0.1f * num, 0.2f * num, 0.4f * num);
		((ModProjectile)this).projectile.rotation = (float)Math.Atan2(((ModProjectile)this).projectile.velocity.Y, ((ModProjectile)this).projectile.velocity.X) + 1.57f;
		((ModProjectile)this).projectile.velocity = ((ModProjectile)this).projectile.velocity.RotatedBy(Math.PI / 50.0);
		int num2 = Dust.NewDust(((ModProjectile)this).projectile.Center, 4, 4, ((ModProjectile)this).mod.DustType("ShadowDustPurple"), 0f, 0f, 0, default(Color), 1.8f);
		Main.dust[num2].velocity = ((ModProjectile)this).projectile.velocity;
		Main.dust[num2].noGravity = true;
		return true;
	}
}
