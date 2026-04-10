using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.TrueDread.Projectiles;

public class GiantDreadOrb : ModProjectile
{
	public int timer;

	private int target;

	private Color[] ColorCycle = new Color[2]
	{
		new Color(200, 0, 0),
		new Color(124, 7, 31)
	};

	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Dread Orb");
		Main.projFrames[((ModProjectile)this).projectile.type] = 4;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.scale = 1f;
		((ModProjectile)this).projectile.width = 50;
		((ModProjectile)this).projectile.height = 48;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.aiStyle = 0;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.extraUpdates = 1;
		((ModProjectile)this).projectile.timeLeft = 360;
		((ModProjectile)this).projectile.tileCollide = false;
	}

	public override void OnHitPlayer(Player player, int damage, bool crit)
	{
		player.AddBuff(((ModProjectile)this).mod.BuffType("DreadDebuff"), 180, fromNetPvP: true);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		float amount = (float)(Main.GameUpdateCount % 60) / 60f;
		int num = (int)(Main.GameUpdateCount / 60 % 2);
		return Color.Lerp(ColorCycle[num], ColorCycle[(num + 1) % 2], amount);
	}

	public override void AI()
	{
		if (++((ModProjectile)this).projectile.frameCounter >= 16)
		{
			((ModProjectile)this).projectile.frameCounter = 0;
			if (++((ModProjectile)this).projectile.frame >= 4)
			{
				((ModProjectile)this).projectile.frame = 0;
			}
		}
		timer++;
		if (timer <= 300)
		{
			((ModProjectile)this).projectile.scale += 0.02f;
		}
		if (timer >= 240)
		{
			int num = 25;
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = (Vector2.One * new Vector2((float)((ModProjectile)this).projectile.width / 7f, (float)((ModProjectile)this).projectile.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(i - (num / 2 - 1)) * ((float)Math.PI * 2f) / (float)num) + ((ModProjectile)this).projectile.Center;
				Vector2 vector2 = vector - ((ModProjectile)this).projectile.Center;
				Dust obj = Main.dust[Dust.NewDust(vector + vector2, 0, 0, 90, vector2.X * 2f, vector2.Y * 2f, 100, default(Color), 1.4f)];
				obj.noGravity = true;
				obj.noLight = false;
				obj.velocity = Vector2.Normalize(vector2) * 10f;
				obj.fadeIn = 1.3f;
			}
		}
		if (timer >= 300)
		{
			((ModProjectile)this).projectile.Kill();
			Ultranium.seizureAmount = 20f;
		}
		if (((ModProjectile)this).projectile.ai[0] == 0f && Main.netMode != 1)
		{
			target = -1;
			float num2 = 2000f;
			for (int j = 0; j < 255; j++)
			{
				if (((Entity)Main.player[j]).active && !Main.player[j].dead)
				{
					float num3 = Vector2.Distance(Main.player[j].Center, ((ModProjectile)this).projectile.Center);
					if (num3 < num2 || target == -1)
					{
						num2 = num3;
						target = j;
					}
				}
			}
			if (target != -1)
			{
				((ModProjectile)this).projectile.ai[0] = 1f;
				((ModProjectile)this).projectile.netUpdate = true;
			}
		}
		else
		{
			Player player = Main.player[target];
			if (!((Entity)player).active || player.dead)
			{
				target = -1;
				((ModProjectile)this).projectile.ai[0] = 0f;
				((ModProjectile)this).projectile.netUpdate = true;
			}
			else
			{
				float num4 = ((ModProjectile)this).projectile.velocity.ToRotation();
				Vector2 vector3 = player.Center - ((ModProjectile)this).projectile.Center;
				float targetAngle = vector3.ToRotation();
				if (vector3 == Vector2.Zero)
				{
					targetAngle = num4;
				}
				float num5 = num4.AngleLerp(targetAngle, 0.1f);
				((ModProjectile)this).projectile.velocity = new Vector2(((ModProjectile)this).projectile.velocity.Length(), 0f).RotatedBy(num5);
			}
		}
		((ModProjectile)this).projectile.rotation = ((ModProjectile)this).projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		((ModProjectile)this).projectile.rotation += 0f * (float)((ModProjectile)this).projectile.direction;
	}

	public override void Kill(int timeLeft)
	{
		Main.PlaySound(2, (int)((ModProjectile)this).projectile.position.X, (int)((ModProjectile)this).projectile.position.Y, 14, 1f, 0f);
		for (int i = 0; i < 100; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 90, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = false;
			Main.dust[num].scale = 3.5f;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModProjectile)this).projectile.Center)
			{
				Main.dust[num].velocity = ((ModProjectile)this).projectile.DirectionTo(Main.dust[num].position) * 10f;
			}
		}
		for (int j = 0; j < 30; j++)
		{
			Vector2 vector = ((float)Math.PI / 15f * (float)j).ToRotationVector2();
			vector.Normalize();
			vector *= 8f;
			Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, vector.X, vector.Y, ((ModProjectile)this).mod.ProjectileType("DreadFireBlast"), ((ModProjectile)this).projectile.damage, 1f, Main.myPlayer, 0f, 0f);
		}
	}
}
