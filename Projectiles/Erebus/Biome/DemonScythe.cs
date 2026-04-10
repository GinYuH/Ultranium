using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.Biome;

public class DemonScythe : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Shade Sickle");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 48;
		((ModProjectile)this).projectile.height = 48;
		((ModProjectile)this).projectile.aiStyle = -1;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.tileCollide = true;
		((ModProjectile)this).projectile.timeLeft = 120;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		target.immune[((ModProjectile)this).projectile.owner] = 10;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.rotation += 0.4f;
	}

	public override void Kill(int timeLeft)
	{
		int num = 25;
		for (int i = 0; i < num; i++)
		{
			Vector2 vector = (Vector2.One * new Vector2((float)((ModProjectile)this).projectile.width / 5f, ((ModProjectile)this).projectile.height) * 0.75f * 0.5f).RotatedBy((float)(i - (num / 2 - 1)) * ((float)Math.PI * 2f) / (float)num) + ((ModProjectile)this).projectile.Center;
			Vector2 vector2 = vector - ((ModProjectile)this).projectile.Center;
			Dust obj = Main.dust[Dust.NewDust(vector + vector2, 0, 0, 89, vector2.X * 2f, vector2.Y * 2f, 100, default(Color), 1.4f)];
			obj.noGravity = true;
			obj.noLight = false;
			obj.velocity = Vector2.Normalize(vector2) * 3f;
			obj.fadeIn = 1.3f;
			((ModProjectile)this).projectile.ai[0] = 1f;
		}
	}
}
