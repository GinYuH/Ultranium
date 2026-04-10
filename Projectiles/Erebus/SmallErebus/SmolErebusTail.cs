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
		((ModProjectile)this).DisplayName.SetDefault("Erebus Minion");
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 32;
		((ModProjectile)this).projectile.height = 22;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.hide = true;
		((ModProjectile)this).projectile.alpha = 255;
		((ModProjectile)this).projectile.netImportant = true;
		((ModProjectile)this).projectile.timeLeft = 18000;
		ProjectileID.Sets.MinionSacrificable[((ModProjectile)this).projectile.type] = true;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.timeLeft *= 5;
		((ModProjectile)this).projectile.minion = true;
	}

	public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
	{
		drawCacheProjsBehindProjectiles.Add(index);
	}

	public override void AI()
	{
		Player player = Main.player[((ModProjectile)this).projectile.owner];
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if ((int)Main.time % 120 == 0)
		{
			((ModProjectile)this).projectile.netUpdate = true;
		}
		if (!((Entity)player).active)
		{
			((Entity)((ModProjectile)this).projectile).active = false;
			return;
		}
		int num = 30;
		if (player.dead)
		{
			modPlayer.ErebusMinion = false;
		}
		if (modPlayer.ErebusMinion)
		{
			((ModProjectile)this).projectile.timeLeft = 2;
		}
		Vector2 zero = Vector2.Zero;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 1f;
		if (((ModProjectile)this).projectile.ai[1] == 1f)
		{
			((ModProjectile)this).projectile.ai[1] = 0f;
			((ModProjectile)this).projectile.netUpdate = true;
		}
		int num5 = (int)((ModProjectile)this).projectile.ai[0];
		if (num5 >= 0 && ((Entity)Main.projectile[num5]).active)
		{
			zero = Main.projectile[num5].Center;
			_ = Main.projectile[num5].velocity;
			num2 = Main.projectile[num5].rotation;
			num4 = MathHelper.Clamp(Main.projectile[num5].scale, 0f, 50f);
			num3 = 16f;
			Main.projectile[num5].localAI[0] = ((ModProjectile)this).projectile.localAI[0] + 1f;
			((ModProjectile)this).projectile.alpha -= 42;
			if (((ModProjectile)this).projectile.alpha < 0)
			{
				((ModProjectile)this).projectile.alpha = 0;
			}
			((ModProjectile)this).projectile.velocity = Vector2.Zero;
			Vector2 vector = zero - ((ModProjectile)this).projectile.Center;
			if (num2 != ((ModProjectile)this).projectile.rotation)
			{
				float num6 = MathHelper.WrapAngle(num2 - ((ModProjectile)this).projectile.rotation);
				vector = vector.RotatedBy(num6 * 0.1f);
			}
			((ModProjectile)this).projectile.rotation = vector.ToRotation() + (float)Math.PI / 2f;
			((ModProjectile)this).projectile.position = ((ModProjectile)this).projectile.Center;
			((ModProjectile)this).projectile.scale = num4;
			((ModProjectile)this).projectile.width = (((ModProjectile)this).projectile.height = (int)((float)num * ((ModProjectile)this).projectile.scale));
			((ModProjectile)this).projectile.Center = ((ModProjectile)this).projectile.position;
			if (vector != Vector2.Zero)
			{
				((ModProjectile)this).projectile.Center = zero - Vector2.Normalize(vector) * num3 * num4;
			}
			((ModProjectile)this).projectile.spriteDirection = ((vector.X > 0f) ? 1 : (-1));
		}
		else
		{
			((ModProjectile)this).projectile.Kill();
		}
	}
}
