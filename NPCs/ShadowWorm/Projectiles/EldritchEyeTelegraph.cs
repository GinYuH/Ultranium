using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowWorm.Projectiles;

public class EldritchEyeTelegraph : ModProjectile
{
	private int Timer;

	public override string Texture => "Ultranium/NPCs/ShadowWorm/Projectiles/ShadowFlameBreath";

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 136;
		((ModProjectile)this).Projectile.height = 136;
		((ModProjectile)this).Projectile.hostile = false;
		((ModProjectile)this).Projectile.friendly = false;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.tileCollide = true;
		((ModProjectile)this).Projectile.penetrate = 2;
		((ModProjectile)this).Projectile.timeLeft = 260;
		((ModProjectile)this).Projectile.alpha = 255;
	}

	public override void AI()
	{
		bool expertMode = Main.expertMode;
		((ModProjectile)this).Projectile.velocity *= 0f;
		Timer++;
		if (Timer < 120)
		{
			int num = 12;
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = (Vector2.One * new Vector2((float)((ModProjectile)this).Projectile.width / 5f, ((ModProjectile)this).Projectile.height) * 0.75f * 0.5f).RotatedBy((float)(i - (num / 2 - 1)) * ((float)Math.PI * 2f) / (float)num) + ((ModProjectile)this).Projectile.Center;
				Vector2 vector2 = vector - ((ModProjectile)this).Projectile.Center;
				Dust obj = Main.dust[Dust.NewDust(vector + vector2, 0, 0, 89, vector2.X * 2f, vector2.Y * 2f, 100, default(Color), 1.4f)];
				obj.noGravity = true;
				obj.noLight = false;
				obj.velocity = Vector2.Normalize(vector2) * 3f;
				obj.fadeIn = 1.3f;
			}
		}
		if (Timer >= 120)
		{
			int num2 = (expertMode ? 35 : 48);
			Projectile.NewProjectile(null, ((ModProjectile)this).Projectile.Center.X, ((ModProjectile)this).Projectile.Center.Y, 0f, 0f, ((ModProjectile)this).Mod.Find<ModProjectile>("EldritchEye").Type, num2, 1f, Main.myPlayer, 0f, 0f);
			((Entity)((ModProjectile)this).Projectile).active = false;
		}
	}
}
