using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ocean.Projectiles;

public class WaterHelix2 : ModProjectile
{
	public override string Texture => "Ultranium/NPCs/Ocean/Projectiles/WaterHelix1";

	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Water Helix");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 8;
		((ModProjectile)this).Projectile.height = 8;
		((ModProjectile)this).Projectile.alpha = 255;
		((ModProjectile)this).Projectile.timeLeft = 600;
		((ModProjectile)this).Projectile.friendly = false;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.tileCollide = true;
		((ModProjectile)this).Projectile.ignoreWater = false;
		((ModProjectile)this).Projectile.penetrate = 4;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/Ocean/Projectiles/WaterHelixTrail").Width * 0.5f, (float)((ModProjectile)this).Projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).Projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).Projectile.gfxOffY);
			Color color = ((ModProjectile)this).Projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).Projectile.oldPos.Length - i) / (float)((ModProjectile)this).Projectile.oldPos.Length);
			spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/Ocean/Projectiles/WaterHelixTrail"), position, null, color, ((ModProjectile)this).Projectile.rotation, vector, ((ModProjectile)this).Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.rotation = ((ModProjectile)this).Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		((ModProjectile)this).Projectile.rotation += 0f * (float)((ModProjectile)this).Projectile.direction;
		((ModProjectile)this).Projectile.localAI[0] += 0.075f;
		if (((ModProjectile)this).Projectile.localAI[0] > 8f)
		{
			((ModProjectile)this).Projectile.localAI[0] = 8f;
		}
		float num = ((ModProjectile)this).Projectile.localAI[0];
		float num2 = 16f;
		((ModProjectile)this).Projectile.ai[0] += 1f;
		if (((ModProjectile)this).Projectile.ai[1] == 0f)
		{
			if (((ModProjectile)this).Projectile.ai[0] <= num2)
			{
				Vector2 velocity = new Vector2(((ModProjectile)this).Projectile.velocity.X, ((ModProjectile)this).Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(num));
				((ModProjectile)this).Projectile.velocity = velocity;
			}
			else
			{
				Vector2 velocity2 = new Vector2(((ModProjectile)this).Projectile.velocity.X, ((ModProjectile)this).Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(0f - num));
				((ModProjectile)this).Projectile.velocity = velocity2;
			}
			if (((ModProjectile)this).Projectile.ai[0] >= num2 * 2f)
			{
				((ModProjectile)this).Projectile.ai[0] = 0f;
			}
		}
		else if (((ModProjectile)this).Projectile.ai[0] > num2 * 0.5f)
		{
			((ModProjectile)this).Projectile.ai[0] = 0f;
			((ModProjectile)this).Projectile.ai[1] = 1f;
		}
		else
		{
			Vector2 velocity3 = new Vector2(((ModProjectile)this).Projectile.velocity.X, ((ModProjectile)this).Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(0f - num));
			((ModProjectile)this).Projectile.velocity = velocity3;
		}
	}
}
