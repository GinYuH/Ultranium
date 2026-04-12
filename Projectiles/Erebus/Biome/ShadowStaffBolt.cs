using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.Biome;

public class ShadowStaffBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Shadow Bolt");
	}

	public override void SetDefaults()
	{
		Projectile.alpha = 255;
		Projectile.width = 16;
		Projectile.height = 16;
		Projectile.hostile = false;
		Projectile.friendly = true;
		Projectile.ignoreWater = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.penetrate = 1;
		Projectile.timeLeft = 120;
		Projectile.extraUpdates = 3;
		Projectile.tileCollide = true;
	}

	public override void AI()
	{
		int num = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, Mod.Find<ModDust>("ShadowDustPurple").Type, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 0, default(Color), 3.75f);
		Main.dust[num].velocity *= 0.5f;
		Main.dust[num].scale *= 0.5f;
		Main.dust[num].noGravity = true;
	}
}
