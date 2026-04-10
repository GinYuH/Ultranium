using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.TrueDread.Projectiles;

public class BigToothBall : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		// DisplayName.SetDefault("Tooth Ball");
	}

	public override void SetDefaults()
	{
		Projectile.scale = 1f;
		Projectile.width = 70;
		Projectile.height = 70;
		Projectile.friendly = false;
		Projectile.hostile = true;
		Projectile.aiStyle = 0;
		Projectile.penetrate = 1;
		Projectile.extraUpdates = 1;
		Projectile.timeLeft = 300;
		Projectile.tileCollide = true;
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 9; i++)
		{
			Vector2 vector = ((float)Math.PI * 2f / 9f * (float)i).ToRotationVector2();
			vector.Normalize();
			vector *= 6f;
			Projectile.NewProjectile(null, Projectile.Center.X, Projectile.Center.Y, vector.X, vector.Y, Mod.Find<ModProjectile>("BigDreadTooth").Type, 50, 1f, Main.myPlayer, 0f, 0f);
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
		Projectile.rotation += 0.1f * (float)Projectile.direction;
	}
}
