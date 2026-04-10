using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowWorm.Projectiles;

public class ShadowFlameBreath : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Shadow Flame Breath");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.alpha = 255;
		((ModProjectile)this).Projectile.width = 32;
		((ModProjectile)this).Projectile.height = 32;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Ranged;
		((ModProjectile)this).Projectile.penetrate = 1;
		((ModProjectile)this).Projectile.timeLeft = 125;
		((ModProjectile)this).Projectile.extraUpdates = 3;
	}

	public override void AI()
	{
		Lighting.AddLight(((ModProjectile)this).Projectile.Center, (float)(255 - ((ModProjectile)this).Projectile.alpha) * 0.15f / 255f, (float)(255 - ((ModProjectile)this).Projectile.alpha) * 0.45f / 255f, (float)(255 - ((ModProjectile)this).Projectile.alpha) * 0.05f / 255f);
		for (int i = 0; i < 2; i++)
		{
			int num = Dust.NewDust(new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y), ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 89, ((ModProjectile)this).Projectile.velocity.X * 1.2f, ((ModProjectile)this).Projectile.velocity.Y * 1.2f, 130, default(Color), 3.75f);
			Main.dust[num].scale *= 1f;
			Main.dust[num].noGravity = true;
			Main.dust[num].velocity *= 2.5f;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		player.AddBuff(((ModProjectile)this).Mod.Find<ModBuff>("DarkDebuff").Type, 300, fromNetPvP: true);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		((ModProjectile)this).Projectile.Kill();
		return false;
	}
}
