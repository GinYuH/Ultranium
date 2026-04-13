using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Aldin.Projectiles;

public class HomingStar : ModProjectile
{
	public int timer;

	private int target;

	private Color[] ColorCycle = new Color[2]
	{
		new Color(117, 235, 215),
		new Color(62, 30, 152)
	};

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Cosmos Star");
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		Projectile.width = 72;
		Projectile.height = 72;
		Projectile.penetrate = 1;
		Projectile.hostile = true;
		Projectile.friendly = false;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.alpha = 0;
		Projectile.timeLeft = 300;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		float amount = (float)(Main.GameUpdateCount % 60) / 60f;
		int num = (int)(Main.GameUpdateCount / 60 % 2);
		return Color.Lerp(ColorCycle[num], ColorCycle[(num + 1) % 2], amount);
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
		Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Projectile.rotation += 0f * (float)Projectile.direction;
		timer++;
		if (timer <= 180)
		{
			Projectile.scale += 0.01f;
		}
		if (timer >= 180)
		{
			Projectile.Kill();
			Ultranium.seizureAmount = 15f;
		}
		if (timer <= 60)
		{
			return;
		}
		if (Projectile.ai[0] == 0f && Main.netMode != 1)
		{
			target = -1;
			float num = 2000f;
			for (int i = 0; i < 255; i++)
			{
				if (((Entity)Main.player[i]).active && !Main.player[i].dead)
				{
					float num2 = Vector2.Distance(Main.player[i].Center, Projectile.Center);
					if (num2 < num || target == -1)
					{
						num = num2;
						target = i;
					}
				}
			}
			if (target != -1)
			{
				Projectile.ai[0] = 1f;
				Projectile.netUpdate = true;
			}
			return;
		}
		Player player = Main.player[target];
		if (!((Entity)player).active || player.dead)
		{
			target = -1;
			Projectile.ai[0] = 0f;
			Projectile.netUpdate = true;
			return;
		}
		float num3 = Projectile.velocity.ToRotation();
		Vector2 vector = player.Center - Projectile.Center;
		float targetAngle = vector.ToRotation();
		if (vector == Vector2.Zero)
		{
			targetAngle = num3;
		}
		float num4 = num3.AngleLerp(targetAngle, 0.1f);
		Projectile.velocity = new Vector2(Projectile.velocity.Length(), 0f).RotatedBy(num4);
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 62, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != Projectile.Center)
			{
				Main.dust[num].velocity = Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
		float num2 = 9f;
		float num3 = 21f;
		float num4 = MathHelper.ToRadians(360f);
		int num5 = -1;
		for (int j = 0; (float)j < num3; j++)
		{
			Vector2 vector = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num4, num4, (float)j / (num3 - 1f))) * num2;
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, vector.X, vector.Y, Mod.Find<ModProjectile>("CosmicBlastSpiral").Type, Projectile.damage, 2f, Main.myPlayer, (float)num5, 0f);
		}
	}
}
