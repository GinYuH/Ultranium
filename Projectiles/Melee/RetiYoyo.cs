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
		// DisplayName.SetDefault("Reti-Yoyo");
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		Projectile.DamageType = DamageClass.Melee;
		Projectile.CloneDefaults(552);
		Projectile.damage = 68;
		Projectile.extraUpdates = 1;
		base.AIType = 552;
	}

	public override bool PreAI()
	{
		Player player = Main.player[Projectile.owner];
		if (!player.CheckMana(player.inventory[player.selectedItem].mana, pay: true))
		{
			Projectile.Kill();
		}
		return true;
	}

	public override void PostAI()
	{
		Projectile.rotation -= 10f;
	}

	public override void AI()
	{
		Projectile.frameCounter++;
		if (Projectile.frameCounter < 65)
		{
			return;
		}
		Projectile.frameCounter = 0;
		float num = 8000f;
		int num2 = -1;
		for (int i = 0; i < 200; i++)
		{
			float num3 = Vector2.Distance(Projectile.Center, Main.npc[i].Center);
			if (num3 < num && num3 < 640f && Main.npc[i].CanBeChasedBy(Projectile))
			{
				num2 = i;
				num = num3;
			}
		}
		if (num2 != -1 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[num2].position, Main.npc[num2].width, Main.npc[num2].height))
		{
			Vector2 vector = Main.npc[num2].Center - Projectile.Center;
			float num4 = 9f;
			float num5 = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (num5 > num4)
			{
				num5 = num4 / num5;
			}
			vector *= num5;
			int num6 = Projectile.NewProjectile(null, Projectile.Center.X, Projectile.Center.Y, vector.X, vector.Y, 100, 40, Projectile.knockBack / 2f, Projectile.owner, 0f, 0f);
			Main.projectile[num6].friendly = true;
			Main.projectile[num6].hostile = false;
		}
	}
}
