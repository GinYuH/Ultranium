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
		((ModProjectile)this).DisplayName.SetDefault("Water Helix");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 8;
		((ModProjectile)this).projectile.height = 8;
		((ModProjectile)this).projectile.alpha = 255;
		((ModProjectile)this).projectile.timeLeft = 600;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.tileCollide = true;
		((ModProjectile)this).projectile.ignoreWater = false;
		((ModProjectile)this).projectile.penetrate = 4;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/Ocean/Projectiles/WaterHelixTrail").Width * 0.5f, (float)((ModProjectile)this).projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).projectile.gfxOffY);
			Color color = ((ModProjectile)this).projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).projectile.oldPos.Length - i) / (float)((ModProjectile)this).projectile.oldPos.Length);
			spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/Ocean/Projectiles/WaterHelixTrail"), position, null, color, ((ModProjectile)this).projectile.rotation, vector, ((ModProjectile)this).projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.rotation = ((ModProjectile)this).projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		((ModProjectile)this).projectile.rotation += 0f * (float)((ModProjectile)this).projectile.direction;
		((ModProjectile)this).projectile.localAI[0] += 0.075f;
		if (((ModProjectile)this).projectile.localAI[0] > 8f)
		{
			((ModProjectile)this).projectile.localAI[0] = 8f;
		}
		float num = ((ModProjectile)this).projectile.localAI[0];
		float num2 = 16f;
		((ModProjectile)this).projectile.ai[0] += 1f;
		if (((ModProjectile)this).projectile.ai[1] == 0f)
		{
			if (((ModProjectile)this).projectile.ai[0] > num2 * 0.5f)
			{
				((ModProjectile)this).projectile.ai[0] = 0f;
				((ModProjectile)this).projectile.ai[1] = 1f;
			}
			else
			{
				Vector2 velocity = new Vector2(((ModProjectile)this).projectile.velocity.X, ((ModProjectile)this).projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(0f - num));
				((ModProjectile)this).projectile.velocity = velocity;
			}
			return;
		}
		if (((ModProjectile)this).projectile.ai[0] <= num2)
		{
			Vector2 velocity2 = new Vector2(((ModProjectile)this).projectile.velocity.X, ((ModProjectile)this).projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(num));
			((ModProjectile)this).projectile.velocity = velocity2;
		}
		else
		{
			Vector2 velocity3 = new Vector2(((ModProjectile)this).projectile.velocity.X, ((ModProjectile)this).projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(0f - num));
			((ModProjectile)this).projectile.velocity = velocity3;
		}
		if (((ModProjectile)this).projectile.ai[0] >= num2 * 2f)
		{
			((ModProjectile)this).projectile.ai[0] = 0f;
		}
	}
}
