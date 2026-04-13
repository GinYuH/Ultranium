using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ethereal.Projectiles;

public class EtherealLaserRift2 : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ethereal Portal 2");
	}

	public override void SetDefaults()
	{
		Projectile.width = 55;
		Projectile.height = 55;
		Projectile.penetrate = -1;
		Projectile.hostile = true;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 480;
		Projectile.alpha = 255;
	}

	public override void AI()
	{
		Projectile.velocity *= 0f;
		if (MathHelper.Distance(Projectile.velocity.X, 0f) < 0.1f && MathHelper.Distance(Projectile.velocity.Y, 0f) < 0.1f)
		{
			Projectile.velocity = Vector2.Zero;
		}
		Projectile.ai[0] += 1f;
		Vector2 spinningpoint = new Vector2(10f, -10f);
		float num = -1f;
		Projectile.ai[0] += 2f;
		float num2 = Projectile.ai[0] / num;
		if (Projectile.ai[0] % 30f == 0f)
		{
			SoundEngine.PlaySound(SoundID.Item117, Projectile.position);
			float num3 = 2f;
			Vector2 vector = Projectile.Center + spinningpoint.RotatedBy(num2);
			float num4 = (float)Math.Atan2(Projectile.Center.Y - vector.Y, Projectile.Center.X - vector.X);
			Vector2 vector2 = new Vector2((float)(Math.Cos(num4) * (double)num3 * -1.0), (float)(Math.Sin(num4) * (double)num3 * -1.0));
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + vector2 * 30f, vector2, Mod.Find<ModProjectile>("EtherealDeathray").Type, 55, 0f, Main.myPlayer, 0f, 0f);
		}
		if (NPC.AnyNPCs(Mod.Find<ModNPC>("XenanisClone").Type) || !NPC.AnyNPCs(Mod.Find<ModNPC>("Xenanis").Type))
		{
			Projectile.Kill();
		}
		for (int i = 0; i < 20; i++)
		{
			Vector2 vector3 = default(Vector2);
			double num5 = Main.rand.NextDouble() * 2.0 * Math.PI;
			vector3.X += (float)(Math.Sin(num5) * 1200.0);
			vector3.Y += (float)(Math.Cos(num5) * 1200.0);
			Dust obj = Main.dust[Dust.NewDust(Projectile.Center + vector3 - new Vector2(4f, 4f), 0, 0, DustID.PurpleTorch, 0f, 0f, 100, Color.White)];
			obj.velocity *= 0f;
			obj.noGravity = true;
			obj.scale = 2.5f;
		}
		Player localPlayer = Main.LocalPlayer;
		if (((Entity)localPlayer).active && !localPlayer.dead && localPlayer.Distance(Projectile.Center) > 1200f)
		{
			Vector2 vector4 = Projectile.Center - localPlayer.Center;
			float num6 = vector4.Length() - 600f;
			vector4.Normalize();
			vector4 *= ((num6 < 25f) ? num6 : 25f);
			localPlayer.position += vector4;
		}
	}
}
