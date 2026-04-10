using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Dread.Projectiles;

public class ToothBall : ModProjectile
{
	private int bounceInt = 3;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
		// ((ModProjectile)this).DisplayName.SetDefault("Tooth Ball");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.scale = 1f;
		((ModProjectile)this).Projectile.width = 56;
		((ModProjectile)this).Projectile.height = 60;
		((ModProjectile)this).Projectile.friendly = false;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.aiStyle = 0;
		((ModProjectile)this).Projectile.penetrate = 1;
		((ModProjectile)this).Projectile.extraUpdates = 1;
		((ModProjectile)this).Projectile.timeLeft = 2000;
		((ModProjectile)this).Projectile.tileCollide = true;
	}

	public override void OnKill(int timeLeft)
	{
		int num = (Main.expertMode ? 25 : 45);
		for (int i = 0; i < 5; i++)
		{
			Vector2 vector = ((float)Math.PI * 2f / 5f * (float)i).ToRotationVector2();
			vector.Normalize();
			vector *= 6f;
			Projectile.NewProjectile(null, ((ModProjectile)this).Projectile.Center.X, ((ModProjectile)this).Projectile.Center.Y, vector.X, vector.Y, ((ModProjectile)this).Mod.Find<ModProjectile>("DreadTooth").Type, num, 1f, Main.myPlayer, 0f, 0f);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)TextureAssets.Projectile[((ModProjectile)this).Projectile.type].Value.Width * 0.5f, (float)((ModProjectile)this).Projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).Projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).Projectile.gfxOffY);
			Color color = ((ModProjectile)this).Projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).Projectile.oldPos.Length - i) / (float)((ModProjectile)this).Projectile.oldPos.Length);
			spriteBatch.Draw(TextureAssets.Projectile[((ModProjectile)this).Projectile.type].Value, position, null, color, ((ModProjectile)this).Projectile.rotation, vector, ((ModProjectile)this).Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.spriteDirection = ((((ModProjectile)this).Projectile.velocity.X > 0f) ? 1 : (-1));
		if (((ModProjectile)this).Projectile.spriteDirection == 1)
		{
			((ModProjectile)this).Projectile.rotation += 0.05f;
		}
		if (((ModProjectile)this).Projectile.spriteDirection == -1)
		{
			((ModProjectile)this).Projectile.rotation += -0.05f;
		}
		((ModProjectile)this).Projectile.ai[0] += 1f;
		if (((ModProjectile)this).Projectile.ai[0] >= 100f)
		{
			((ModProjectile)this).Projectile.velocity.Y = ((ModProjectile)this).Projectile.velocity.Y + 0.15f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		bounceInt--;
		if (bounceInt <= 0)
		{
			((ModProjectile)this).Projectile.Kill();
		}
		else
		{
			if (((ModProjectile)this).Projectile.velocity.X != oldVelocity.X)
			{
				((ModProjectile)this).Projectile.velocity.X = (0f - oldVelocity.X) * 0.85f;
			}
			if (((ModProjectile)this).Projectile.velocity.Y != oldVelocity.Y)
			{
				((ModProjectile)this).Projectile.velocity.Y = (0f - oldVelocity.Y) * 0.85f;
			}
		}
		return false;
	}
}
