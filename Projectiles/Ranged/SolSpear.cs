using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ranged;

public class SolSpear : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
		((ModProjectile)this).DisplayName.SetDefault("Spear of the Sol");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 20;
		((ModProjectile)this).projectile.height = 28;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ranged = true;
		((ModProjectile)this).projectile.tileCollide = true;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.timeLeft = 180;
		((ModProjectile)this).projectile.extraUpdates = 1;
		((ModProjectile)this).projectile.ignoreWater = false;
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
		if (Utils.NextBool(Main.rand))
		{
			Dust dust = Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 228);
			dust.noGravity = true;
			dust.scale = 1.6f;
		}
		((ModProjectile)this).projectile.rotation = ((ModProjectile)this).projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		((ModProjectile)this).projectile.rotation += 0f * (float)((ModProjectile)this).projectile.direction;
		Lighting.AddLight(((ModProjectile)this).projectile.Center, 0.8f, 0.7f, 0.2f);
	}

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		target.immune[((ModProjectile)this).projectile.owner] = 10;
	}

	public override void Kill(int timeLeft)
	{
		Main.PlaySound(2, (int)((ModProjectile)this).projectile.position.X, (int)((ModProjectile)this).projectile.position.Y, 10, 1f, 0f);
		for (int i = 0; i < 5; i++)
		{
			Dust.NewDust(((ModProjectile)this).projectile.position + ((ModProjectile)this).projectile.velocity, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 25, ((ModProjectile)this).projectile.oldVelocity.X * 0.5f, ((ModProjectile)this).projectile.oldVelocity.Y * 0.5f);
		}
		for (int j = 0; j < 3; j++)
		{
			Vector2 spinningpoint = new Vector2(0f, -1f);
			float num = Main.rand.NextFloat() * 6.283f;
			spinningpoint = spinningpoint.RotatedBy(num);
			spinningpoint *= 5f;
			Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, spinningpoint.X, spinningpoint.Y, ((ModProjectile)this).mod.ProjectileType("SolSparks"), ((ModProjectile)this).projectile.damage, 0f, Main.myPlayer, 0f, 0f);
		}
	}
}
