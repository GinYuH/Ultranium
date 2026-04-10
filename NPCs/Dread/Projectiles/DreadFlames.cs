using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Dread.Projectiles;

public class DreadFlames : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Flame Breath");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.scale = 0.01f;
		((ModProjectile)this).projectile.width = 16;
		((ModProjectile)this).projectile.height = 16;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.ranged = true;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.timeLeft = 125;
		((ModProjectile)this).projectile.extraUpdates = 3;
		((ModProjectile)this).projectile.tileCollide = false;
	}

	public override void AI()
	{
		Lighting.AddLight(((ModProjectile)this).projectile.Center, (float)(255 - ((ModProjectile)this).projectile.alpha) * 0.15f / 255f, (float)(255 - ((ModProjectile)this).projectile.alpha) * 0.45f / 255f, (float)(255 - ((ModProjectile)this).projectile.alpha) * 0.05f / 255f);
		for (int i = 0; i < 2; i++)
		{
			int num = Dust.NewDust(new Vector2(((ModProjectile)this).projectile.position.X, ((ModProjectile)this).projectile.position.Y), ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 90, ((ModProjectile)this).projectile.velocity.X * 1.2f, ((ModProjectile)this).projectile.velocity.Y * 1.2f, 130, default(Color), 3.75f);
			Main.dust[num].scale *= 0.5f;
			Main.dust[num].noGravity = true;
			Main.dust[num].velocity *= 2.5f;
		}
	}

	public override void OnHitPlayer(Player player, int damage, bool crit)
	{
		player.AddBuff(((ModProjectile)this).mod.BuffType("DreadDebuff"), 240, fromNetPvP: true);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		((ModProjectile)this).projectile.Kill();
		return false;
	}
}
