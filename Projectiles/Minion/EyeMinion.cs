using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Minion;

public class EyeMinion : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Mini Eye");
		ProjectileID.Sets.MinionTargettingFeature[((ModProjectile)this).projectile.type] = true;
		ProjectileID.Sets.MinionSacrificable[((ModProjectile)this).projectile.type] = true;
		ProjectileID.Sets.Homing[((ModProjectile)this).projectile.type] = true;
		Main.projFrames[((ModProjectile)this).projectile.type] = 3;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.minionSlots = 1f;
		((ModProjectile)this).projectile.CloneDefaults(388);
		((ModProjectile)this).projectile.minion = true;
		base.aiType = 388;
	}

	public override void AI()
	{
		_ = ((ModProjectile)this).projectile.type;
		((ModProjectile)this).mod.ProjectileType("EyeMinion");
		Player obj = Main.player[((ModProjectile)this).projectile.owner];
		UltraniumPlayer modPlayer = obj.GetModPlayer<UltraniumPlayer>();
		if (obj.dead)
		{
			modPlayer.EyeMinion = false;
		}
		if (modPlayer.EyeMinion)
		{
			((ModProjectile)this).projectile.timeLeft = 2;
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}
}
