using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dread.TrueDread;

public class DreadDisc : ModProjectile
{
	private int ProjectileTimer;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		//DisplayName.SetDefault("Disc of Dismay");
	}

	public override void SetDefaults()
	{
		Projectile.width = 48;
		Projectile.height = 48;
		Projectile.aiStyle = ProjAIStyleID.Boomerang;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.tileCollide = false;
		Projectile.penetrate = 3;
		Projectile.timeLeft = 700;
		Projectile.extraUpdates = 1;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[Projectile.owner] = 2;
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
		ProjectileTimer++;
		if (ProjectileTimer >= 60)
		{
			int num = 4;
			int num2 = Main.rand.Next(0, 180);
			for (int i = 0; i < num; i++)
			{
				float num3 = MathHelper.ToRadians(360 / num * i + num2);
				Vector2 vector = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(num3);
				vector.Normalize();
				vector.X *= 3f;
				vector.Y *= 3f;
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, vector.X, vector.Y, Mod.Find<ModProjectile>("DreadParticleBolt").Type, Projectile.damage, 2f, Projectile.owner, 0f, 0f);
			}
			ProjectileTimer = 0;
		}
		Vector2 position = Projectile.Center + Vector2.Normalize(Projectile.velocity) * 10f;
		Dust obj = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemRuby)];
		obj.position = position;
		obj.velocity = Projectile.velocity.RotatedBy(Math.PI / 2.0) * 0.33f + Projectile.velocity / 4f;
		obj.position += Projectile.velocity.RotatedBy(Math.PI / 2.0);
		obj.fadeIn = 0.5f;
		obj.noGravity = true;
		Dust obj2 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemRuby)];
		obj2.position = position;
		obj2.velocity = Projectile.velocity.RotatedBy(-Math.PI / 2.0) * 0.33f + Projectile.velocity / 4f;
		obj2.position += Projectile.velocity.RotatedBy(-Math.PI / 2.0);
		obj2.fadeIn = 0.5f;
		obj2.noGravity = true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}
}
