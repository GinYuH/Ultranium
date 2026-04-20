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
        //DisplayName.SetDefault("Erebus Minion");
        ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
    }

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void SetDefaults()
	{
		Projectile.width = 32;
		Projectile.height = 22;
		Projectile.friendly = true;
		Projectile.ignoreWater = true;
		Projectile.hide = true;
		Projectile.alpha = 255;
		Projectile.netImportant = true;
		Projectile.timeLeft = 18000;
		Projectile.penetrate = -1;
		Projectile.tileCollide = false;
		Projectile.timeLeft *= 5;
		Projectile.minion = true;
		Projectile.DamageType = DamageClass.Summon;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindProjectiles.Add(index);
	}

	public override void AI()
	{
		Player player = Main.player[Projectile.owner];
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if ((int)Main.time % 120 == 0)
		{
			Projectile.netUpdate = true;
		}
		if (!player.active)
		{
			((Entity)Projectile).active = false;
			return;
		}
		int num = 30;
		if (player.dead)
		{
			modPlayer.ErebusMinion = false;
		}
		if (modPlayer.ErebusMinion)
		{
			Projectile.timeLeft = 2;
		}
		Vector2 zero = Vector2.Zero;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 1f;
		if (Projectile.ai[1] == 1f)
		{
			Projectile.ai[1] = 0f;
			Projectile.netUpdate = true;
		}
		int num5 = (int)Projectile.ai[0];
		if (num5 >= 0 && ((Entity)Main.projectile[num5]).active)
		{
			zero = Main.projectile[num5].Center;
			_ = Main.projectile[num5].velocity;
			num2 = Main.projectile[num5].rotation;
			num4 = MathHelper.Clamp(Main.projectile[num5].scale, 0f, 50f);
			num3 = 16f;
			Main.projectile[num5].localAI[0] = Projectile.localAI[0] + 1f;
			Projectile.alpha -= 42;
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			Projectile.velocity = Vector2.Zero;
			Vector2 vector = zero - Projectile.Center;
			if (num2 != Projectile.rotation)
			{
				float num6 = MathHelper.WrapAngle(num2 - Projectile.rotation);
				vector = vector.RotatedBy(num6 * 0.1f);
			}
			Projectile.rotation = vector.ToRotation() + (float)Math.PI / 2f;
			Projectile.position = Projectile.Center;
			Projectile.scale = num4;
			Projectile.width = (Projectile.height = (int)((float)num * Projectile.scale));
			Projectile.Center = Projectile.position;
			if (vector != Vector2.Zero)
			{
				Projectile.Center = zero - Vector2.Normalize(vector) * num3 * num4;
			}
			Projectile.spriteDirection = ((vector.X > 0f) ? 1 : (-1));
		}
		else
		{
			Projectile.Kill();
		}
	}
}
