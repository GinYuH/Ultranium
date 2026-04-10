using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
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
		// ((ModProjectile)this).DisplayName.SetDefault("Dread Orb");
		Main.projFrames[((ModProjectile)this).Projectile.type] = 4;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.scale = 1f;
		((ModProjectile)this).Projectile.width = 50;
		((ModProjectile)this).Projectile.height = 48;
		((ModProjectile)this).Projectile.friendly = false;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.aiStyle = 0;
		((ModProjectile)this).Projectile.penetrate = 1;
		((ModProjectile)this).Projectile.extraUpdates = 1;
		((ModProjectile)this).Projectile.timeLeft = 360;
		((ModProjectile)this).Projectile.tileCollide = false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		player.AddBuff(((ModProjectile)this).Mod.Find<ModBuff>("DreadDebuff").Type, 180, fromNetPvP: true);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		float amount = (float)(Main.GameUpdateCount % 60) / 60f;
		int num = (int)(Main.GameUpdateCount / 60 % 2);
		return Color.Lerp(ColorCycle[num], ColorCycle[(num + 1) % 2], amount);
	}

	public override void AI()
	{
		if (++((ModProjectile)this).Projectile.frameCounter >= 16)
		{
			((ModProjectile)this).Projectile.frameCounter = 0;
			if (++((ModProjectile)this).Projectile.frame >= 4)
			{
				((ModProjectile)this).Projectile.frame = 0;
			}
		}
		timer++;
		if (timer <= 300)
		{
			((ModProjectile)this).Projectile.scale += 0.02f;
		}
		if (timer >= 240)
		{
			int num = 25;
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = (Vector2.One * new Vector2((float)((ModProjectile)this).Projectile.width / 7f, (float)((ModProjectile)this).Projectile.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(i - (num / 2 - 1)) * ((float)Math.PI * 2f) / (float)num) + ((ModProjectile)this).Projectile.Center;
				Vector2 vector2 = vector - ((ModProjectile)this).Projectile.Center;
				Dust obj = Main.dust[Dust.NewDust(vector + vector2, 0, 0, 90, vector2.X * 2f, vector2.Y * 2f, 100, default(Color), 1.4f)];
				obj.noGravity = true;
				obj.noLight = false;
				obj.velocity = Vector2.Normalize(vector2) * 10f;
				obj.fadeIn = 1.3f;
			}
		}
		if (timer >= 300)
		{
			((ModProjectile)this).Projectile.Kill();
			Ultranium.seizureAmount = 20f;
		}
		if (((ModProjectile)this).Projectile.ai[0] == 0f && Main.netMode != 1)
		{
			target = -1;
			float num2 = 2000f;
			for (int j = 0; j < 255; j++)
			{
				if (((Entity)Main.player[j]).active && !Main.player[j].dead)
				{
					float num3 = Vector2.Distance(Main.player[j].Center, ((ModProjectile)this).Projectile.Center);
					if (num3 < num2 || target == -1)
					{
						num2 = num3;
						target = j;
					}
				}
			}
			if (target != -1)
			{
				((ModProjectile)this).Projectile.ai[0] = 1f;
				((ModProjectile)this).Projectile.netUpdate = true;
			}
		}
		else
		{
			Player player = Main.player[target];
			if (!((Entity)player).active || player.dead)
			{
				target = -1;
				((ModProjectile)this).Projectile.ai[0] = 0f;
				((ModProjectile)this).Projectile.netUpdate = true;
			}
			else
			{
				float num4 = ((ModProjectile)this).Projectile.velocity.ToRotation();
				Vector2 vector3 = player.Center - ((ModProjectile)this).Projectile.Center;
				float targetAngle = vector3.ToRotation();
				if (vector3 == Vector2.Zero)
				{
					targetAngle = num4;
				}
				float num5 = num4.AngleLerp(targetAngle, 0.1f);
				((ModProjectile)this).Projectile.velocity = new Vector2(((ModProjectile)this).Projectile.velocity.Length(), 0f).RotatedBy(num5);
			}
		}
		((ModProjectile)this).Projectile.rotation = ((ModProjectile)this).Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		((ModProjectile)this).Projectile.rotation += 0f * (float)((ModProjectile)this).Projectile.direction;
	}

	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item14, new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y));
		for (int i = 0; i < 100; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 90, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = false;
			Main.dust[num].scale = 3.5f;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModProjectile)this).Projectile.Center)
			{
				Main.dust[num].velocity = ((ModProjectile)this).Projectile.DirectionTo(Main.dust[num].position) * 10f;
			}
		}
		for (int j = 0; j < 30; j++)
		{
			Vector2 vector = ((float)Math.PI / 15f * (float)j).ToRotationVector2();
			vector.Normalize();
			vector *= 8f;
			Projectile.NewProjectile(null, ((ModProjectile)this).Projectile.Center.X, ((ModProjectile)this).Projectile.Center.Y, vector.X, vector.Y, ((ModProjectile)this).Mod.Find<ModProjectile>("DreadFireBlast").Type, ((ModProjectile)this).Projectile.damage, 1f, Main.myPlayer, 0f, 0f);
		}
	}
}
