using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent.Projectiles;

public class PhantomWave : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Phantom Wave");
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		Projectile.width = 60;
		Projectile.height = 28;
		Projectile.aiStyle = -1;
		Projectile.hostile = true;
		Projectile.friendly = false;
		Projectile.tileCollide = false;
		Projectile.penetrate = 5;
		Projectile.timeLeft = 120;
		Projectile.extraUpdates = 1;
		Projectile.alpha = 155;
		Projectile.tileCollide = false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(Mod.Find<ModBuff>("DarkDebuff").Type, 180, quiet: false);
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
		Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Projectile.rotation += 0f * (float)Projectile.direction;
	}

	public override void OnKill(int timeLeft)
	{
		((Entity)Projectile).active = false;
		Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
		Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
		Projectile.width = 30;
		Projectile.height = 30;
		Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
		Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
		for (int i = 0; i < 20; i++)
		{
			int num = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, Mod.Find<ModDust>("ShadowDustPurple").Type, 0f, 0f, 100, default(Color), 2f);
			Main.dust[num].velocity *= 2f;
			if (Main.rand.Next(2) == 0)
			{
				Main.dust[num].scale = 0.5f;
				Main.dust[num].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
	}
}
