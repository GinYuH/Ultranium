using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowWorm.Projectiles;

public class ErebusTentacle : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Erebus Tentacle");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 38;
		((ModProjectile)this).projectile.height = 55;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.alpha = 255;
		((ModProjectile)this).projectile.penetrate = -1;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.rotation = (float)Math.Atan2(((ModProjectile)this).projectile.velocity.Y, ((ModProjectile)this).projectile.velocity.X) + 1.57f;
		if (((ModProjectile)this).projectile.localAI[0] != 0f)
		{
			((ModProjectile)this).projectile.position -= ((ModProjectile)this).projectile.velocity * 1f;
		}
		((ModProjectile)this).projectile.localAI[0] += 1f;
		if (((ModProjectile)this).projectile.ai[0] == 0f)
		{
			((ModProjectile)this).projectile.alpha -= (int)((ModProjectile)this).projectile.localAI[1];
			if (((ModProjectile)this).projectile.alpha > 0)
			{
				return;
			}
			((ModProjectile)this).projectile.alpha = 0;
			((ModProjectile)this).projectile.ai[0] = 1f;
			if (((ModProjectile)this).projectile.ai[1] == 0f)
			{
				((ModProjectile)this).projectile.ai[1] += 1f;
				((ModProjectile)this).projectile.position += ((ModProjectile)this).projectile.velocity * 1f;
			}
			if (Main.myPlayer == ((ModProjectile)this).projectile.owner)
			{
				int num = ((ModProjectile)this).projectile.type;
				float num2 = 1f;
				if (((ModProjectile)this).projectile.ai[1] >= 60f + (float)Main.rand.Next(0, 6))
				{
					num = ((ModProjectile)this).mod.ProjectileType("ErebusTentacleTip");
					num2 = 1.4f;
				}
				int num3 = Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X + ((ModProjectile)this).projectile.velocity.X * num2, ((ModProjectile)this).projectile.Center.Y + ((ModProjectile)this).projectile.velocity.Y * num2, ((ModProjectile)this).projectile.velocity.X, ((ModProjectile)this).projectile.velocity.Y, num, ((ModProjectile)this).projectile.damage, ((ModProjectile)this).projectile.knockBack, ((ModProjectile)this).projectile.owner, 0f, ((ModProjectile)this).projectile.ai[1] + 1f);
				NetMessage.SendData(27, -1, -1, null, num3);
				Main.projectile[num3].localAI[1] = ((ModProjectile)this).projectile.localAI[1];
			}
			return;
		}
		((ModProjectile)this).projectile.ai[0] += 1f;
		if (((ModProjectile)this).projectile.ai[0] > 40f)
		{
			((ModProjectile)this).projectile.alpha += 15;
			if (((ModProjectile)this).projectile.alpha >= 255)
			{
				((ModProjectile)this).projectile.Kill();
			}
		}
	}
}
