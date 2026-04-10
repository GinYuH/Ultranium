using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Stellar;

public class StellarStar : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Stellar Star");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 10;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 30;
		((ModProjectile)this).Projectile.height = 30;
		((ModProjectile)this).Projectile.penetrate = 1;
		((ModProjectile)this).Projectile.hostile = false;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Melee;
		((ModProjectile)this).Projectile.alpha = 0;
		((ModProjectile)this).Projectile.timeLeft = 300;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/Projectiles/Stellar/StellarStarTrail").Width * 0.5f, (float)((ModProjectile)this).Projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).Projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).Projectile.gfxOffY);
			Color color = ((ModProjectile)this).Projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).Projectile.oldPos.Length - i) / (float)((ModProjectile)this).Projectile.oldPos.Length);
			spriteBatch.Draw(ModContent.GetTexture("Ultranium/Projectiles/Stellar/StellarStarTrail"), position, null, color, ((ModProjectile)this).Projectile.rotation, vector, ((ModProjectile)this).Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		float num = 5f;
		float num2 = 10f;
		((ModProjectile)this).Projectile.ai[0] += 1f;
		if (((ModProjectile)this).Projectile.ai[1] == 0f)
		{
			if (((ModProjectile)this).Projectile.ai[0] > num2 * 0.5f)
			{
				((ModProjectile)this).Projectile.ai[0] = 0f;
				((ModProjectile)this).Projectile.ai[1] = 1f;
			}
			else
			{
				Vector2 velocity = new Vector2(((ModProjectile)this).Projectile.velocity.X, ((ModProjectile)this).Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(0f - num));
				((ModProjectile)this).Projectile.velocity = velocity;
			}
		}
		else
		{
			if (((ModProjectile)this).Projectile.ai[0] <= num2)
			{
				Vector2 velocity2 = new Vector2(((ModProjectile)this).Projectile.velocity.X, ((ModProjectile)this).Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(num));
				((ModProjectile)this).Projectile.velocity = velocity2;
			}
			else
			{
				Vector2 velocity3 = new Vector2(((ModProjectile)this).Projectile.velocity.X, ((ModProjectile)this).Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(0f - num));
				((ModProjectile)this).Projectile.velocity = velocity3;
			}
			if (((ModProjectile)this).Projectile.ai[0] >= num2 * 2f)
			{
				((ModProjectile)this).Projectile.ai[0] = 0f;
			}
		}
		((ModProjectile)this).Projectile.velocity *= 1f;
		((ModProjectile)this).Projectile.rotation = (float)Math.Atan2(((ModProjectile)this).Projectile.velocity.Y, ((ModProjectile)this).Projectile.velocity.X) + 0f;
	}
}
