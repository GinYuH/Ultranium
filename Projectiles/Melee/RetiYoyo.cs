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
		// ((ModProjectile)this).DisplayName.SetDefault("Reti-Yoyo");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 4;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.DamageType = DamageClass.Melee;
		((ModProjectile)this).Projectile.CloneDefaults(552);
		((ModProjectile)this).Projectile.damage = 68;
		((ModProjectile)this).Projectile.extraUpdates = 1;
		base.AIType = 552;
	}

	public override bool PreAI()
	{
		Player player = Main.player[((ModProjectile)this).Projectile.owner];
		if (!player.CheckMana(player.inventory[player.selectedItem].mana, pay: true))
		{
			((ModProjectile)this).Projectile.Kill();
		}
		return true;
	}

	public override void PostAI()
	{
		((ModProjectile)this).Projectile.rotation -= 10f;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.frameCounter++;
		if (((ModProjectile)this).Projectile.frameCounter < 65)
		{
			return;
		}
		((ModProjectile)this).Projectile.frameCounter = 0;
		float num = 8000f;
		int num2 = -1;
		for (int i = 0; i < 200; i++)
		{
			float num3 = Vector2.Distance(((ModProjectile)this).Projectile.Center, Main.npc[i].Center);
			if (num3 < num && num3 < 640f && Main.npc[i].CanBeChasedBy(((ModProjectile)this).Projectile))
			{
				num2 = i;
				num = num3;
			}
		}
		if (num2 != -1 && Collision.CanHit(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, Main.npc[num2].position, Main.npc[num2].width, Main.npc[num2].height))
		{
			Vector2 vector = Main.npc[num2].Center - ((ModProjectile)this).Projectile.Center;
			float num4 = 9f;
			float num5 = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (num5 > num4)
			{
				num5 = num4 / num5;
			}
			vector *= num5;
			int num6 = Projectile.NewProjectile(((ModProjectile)this).Projectile.Center.X, ((ModProjectile)this).Projectile.Center.Y, vector.X, vector.Y, 100, 40, ((ModProjectile)this).Projectile.knockBack / 2f, ((ModProjectile)this).Projectile.owner, 0f, 0f);
			Main.projectile[num6].friendly = true;
			Main.projectile[num6].hostile = false;
		}
	}
}
