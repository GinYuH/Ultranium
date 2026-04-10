using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pumpkin;

public class PumpSlime : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Pumpkin Minion");
		Main.projFrames[((ModProjectile)this).projectile.type] = 1;
		ProjectileID.Sets.MinionSacrificable[((ModProjectile)this).projectile.type] = true;
		ProjectileID.Sets.Homing[((ModProjectile)this).projectile.type] = true;
		ProjectileID.Sets.MinionTargettingFeature[((ModProjectile)this).projectile.type] = true;
	}

	public override void SetDefaults()
	{
		Main.projFrames[((ModProjectile)this).projectile.type] = 6;
		((ModProjectile)this).projectile.CloneDefaults(266);
		((ModProjectile)this).projectile.width = 26;
		((ModProjectile)this).projectile.height = 26;
		((ModProjectile)this).projectile.minion = true;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.tileCollide = true;
		((ModProjectile)this).projectile.netImportant = true;
		base.aiType = 266;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.timeLeft = 18000;
		((ModProjectile)this).projectile.minionSlots = 1f;
		((ModProjectile)this).projectile.alpha = 0;
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
		bool num = ((ModProjectile)this).projectile.type == ((ModProjectile)this).mod.ProjectileType("PumpSlime");
		Player player = Main.player[((ModProjectile)this).projectile.owner];
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (num)
		{
			if (player.dead)
			{
				modPlayer.PumpSlime = false;
			}
			if (modPlayer.PumpSlime)
			{
				((ModProjectile)this).projectile.timeLeft = 2;
			}
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}
}
