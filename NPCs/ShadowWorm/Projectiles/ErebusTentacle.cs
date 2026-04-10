using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowWorm.Projectiles;

public class ErebusTentacle : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Erebus Tentacle");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 38;
		((ModProjectile)this).Projectile.height = 55;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.alpha = 255;
		((ModProjectile)this).Projectile.penetrate = -1;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.rotation = (float)Math.Atan2(((ModProjectile)this).Projectile.velocity.Y, ((ModProjectile)this).Projectile.velocity.X) + 1.57f;
		if (((ModProjectile)this).Projectile.localAI[0] != 0f)
		{
			((ModProjectile)this).Projectile.position -= ((ModProjectile)this).Projectile.velocity * 1f;
		}
		((ModProjectile)this).Projectile.localAI[0] += 1f;
		if (((ModProjectile)this).Projectile.ai[0] == 0f)
		{
			((ModProjectile)this).Projectile.alpha -= (int)((ModProjectile)this).Projectile.localAI[1];
			if (((ModProjectile)this).Projectile.alpha > 0)
			{
				return;
			}
			((ModProjectile)this).Projectile.alpha = 0;
			((ModProjectile)this).Projectile.ai[0] = 1f;
			if (((ModProjectile)this).Projectile.ai[1] == 0f)
			{
				((ModProjectile)this).Projectile.ai[1] += 1f;
				((ModProjectile)this).Projectile.position += ((ModProjectile)this).Projectile.velocity * 1f;
			}
			if (Main.myPlayer == ((ModProjectile)this).Projectile.owner)
			{
				int num = ((ModProjectile)this).Projectile.type;
				float num2 = 1f;
				if (((ModProjectile)this).Projectile.ai[1] >= 60f + (float)Main.rand.Next(0, 6))
				{
					num = ((ModProjectile)this).Mod.Find<ModProjectile>("ErebusTentacleTip").Type;
					num2 = 1.4f;
				}
				int num3 = Projectile.NewProjectile(((ModProjectile)this).Projectile.Center.X + ((ModProjectile)this).Projectile.velocity.X * num2, ((ModProjectile)this).Projectile.Center.Y + ((ModProjectile)this).Projectile.velocity.Y * num2, ((ModProjectile)this).Projectile.velocity.X, ((ModProjectile)this).Projectile.velocity.Y, num, ((ModProjectile)this).Projectile.damage, ((ModProjectile)this).Projectile.knockBack, ((ModProjectile)this).Projectile.owner, 0f, ((ModProjectile)this).Projectile.ai[1] + 1f);
				NetMessage.SendData(27, -1, -1, null, num3);
				Main.projectile[num3].localAI[1] = ((ModProjectile)this).Projectile.localAI[1];
			}
			return;
		}
		((ModProjectile)this).Projectile.ai[0] += 1f;
		if (((ModProjectile)this).Projectile.ai[0] > 40f)
		{
			((ModProjectile)this).Projectile.alpha += 15;
			if (((ModProjectile)this).Projectile.alpha >= 255)
			{
				((ModProjectile)this).Projectile.Kill();
			}
		}
	}
}
