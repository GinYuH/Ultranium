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
		//DisplayName.SetDefault("Stellar Star");
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		Projectile.width = 30;
		Projectile.height = 30;
		Projectile.penetrate = 1;
		Projectile.hostile = false;
		Projectile.friendly = true;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.alpha = 0;
		Projectile.timeLeft = 300;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.Request<Texture2D>("Ultranium/Projectiles/Stellar/StellarStarTrail").Width() * 0.5f, (float)Projectile.height * 0.5f);
		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 position = Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, Projectile.gfxOffY);
			Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Ultranium/Projectiles/Stellar/StellarStarTrail").Value, position, null, color, Projectile.rotation, vector, Projectile.scale, SpriteEffects.None, 0f);
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
		Projectile.ai[0] += 1f;
		if (Projectile.ai[1] == 0f)
		{
			if (Projectile.ai[0] > num2 * 0.5f)
			{
				Projectile.ai[0] = 0f;
				Projectile.ai[1] = 1f;
			}
			else
			{
				Vector2 velocity = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(0f - num));
				Projectile.velocity = velocity;
			}
		}
		else
		{
			if (Projectile.ai[0] <= num2)
			{
				Vector2 velocity2 = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(num));
				Projectile.velocity = velocity2;
			}
			else
			{
				Vector2 velocity3 = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(0f - num));
				Projectile.velocity = velocity3;
			}
			if (Projectile.ai[0] >= num2 * 2f)
			{
				Projectile.ai[0] = 0f;
			}
		}
		Projectile.velocity *= 1f;
		Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 0f;
	}
}
