using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Mushroom;

public class MushroomBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("MushroomBolt Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.alpha = 255;
		((ModProjectile)this).projectile.width = 16;
		((ModProjectile)this).projectile.height = 16;
		((ModProjectile)this).projectile.hostile = false;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.magic = true;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.timeLeft = 120;
		((ModProjectile)this).projectile.extraUpdates = 3;
		((ModProjectile)this).projectile.tileCollide = true;
	}

	public override void AI()
	{
		int num = Dust.NewDust(new Vector2(((ModProjectile)this).projectile.position.X, ((ModProjectile)this).projectile.position.Y), ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 41, ((ModProjectile)this).projectile.velocity.X * 1.2f, ((ModProjectile)this).projectile.velocity.Y * 1.2f, 0, default(Color), 3.75f);
		Main.dust[num].velocity *= 0.3f;
		Main.dust[num].scale *= 0.3f;
		Main.dust[num].noGravity = false;
	}
}
