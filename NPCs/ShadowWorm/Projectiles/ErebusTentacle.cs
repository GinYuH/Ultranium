using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowWorm.Projectiles;

public class ErebusTentacle : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Erebus Tentacle");
	}

	public override void SetDefaults()
	{
		Projectile.width = 38;
		Projectile.height = 55;
		Projectile.tileCollide = false;
		Projectile.hostile = true;
		Projectile.alpha = 255;
		Projectile.penetrate = -1;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
		if (Projectile.localAI[0] != 0f)
		{
			Projectile.position -= Projectile.velocity * 1f;
		}
		Projectile.localAI[0] += 1f;
		if (Projectile.ai[0] == 0f)
		{
			Projectile.alpha -= (int)Projectile.localAI[1];
			if (Projectile.alpha > 0)
			{
				return;
			}
			Projectile.alpha = 0;
			Projectile.ai[0] = 1f;
			if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] += 1f;
				Projectile.position += Projectile.velocity * 1f;
			}
			if (Main.myPlayer == Projectile.owner)
			{
				int num = Projectile.type;
				float num2 = 1f;
				if (Projectile.ai[1] >= 60f + (float)Main.rand.Next(0, 6))
				{
					num = Mod.Find<ModProjectile>("ErebusTentacleTip").Type;
					num2 = 1.4f;
				}
				int num3 = Projectile.NewProjectile(null, Projectile.Center.X + Projectile.velocity.X * num2, Projectile.Center.Y + Projectile.velocity.Y * num2, Projectile.velocity.X, Projectile.velocity.Y, num, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, Projectile.ai[1] + 1f);
				NetMessage.SendData(27, -1, -1, null, num3);
				Main.projectile[num3].localAI[1] = Projectile.localAI[1];
			}
			return;
		}
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] > 40f)
		{
			Projectile.alpha += 15;
			if (Projectile.alpha >= 255)
			{
				Projectile.Kill();
			}
		}
	}
}
