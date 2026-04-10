using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
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
		((ModProjectile)this).DisplayName.SetDefault("Cosmos Star");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 10;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 72;
		((ModProjectile)this).projectile.height = 72;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.alpha = 0;
		((ModProjectile)this).projectile.timeLeft = 300;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		float amount = (float)(Main.GameUpdateCount % 60) / 60f;
		int num = (int)(Main.GameUpdateCount / 60 % 2);
		return Color.Lerp(ColorCycle[num], ColorCycle[(num + 1) % 2], amount);
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
		((ModProjectile)this).projectile.rotation = ((ModProjectile)this).projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		((ModProjectile)this).projectile.rotation += 0f * (float)((ModProjectile)this).projectile.direction;
		timer++;
		if (timer <= 180)
		{
			((ModProjectile)this).projectile.scale += 0.01f;
		}
		if (timer >= 180)
		{
			((ModProjectile)this).projectile.Kill();
			Ultranium.seizureAmount = 15f;
		}
		if (timer <= 60)
		{
			return;
		}
		if (((ModProjectile)this).projectile.ai[0] == 0f && Main.netMode != 1)
		{
			target = -1;
			float num = 2000f;
			for (int i = 0; i < 255; i++)
			{
				if (((Entity)Main.player[i]).active && !Main.player[i].dead)
				{
					float num2 = Vector2.Distance(Main.player[i].Center, ((ModProjectile)this).projectile.Center);
					if (num2 < num || target == -1)
					{
						num = num2;
						target = i;
					}
				}
			}
			if (target != -1)
			{
				((ModProjectile)this).projectile.ai[0] = 1f;
				((ModProjectile)this).projectile.netUpdate = true;
			}
			return;
		}
		Player player = Main.player[target];
		if (!((Entity)player).active || player.dead)
		{
			target = -1;
			((ModProjectile)this).projectile.ai[0] = 0f;
			((ModProjectile)this).projectile.netUpdate = true;
			return;
		}
		float num3 = ((ModProjectile)this).projectile.velocity.ToRotation();
		Vector2 vector = player.Center - ((ModProjectile)this).projectile.Center;
		float targetAngle = vector.ToRotation();
		if (vector == Vector2.Zero)
		{
			targetAngle = num3;
		}
		float num4 = num3.AngleLerp(targetAngle, 0.1f);
		((ModProjectile)this).projectile.velocity = new Vector2(((ModProjectile)this).projectile.velocity.Length(), 0f).RotatedBy(num4);
	}

	public override void Kill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 62, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModProjectile)this).projectile.Center)
			{
				Main.dust[num].velocity = ((ModProjectile)this).projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
		float num2 = 9f;
		float num3 = 21f;
		float num4 = MathHelper.ToRadians(360f);
		int num5 = -1;
		for (int j = 0; (float)j < num3; j++)
		{
			Vector2 vector = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num4, num4, (float)j / (num3 - 1f))) * num2;
			Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, vector.X, vector.Y, ((ModProjectile)this).mod.ProjectileType("CosmicBlastSpiral"), ((ModProjectile)this).projectile.damage, 2f, Main.myPlayer, (float)num5, 0f);
		}
	}
}
