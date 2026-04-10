using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.SmallErebus;

public class SmolErebusTail : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Erebus Minion");
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 32;
		((ModProjectile)this).Projectile.height = 22;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.hide = true;
		((ModProjectile)this).Projectile.alpha = 255;
		((ModProjectile)this).Projectile.netImportant = true;
		((ModProjectile)this).Projectile.timeLeft = 18000;
		ProjectileID.Sets.MinionSacrificable[((ModProjectile)this).Projectile.type] = true;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.timeLeft *= 5;
		((ModProjectile)this).Projectile.minion = true;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		drawCacheProjsBehindProjectiles.Add(index);
	}

	public override void AI()
	{
		Player player = Main.player[((ModProjectile)this).Projectile.owner];
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if ((int)Main.time % 120 == 0)
		{
			((ModProjectile)this).Projectile.netUpdate = true;
		}
		if (!((Entity)player).active)
		{
			((Entity)((ModProjectile)this).Projectile).active = false;
			return;
		}
		int num = 30;
		if (player.dead)
		{
			modPlayer.ErebusMinion = false;
		}
		if (modPlayer.ErebusMinion)
		{
			((ModProjectile)this).Projectile.timeLeft = 2;
		}
		Vector2 zero = Vector2.Zero;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 1f;
		if (((ModProjectile)this).Projectile.ai[1] == 1f)
		{
			((ModProjectile)this).Projectile.ai[1] = 0f;
			((ModProjectile)this).Projectile.netUpdate = true;
		}
		int num5 = (int)((ModProjectile)this).Projectile.ai[0];
		if (num5 >= 0 && ((Entity)Main.projectile[num5]).active)
		{
			zero = Main.projectile[num5].Center;
			_ = Main.projectile[num5].velocity;
			num2 = Main.projectile[num5].rotation;
			num4 = MathHelper.Clamp(Main.projectile[num5].scale, 0f, 50f);
			num3 = 16f;
			Main.projectile[num5].localAI[0] = ((ModProjectile)this).Projectile.localAI[0] + 1f;
			((ModProjectile)this).Projectile.alpha -= 42;
			if (((ModProjectile)this).Projectile.alpha < 0)
			{
				((ModProjectile)this).Projectile.alpha = 0;
			}
			((ModProjectile)this).Projectile.velocity = Vector2.Zero;
			Vector2 vector = zero - ((ModProjectile)this).Projectile.Center;
			if (num2 != ((ModProjectile)this).Projectile.rotation)
			{
				float num6 = MathHelper.WrapAngle(num2 - ((ModProjectile)this).Projectile.rotation);
				vector = vector.RotatedBy(num6 * 0.1f);
			}
			((ModProjectile)this).Projectile.rotation = vector.ToRotation() + (float)Math.PI / 2f;
			((ModProjectile)this).Projectile.position = ((ModProjectile)this).Projectile.Center;
			((ModProjectile)this).Projectile.scale = num4;
			((ModProjectile)this).Projectile.width = (((ModProjectile)this).Projectile.height = (int)((float)num * ((ModProjectile)this).Projectile.scale));
			((ModProjectile)this).Projectile.Center = ((ModProjectile)this).Projectile.position;
			if (vector != Vector2.Zero)
			{
				((ModProjectile)this).Projectile.Center = zero - Vector2.Normalize(vector) * num3 * num4;
			}
			((ModProjectile)this).Projectile.spriteDirection = ((vector.X > 0f) ? 1 : (-1));
		}
		else
		{
			((ModProjectile)this).Projectile.Kill();
		}
	}
}
