using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ranged;

public class SolSpear : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
		// ((ModProjectile)this).DisplayName.SetDefault("Spear of the Sol");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 20;
		((ModProjectile)this).Projectile.height = 28;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Ranged;
		((ModProjectile)this).Projectile.tileCollide = true;
		((ModProjectile)this).Projectile.penetrate = 1;
		((ModProjectile)this).Projectile.timeLeft = 180;
		((ModProjectile)this).Projectile.extraUpdates = 1;
		((ModProjectile)this).Projectile.ignoreWater = false;
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
		if (Utils.NextBool(Main.rand))
		{
			Dust dust = Dust.NewDustDirect(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 228);
			dust.noGravity = true;
			dust.scale = 1.6f;
		}
		((ModProjectile)this).Projectile.rotation = ((ModProjectile)this).Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		((ModProjectile)this).Projectile.rotation += 0f * (float)((ModProjectile)this).Projectile.direction;
		Lighting.AddLight(((ModProjectile)this).Projectile.Center, 0.8f, 0.7f, 0.2f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[((ModProjectile)this).Projectile.owner] = 10;
	}

	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item10, new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y));
		for (int i = 0; i < 5; i++)
		{
			Dust.NewDust(((ModProjectile)this).Projectile.position + ((ModProjectile)this).Projectile.velocity, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 25, ((ModProjectile)this).Projectile.oldVelocity.X * 0.5f, ((ModProjectile)this).Projectile.oldVelocity.Y * 0.5f);
		}
		for (int j = 0; j < 3; j++)
		{
			Vector2 spinningpoint = new Vector2(0f, -1f);
			float num = Main.rand.NextFloat() * 6.283f;
			spinningpoint = spinningpoint.RotatedBy(num);
			spinningpoint *= 5f;
			Projectile.NewProjectile(((ModProjectile)this).Projectile.Center.X, ((ModProjectile)this).Projectile.Center.Y, spinningpoint.X, spinningpoint.Y, ((ModProjectile)this).Mod.Find<ModProjectile>("SolSparks").Type, ((ModProjectile)this).Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
		}
	}
}
