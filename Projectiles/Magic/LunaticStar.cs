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
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 6;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
		((ModProjectile)this).DisplayName.SetDefault("Ancient Light");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 26;
		((ModProjectile)this).projectile.height = 26;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.hostile = false;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.magic = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.alpha = 0;
		((ModProjectile)this).projectile.timeLeft = 300;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/Projectiles/Magic/LunaticStarTrail").Width * 0.5f, (float)((ModProjectile)this).projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).projectile.gfxOffY);
			Color color = ((ModProjectile)this).projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).projectile.oldPos.Length - i) / (float)((ModProjectile)this).projectile.oldPos.Length);
			spriteBatch.Draw(ModContent.GetTexture("Ultranium/Projectiles/Magic/LunaticStarTrail"), position, null, color, ((ModProjectile)this).projectile.rotation, vector, ((ModProjectile)this).projectile.scale, SpriteEffects.None, 0f);
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
				Vector2 velocity = new Vector2(((ModProjectile)this).projectile.velocity.X, ((ModProjectile)this).projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(0f - num));
				((ModProjectile)this).projectile.velocity = velocity;
			}
		}
		else
		{
			if (waveAI0 <= num2)
			{
				Vector2 velocity2 = new Vector2(((ModProjectile)this).projectile.velocity.X, ((ModProjectile)this).projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(num));
				((ModProjectile)this).projectile.velocity = velocity2;
			}
			else
			{
				Vector2 velocity3 = new Vector2(((ModProjectile)this).projectile.velocity.X, ((ModProjectile)this).projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(0f - num));
				((ModProjectile)this).projectile.velocity = velocity3;
			}
			if (waveAI0 >= num2 * 2f)
			{
				waveAI0 = 0f;
			}
		}
		((ModProjectile)this).projectile.velocity *= 1f;
		((ModProjectile)this).projectile.rotation = (float)Math.Atan2(((ModProjectile)this).projectile.velocity.Y, ((ModProjectile)this).projectile.velocity.X) + 0f;
	}
}
