using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ethereal;

public class WispBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Wisp Bolt");
	}

	public override void SetDefaults()
	{
		Projectile.scale = 0.01f;
		Projectile.width = 16;
		Projectile.height = 16;
		Projectile.hostile = false;
		Projectile.friendly = true;
		Projectile.ignoreWater = true;
		Projectile.minion = true;
		Projectile.penetrate = 1;
		Projectile.timeLeft = 120;
		Projectile.extraUpdates = 3;
		Projectile.tileCollide = true;
	}

	public override void AI()
	{
		Lighting.AddLight(Projectile.Center, (float)(255 - Projectile.alpha) * 0.15f / 255f, (float)(255 - Projectile.alpha) * 0.45f / 255f, (float)(255 - Projectile.alpha) * 0.05f / 255f);
		for (int i = 0; i < 2; i++)
		{
			int num = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.PurpleTorch, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 130, default(Color), 3.75f);
			Main.dust[num].scale *= 0.5f;
			Main.dust[num].noGravity = true;
			Main.dust[num].velocity *= 2.5f;
		}
	}
}
