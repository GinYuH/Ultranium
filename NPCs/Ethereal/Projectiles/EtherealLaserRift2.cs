using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ethereal.Projectiles;

public class EtherealLaserRift2 : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Ethereal Portal 2");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 55;
		((ModProjectile)this).projectile.height = 55;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.timeLeft = 480;
		((ModProjectile)this).projectile.alpha = 255;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.velocity *= 0f;
		if (MathHelper.Distance(((ModProjectile)this).projectile.velocity.X, 0f) < 0.1f && MathHelper.Distance(((ModProjectile)this).projectile.velocity.Y, 0f) < 0.1f)
		{
			((ModProjectile)this).projectile.velocity = Vector2.Zero;
		}
		((ModProjectile)this).projectile.ai[0] += 1f;
		Vector2 spinningpoint = new Vector2(10f, -10f);
		float num = -1f;
		((ModProjectile)this).projectile.ai[0] += 2f;
		float num2 = ((ModProjectile)this).projectile.ai[0] / num;
		if (((ModProjectile)this).projectile.ai[0] % 30f == 0f)
		{
			Main.PlaySound(SoundID.Item117, ((ModProjectile)this).projectile.position);
			float num3 = 2f;
			Vector2 vector = ((ModProjectile)this).projectile.Center + spinningpoint.RotatedBy(num2);
			float num4 = (float)Math.Atan2(((ModProjectile)this).projectile.Center.Y - vector.Y, ((ModProjectile)this).projectile.Center.X - vector.X);
			Vector2 vector2 = new Vector2((float)(Math.Cos(num4) * (double)num3 * -1.0), (float)(Math.Sin(num4) * (double)num3 * -1.0));
			Projectile.NewProjectile(((ModProjectile)this).projectile.Center + vector2 * 30f, vector2, ((ModProjectile)this).mod.ProjectileType("EtherealDeathray"), 55, 0f, Main.myPlayer, 0f, 0f);
		}
		if (NPC.AnyNPCs(((ModProjectile)this).mod.NPCType("XenanisClone")) || !NPC.AnyNPCs(((ModProjectile)this).mod.NPCType("Xenanis")))
		{
			((ModProjectile)this).projectile.Kill();
		}
		for (int i = 0; i < 20; i++)
		{
			Vector2 vector3 = default(Vector2);
			double num5 = Main.rand.NextDouble() * 2.0 * Math.PI;
			vector3.X += (float)(Math.Sin(num5) * 1200.0);
			vector3.Y += (float)(Math.Cos(num5) * 1200.0);
			Dust obj = Main.dust[Dust.NewDust(((ModProjectile)this).projectile.Center + vector3 - new Vector2(4f, 4f), 0, 0, 62, 0f, 0f, 100, Color.White)];
			obj.velocity *= 0f;
			obj.noGravity = true;
			obj.scale = 2.5f;
		}
		Player localPlayer = Main.LocalPlayer;
		if (((Entity)localPlayer).active && !localPlayer.dead && localPlayer.Distance(((ModProjectile)this).projectile.Center) > 1200f)
		{
			Vector2 vector4 = ((ModProjectile)this).projectile.Center - localPlayer.Center;
			float num6 = vector4.Length() - 600f;
			vector4.Normalize();
			vector4 *= ((num6 < 25f) ? num6 : 25f);
			localPlayer.position += vector4;
		}
	}
}
