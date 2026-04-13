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
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		DisplayName.SetDefault("Tooth Ball");
	}

	public override void SetDefaults()
	{
		Projectile.scale = 1f;
		Projectile.width = 56;
		Projectile.height = 60;
		Projectile.friendly = false;
		Projectile.hostile = true;
		Projectile.aiStyle = 0;
		Projectile.penetrate = 1;
		Projectile.extraUpdates = 1;
		Projectile.timeLeft = 2000;
		Projectile.tileCollide = true;
	}

	public override void OnKill(int timeLeft)
	{
		int num = (Main.expertMode ? 25 : 45);
		for (int i = 0; i < 5; i++)
		{
			Vector2 vector = ((float)Math.PI * 2f / 5f * (float)i).ToRotationVector2();
			vector.Normalize();
			vector *= 6f;
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, vector.X, vector.Y, Mod.Find<ModProjectile>("DreadTooth").Type, num, 1f, Main.myPlayer, 0f, 0f);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, (float)Projectile.height * 0.5f);
		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 position = Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, Projectile.gfxOffY);
			Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
			Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, position, null, color, Projectile.rotation, vector, Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		Projectile.spriteDirection = ((Projectile.velocity.X > 0f) ? 1 : (-1));
		if (Projectile.spriteDirection == 1)
		{
			Projectile.rotation += 0.05f;
		}
		if (Projectile.spriteDirection == -1)
		{
			Projectile.rotation += -0.05f;
		}
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] >= 100f)
		{
			Projectile.velocity.Y = Projectile.velocity.Y + 0.15f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		bounceInt--;
		if (bounceInt <= 0)
		{
			Projectile.Kill();
		}
		else
		{
			if (Projectile.velocity.X != oldVelocity.X)
			{
				Projectile.velocity.X = (0f - oldVelocity.X) * 0.85f;
			}
			if (Projectile.velocity.Y != oldVelocity.Y)
			{
				Projectile.velocity.Y = (0f - oldVelocity.Y) * 0.85f;
			}
		}
		return false;
	}
}
