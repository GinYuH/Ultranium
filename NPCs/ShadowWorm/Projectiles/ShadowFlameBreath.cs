using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowWorm.Projectiles;

public class ShadowFlameBreath : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Shadow Flame Breath");
	}

	public override void SetDefaults()
	{
		Projectile.alpha = 255;
		Projectile.width = 32;
		Projectile.height = 32;
		Projectile.hostile = true;
		Projectile.ignoreWater = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.penetrate = 1;
		Projectile.timeLeft = 125;
		Projectile.extraUpdates = 3;
	}

	public override void AI()
	{
		Lighting.AddLight(Projectile.Center, (float)(255 - Projectile.alpha) * 0.15f / 255f, (float)(255 - Projectile.alpha) * 0.45f / 255f, (float)(255 - Projectile.alpha) * 0.05f / 255f);
		for (int i = 0; i < 2; i++)
		{
			int num = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 89, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 130, default(Color), 3.75f);
			Main.dust[num].scale *= 1f;
			Main.dust[num].noGravity = true;
			Main.dust[num].velocity *= 2.5f;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		player.AddBuff(Mod.Find<ModBuff>("DarkDebuff").Type, 300, fromNetPvP: true);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Projectile.Kill();
		return false;
	}
}
