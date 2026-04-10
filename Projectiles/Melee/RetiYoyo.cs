using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Melee;

public class RetiYoyo : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Reti-Yoyo");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 4;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.melee = true;
		((ModProjectile)this).projectile.CloneDefaults(552);
		((ModProjectile)this).projectile.damage = 68;
		((ModProjectile)this).projectile.extraUpdates = 1;
		base.aiType = 552;
	}

	public override bool PreAI()
	{
		Player player = Main.player[((ModProjectile)this).projectile.owner];
		if (!player.CheckMana(player.inventory[player.selectedItem].mana, pay: true))
		{
			((ModProjectile)this).projectile.Kill();
		}
		return true;
	}

	public override void PostAI()
	{
		((ModProjectile)this).projectile.rotation -= 10f;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.frameCounter++;
		if (((ModProjectile)this).projectile.frameCounter < 65)
		{
			return;
		}
		((ModProjectile)this).projectile.frameCounter = 0;
		float num = 8000f;
		int num2 = -1;
		for (int i = 0; i < 200; i++)
		{
			float num3 = Vector2.Distance(((ModProjectile)this).projectile.Center, Main.npc[i].Center);
			if (num3 < num && num3 < 640f && Main.npc[i].CanBeChasedBy(((ModProjectile)this).projectile))
			{
				num2 = i;
				num = num3;
			}
		}
		if (num2 != -1 && Collision.CanHit(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, Main.npc[num2].position, Main.npc[num2].width, Main.npc[num2].height))
		{
			Vector2 vector = Main.npc[num2].Center - ((ModProjectile)this).projectile.Center;
			float num4 = 9f;
			float num5 = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (num5 > num4)
			{
				num5 = num4 / num5;
			}
			vector *= num5;
			int num6 = Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, vector.X, vector.Y, 100, 40, ((ModProjectile)this).projectile.knockBack / 2f, ((ModProjectile)this).projectile.owner, 0f, 0f);
			Main.projectile[num6].friendly = true;
			Main.projectile[num6].hostile = false;
		}
	}
}
