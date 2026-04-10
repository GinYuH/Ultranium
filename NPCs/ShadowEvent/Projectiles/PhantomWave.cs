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
		// ((ModProjectile)this).DisplayName.SetDefault("Phantom Wave");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 60;
		((ModProjectile)this).Projectile.height = 28;
		((ModProjectile)this).Projectile.aiStyle = -1;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.friendly = false;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.penetrate = 5;
		((ModProjectile)this).Projectile.timeLeft = 120;
		((ModProjectile)this).Projectile.extraUpdates = 1;
		((ModProjectile)this).Projectile.alpha = 155;
		((ModProjectile)this).Projectile.tileCollide = false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		player.AddBuff(((ModProjectile)this).Mod.Find<ModBuff>("DarkDebuff").Type, 180, fromNetPvP: true);
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
		((ModProjectile)this).Projectile.rotation = ((ModProjectile)this).Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		((ModProjectile)this).Projectile.rotation += 0f * (float)((ModProjectile)this).Projectile.direction;
	}

	public override void OnKill(int timeLeft)
	{
		((Entity)((ModProjectile)this).Projectile).active = false;
		((ModProjectile)this).Projectile.position.X = ((ModProjectile)this).Projectile.position.X + (float)(((ModProjectile)this).Projectile.width / 2);
		((ModProjectile)this).Projectile.position.Y = ((ModProjectile)this).Projectile.position.Y + (float)(((ModProjectile)this).Projectile.height / 2);
		((ModProjectile)this).Projectile.width = 30;
		((ModProjectile)this).Projectile.height = 30;
		((ModProjectile)this).Projectile.position.X = ((ModProjectile)this).Projectile.position.X - (float)(((ModProjectile)this).Projectile.width / 2);
		((ModProjectile)this).Projectile.position.Y = ((ModProjectile)this).Projectile.position.Y - (float)(((ModProjectile)this).Projectile.height / 2);
		for (int i = 0; i < 20; i++)
		{
			int num = Dust.NewDust(new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y), ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, ((ModProjectile)this).Mod.Find<ModDust>("ShadowDustPurple").Type, 0f, 0f, 100, default(Color), 2f);
			Main.dust[num].velocity *= 2f;
			if (Main.rand.Next(2) == 0)
			{
				Main.dust[num].scale = 0.5f;
				Main.dust[num].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
	}
}
