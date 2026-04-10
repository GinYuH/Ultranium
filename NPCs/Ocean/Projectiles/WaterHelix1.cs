using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ocean.Projectiles;

public class WaterHelix1 : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Water Helix");
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		Projectile.width = 8;
		Projectile.height = 8;
		Projectile.alpha = 255;
		Projectile.timeLeft = 600;
		Projectile.friendly = false;
		Projectile.hostile = true;
		Projectile.tileCollide = true;
		Projectile.ignoreWater = false;
		Projectile.penetrate = 4;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/Ocean/Projectiles/WaterHelixTrail").Width * 0.5f, (float)Projectile.height * 0.5f);
		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 position = Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, Projectile.gfxOffY);
			Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
			spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/Ocean/Projectiles/WaterHelixTrail"), position, null, color, Projectile.rotation, vector, Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Projectile.rotation += 0f * (float)Projectile.direction;
		Projectile.localAI[0] += 0.075f;
		if (Projectile.localAI[0] > 8f)
		{
			Projectile.localAI[0] = 8f;
		}
		float num = Projectile.localAI[0];
		float num2 = 16f;
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
			return;
		}
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
}
