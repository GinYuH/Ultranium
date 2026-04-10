using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Dread.Projectiles;

public class ToothBall : ModProjectile
{
	private int bounceInt = 3;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
		((ModProjectile)this).DisplayName.SetDefault("Tooth Ball");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.scale = 1f;
		((ModProjectile)this).projectile.width = 56;
		((ModProjectile)this).projectile.height = 60;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.aiStyle = 0;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.extraUpdates = 1;
		((ModProjectile)this).projectile.timeLeft = 2000;
		((ModProjectile)this).projectile.tileCollide = true;
	}

	public override void Kill(int timeLeft)
	{
		int num = (Main.expertMode ? 25 : 45);
		for (int i = 0; i < 5; i++)
		{
			Vector2 vector = ((float)Math.PI * 2f / 5f * (float)i).ToRotationVector2();
			vector.Normalize();
			vector *= 6f;
			Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, vector.X, vector.Y, ((ModProjectile)this).mod.ProjectileType("DreadTooth"), num, 1f, Main.myPlayer, 0f, 0f);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Vector2 vector = new Vector2((float)Main.projectileTexture[((ModProjectile)this).projectile.type].Width * 0.5f, (float)((ModProjectile)this).projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).projectile.gfxOffY);
			Color color = ((ModProjectile)this).projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).projectile.oldPos.Length - i) / (float)((ModProjectile)this).projectile.oldPos.Length);
			spriteBatch.Draw(Main.projectileTexture[((ModProjectile)this).projectile.type], position, null, color, ((ModProjectile)this).projectile.rotation, vector, ((ModProjectile)this).projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.spriteDirection = ((((ModProjectile)this).projectile.velocity.X > 0f) ? 1 : (-1));
		if (((ModProjectile)this).projectile.spriteDirection == 1)
		{
			((ModProjectile)this).projectile.rotation += 0.05f;
		}
		if (((ModProjectile)this).projectile.spriteDirection == -1)
		{
			((ModProjectile)this).projectile.rotation += -0.05f;
		}
		((ModProjectile)this).projectile.ai[0] += 1f;
		if (((ModProjectile)this).projectile.ai[0] >= 100f)
		{
			((ModProjectile)this).projectile.velocity.Y = ((ModProjectile)this).projectile.velocity.Y + 0.15f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		bounceInt--;
		if (bounceInt <= 0)
		{
			((ModProjectile)this).projectile.Kill();
		}
		else
		{
			if (((ModProjectile)this).projectile.velocity.X != oldVelocity.X)
			{
				((ModProjectile)this).projectile.velocity.X = (0f - oldVelocity.X) * 0.85f;
			}
			if (((ModProjectile)this).projectile.velocity.Y != oldVelocity.Y)
			{
				((ModProjectile)this).projectile.velocity.Y = (0f - oldVelocity.Y) * 0.85f;
			}
		}
		return false;
	}
}
