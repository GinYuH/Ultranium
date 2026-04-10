using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dread;

public class DreadMinion : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Mini Dread");
		ProjectileID.Sets.MinionTargettingFeature[((ModProjectile)this).projectile.type] = true;
		ProjectileID.Sets.MinionSacrificable[((ModProjectile)this).projectile.type] = true;
		ProjectileID.Sets.Homing[((ModProjectile)this).projectile.type] = true;
		Main.projFrames[((ModProjectile)this).projectile.type] = 3;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.minionSlots = 1f;
		Main.projPet[((ModProjectile)this).projectile.type] = true;
		((ModProjectile)this).projectile.CloneDefaults(388);
		((ModProjectile)this).projectile.minion = true;
		base.aiType = 388;
	}

	public override void AI()
	{
		_ = ((ModProjectile)this).projectile.type;
		((ModProjectile)this).mod.ProjectileType("DreadMinion");
		Player obj = Main.player[((ModProjectile)this).projectile.owner];
		UltraniumPlayer modPlayer = obj.GetModPlayer<UltraniumPlayer>();
		if (obj.dead)
		{
			modPlayer.DreadMinion = false;
		}
		if (modPlayer.DreadMinion)
		{
			((ModProjectile)this).projectile.timeLeft = 2;
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}
}
