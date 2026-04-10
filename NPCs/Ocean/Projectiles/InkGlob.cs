using System;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ocean.Projectiles;

public class InkGlob : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[((ModProjectile)this).projectile.type] = 6;
		((ModProjectile)this).DisplayName.SetDefault("Ink Glob");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.scale = 1f;
		((ModProjectile)this).projectile.width = 14;
		((ModProjectile)this).projectile.height = 38;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.aiStyle = 0;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.extraUpdates = 1;
		((ModProjectile)this).projectile.timeLeft = 300;
		((ModProjectile)this).projectile.tileCollide = false;
	}

	public override void OnHitPlayer(Player player, int damage, bool crit)
	{
		player.AddBuff(32, 60, fromNetPvP: true);
	}

	public override void AI()
	{
		if (++((ModProjectile)this).projectile.frameCounter >= 16)
		{
			((ModProjectile)this).projectile.frameCounter = 0;
			if (++((ModProjectile)this).projectile.frame >= 6)
			{
				((ModProjectile)this).projectile.frame = 0;
			}
		}
		((ModProjectile)this).projectile.rotation = ((ModProjectile)this).projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		((ModProjectile)this).projectile.rotation += 0f * (float)((ModProjectile)this).projectile.direction;
	}
}
