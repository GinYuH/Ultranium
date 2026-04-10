using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Magic;

public class LunaticStar : ModProjectile
{
	private float waveAI0;

	private float waveAI1;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		// DisplayName.SetDefault("Ancient Light");
	}

	public override void SetDefaults()
	{
		Projectile.width = 26;
		Projectile.height = 26;
		Projectile.penetrate = 1;
		Projectile.hostile = false;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.alpha = 0;
		Projectile.timeLeft = 300;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/Projectiles/Magic/LunaticStarTrail").Width * 0.5f, (float)Projectile.height * 0.5f);
		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 position = Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, Projectile.gfxOffY);
			Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
			Main.spriteBatch.Draw(ModContent.GetTexture("Ultranium/Projectiles/Magic/LunaticStarTrail"), position, null, color, Projectile.rotation, vector, Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		float num = 2f;
		float num2 = 18f;
		waveAI0 += 1f;
		if (waveAI1 == 0f)
		{
			if (waveAI0 > num2 * 0.5f)
			{
				waveAI0 = 0f;
				waveAI1 = 1f;
			}
			else
			{
				Vector2 velocity = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(0f - num));
				Projectile.velocity = velocity;
			}
		}
		else
		{
			if (waveAI0 <= num2)
			{
				Vector2 velocity2 = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(num));
				Projectile.velocity = velocity2;
			}
			else
			{
				Vector2 velocity3 = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(0f - num));
				Projectile.velocity = velocity3;
			}
			if (waveAI0 >= num2 * 2f)
			{
				waveAI0 = 0f;
			}
		}
		Projectile.velocity *= 1f;
		Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 0f;
	}
}
