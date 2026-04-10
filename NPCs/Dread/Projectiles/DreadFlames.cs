using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Dread.Projectiles;

public class DreadFlames : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Flame Breath");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.scale = 0.01f;
		((ModProjectile)this).Projectile.width = 16;
		((ModProjectile)this).Projectile.height = 16;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Ranged;
		((ModProjectile)this).Projectile.penetrate = 1;
		((ModProjectile)this).Projectile.timeLeft = 125;
		((ModProjectile)this).Projectile.extraUpdates = 3;
		((ModProjectile)this).Projectile.tileCollide = false;
	}

	public override void AI()
	{
		Lighting.AddLight(((ModProjectile)this).Projectile.Center, (float)(255 - ((ModProjectile)this).Projectile.alpha) * 0.15f / 255f, (float)(255 - ((ModProjectile)this).Projectile.alpha) * 0.45f / 255f, (float)(255 - ((ModProjectile)this).Projectile.alpha) * 0.05f / 255f);
		for (int i = 0; i < 2; i++)
		{
			int num = Dust.NewDust(new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y), ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 90, ((ModProjectile)this).Projectile.velocity.X * 1.2f, ((ModProjectile)this).Projectile.velocity.Y * 1.2f, 130, default(Color), 3.75f);
			Main.dust[num].scale *= 0.5f;
			Main.dust[num].noGravity = true;
			Main.dust[num].velocity *= 2.5f;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		player.AddBuff(((ModProjectile)this).Mod.Find<ModBuff>("DreadDebuff").Type, 240, fromNetPvP: true);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		((ModProjectile)this).Projectile.Kill();
		return false;
	}
}
