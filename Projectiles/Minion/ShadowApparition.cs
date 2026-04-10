using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Minion;

public class ShadowApparition : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Shadowflame Apparition");
		Main.projFrames[((ModProjectile)this).projectile.type] = 8;
		ProjectileID.Sets.MinionSacrificable[((ModProjectile)this).projectile.type] = true;
		ProjectileID.Sets.Homing[((ModProjectile)this).projectile.type] = true;
		ProjectileID.Sets.MinionTargettingFeature[((ModProjectile)this).projectile.type] = true;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.CloneDefaults(317);
		((ModProjectile)this).projectile.width = 30;
		((ModProjectile)this).projectile.height = 30;
		((ModProjectile)this).projectile.minion = true;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.tileCollide = true;
		((ModProjectile)this).projectile.netImportant = true;
		base.aiType = 317;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.timeLeft = 18000;
		((ModProjectile)this).projectile.minionSlots = 1f;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (((ModProjectile)this).projectile.penetrate == 0)
		{
			((ModProjectile)this).projectile.Kill();
		}
		return false;
	}

	public override void AI()
	{
		bool num = ((ModProjectile)this).projectile.type == ((ModProjectile)this).mod.ProjectileType("ShadowApparition");
		Player player = Main.player[((ModProjectile)this).projectile.owner];
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (num)
		{
			if (player.dead)
			{
				modPlayer.ShadowApparition = false;
			}
			if (modPlayer.ShadowApparition)
			{
				((ModProjectile)this).projectile.timeLeft = 2;
			}
		}
		if (++((ModProjectile)this).projectile.frameCounter >= 3)
		{
			((ModProjectile)this).projectile.frameCounter = 0;
			if (++((ModProjectile)this).projectile.frame >= 8)
			{
				((ModProjectile)this).projectile.frame = 0;
			}
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}
}
