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
		((ModProjectile)this).DisplayName.SetDefault("The Inferno");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 4;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.melee = true;
		((ModProjectile)this).projectile.CloneDefaults(552);
		((ModProjectile)this).projectile.damage = 27;
		((ModProjectile)this).projectile.extraUpdates = 1;
		base.aiType = 545;
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

	public override void AI()
	{
		((ModProjectile)this).projectile.frameCounter++;
		if (((ModProjectile)this).projectile.frameCounter >= 130)
		{
			((ModProjectile)this).projectile.frameCounter = 0;
			float num = (float)((double)Main.rand.Next(0, 361) * (Math.PI / 180.0));
			Vector2 vector = new Vector2((float)Math.Cos(num), (float)Math.Sin(num));
			int num2 = Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, vector.X, vector.Y, 15, ((ModProjectile)this).projectile.damage, (float)((ModProjectile)this).projectile.owner, 0, 0f, 0f);
			Main.projectile[num2].friendly = true;
			Main.projectile[num2].hostile = false;
			Main.projectile[num2].velocity *= 7f;
		}
	}
}
