using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ethereal.Clones;

public class EtherealWave : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Ethereal Wave");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 12;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 60;
		((ModProjectile)this).projectile.height = 28;
		((ModProjectile)this).projectile.aiStyle = -1;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.penetrate = 5;
		((ModProjectile)this).projectile.timeLeft = 120;
		((ModProjectile)this).projectile.extraUpdates = 1;
		((ModProjectile)this).projectile.alpha = 155;
		((ModProjectile)this).projectile.tileCollide = false;
	}

	public override void OnHitPlayer(Player player, int damage, bool crit)
	{
		player.AddBuff(((ModProjectile)this).mod.BuffType("DarkDebuff"), 180, fromNetPvP: true);
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
		((ModProjectile)this).projectile.rotation = ((ModProjectile)this).projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		((ModProjectile)this).projectile.rotation += 0f * (float)((ModProjectile)this).projectile.direction;
	}

	public override void Kill(int timeLeft)
	{
		((Entity)((ModProjectile)this).projectile).active = false;
		((ModProjectile)this).projectile.position.X = ((ModProjectile)this).projectile.position.X + (float)(((ModProjectile)this).projectile.width / 2);
		((ModProjectile)this).projectile.position.Y = ((ModProjectile)this).projectile.position.Y + (float)(((ModProjectile)this).projectile.height / 2);
		((ModProjectile)this).projectile.width = 30;
		((ModProjectile)this).projectile.height = 30;
		((ModProjectile)this).projectile.position.X = ((ModProjectile)this).projectile.position.X - (float)(((ModProjectile)this).projectile.width / 2);
		((ModProjectile)this).projectile.position.Y = ((ModProjectile)this).projectile.position.Y - (float)(((ModProjectile)this).projectile.height / 2);
		for (int i = 0; i < 20; i++)
		{
			int num = Dust.NewDust(new Vector2(((ModProjectile)this).projectile.position.X, ((ModProjectile)this).projectile.position.Y), ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("ShadowDustPurple"), 0f, 0f, 100, default(Color), 2f);
			Main.dust[num].velocity *= 2f;
			if (Main.rand.Next(2) == 0)
			{
				Main.dust[num].scale = 0.5f;
				Main.dust[num].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
	}
}
