using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.ShadowEvent;

public class EldritchScythe : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		// DisplayName.SetDefault("Soul Harvester");
	}

	public override void SetDefaults()
	{
		Projectile.tileCollide = false;
		Projectile.width = 76;
		Projectile.height = 66;
		Projectile.aiStyle = 0;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.penetrate = 1;
		Projectile.timeLeft = 300;
		Projectile.light = 0f;
		Projectile.extraUpdates = 1;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.Request<Texture2D>("Ultranium/Projectiles/Erebus/ShadowEvent/EldritchScytheTrail").Width() * 0.5f, (float)Projectile.height * 0.5f);
		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 position = Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, Projectile.gfxOffY);
			Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Ultranium/Projectiles/Erebus/ShadowEvent/EldritchScytheTrail").Value, position, null, color, Projectile.rotation, vector, Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		Projectile.rotation += 0.1f * (float)Projectile.direction;
	}

	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item20, new Vector2(Projectile.position.X, Projectile.position.Y));
		for (int i = 0; i < 5; i++)
		{
			Vector2 vector = ((float)Math.PI * 2f / 5f * (float)i).ToRotationVector2();
			vector.Normalize();
			vector *= 6f;
			Projectile.NewProjectile(null, Projectile.Center.X, Projectile.Center.Y, vector.X, vector.Y, Mod.Find<ModProjectile>("ShadeTentacle").Type, 200, 1f, Main.myPlayer, 0f, 0f);
		}
	}
}
