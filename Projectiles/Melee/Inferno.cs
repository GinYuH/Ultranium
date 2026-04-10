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
		// ((ModProjectile)this).DisplayName.SetDefault("The Inferno");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 4;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.DamageType = DamageClass.Melee;
		((ModProjectile)this).Projectile.CloneDefaults(552);
		((ModProjectile)this).Projectile.damage = 27;
		((ModProjectile)this).Projectile.extraUpdates = 1;
		base.AIType = 545;
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

	public override void AI()
	{
		((ModProjectile)this).Projectile.frameCounter++;
		if (((ModProjectile)this).Projectile.frameCounter >= 130)
		{
			((ModProjectile)this).Projectile.frameCounter = 0;
			float num = (float)((double)Main.rand.Next(0, 361) * (Math.PI / 180.0));
			Vector2 vector = new Vector2((float)Math.Cos(num), (float)Math.Sin(num));
			int num2 = Projectile.NewProjectile(((ModProjectile)this).Projectile.Center.X, ((ModProjectile)this).Projectile.Center.Y, vector.X, vector.Y, 15, ((ModProjectile)this).Projectile.damage, (float)((ModProjectile)this).Projectile.owner, 0, 0f, 0f);
			Main.projectile[num2].friendly = true;
			Main.projectile[num2].hostile = false;
			Main.projectile[num2].velocity *= 7f;
		}
	}
}
