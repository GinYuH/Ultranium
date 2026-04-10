using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Melee;

public class Inferno : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("The Inferno");
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		Projectile.DamageType = DamageClass.Melee;
		Projectile.CloneDefaults(552);
		Projectile.damage = 27;
		Projectile.extraUpdates = 1;
		base.AIType = 545;
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

	public override void AI()
	{
		Projectile.frameCounter++;
		if (Projectile.frameCounter >= 130)
		{
			Projectile.frameCounter = 0;
			float num = (float)((double)Main.rand.Next(0, 361) * (Math.PI / 180.0));
			Vector2 vector = new Vector2((float)Math.Cos(num), (float)Math.Sin(num));
			int num2 = Projectile.NewProjectile(null, Projectile.Center.X, Projectile.Center.Y, vector.X, vector.Y, 15, Projectile.damage, (float)Projectile.owner, 0, 0f, 0f);
			Main.projectile[num2].friendly = true;
			Main.projectile[num2].hostile = false;
			Main.projectile[num2].velocity *= 7f;
		}
	}
}
