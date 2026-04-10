using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Minion;

public class EyeMinion : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Mini Eye");
		ProjectileID.Sets.MinionTargettingFeature[((ModProjectile)this).Projectile.type] = true;
		ProjectileID.Sets.MinionSacrificable[((ModProjectile)this).Projectile.type] = true;
		ProjectileID.Sets.CultistIsResistantTo[((ModProjectile)this).Projectile.type] = true;
		Main.projFrames[((ModProjectile)this).Projectile.type] = 3;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.minionSlots = 1f;
		((ModProjectile)this).Projectile.CloneDefaults(388);
		((ModProjectile)this).Projectile.minion = true;
		base.AIType = 388;
	}

	public override void AI()
	{
		_ = ((ModProjectile)this).Projectile.type;
		((ModProjectile)this).Mod.Find<ModProjectile>("EyeMinion").Type;
		Player obj = Main.player[((ModProjectile)this).Projectile.owner];
		UltraniumPlayer modPlayer = obj.GetModPlayer<UltraniumPlayer>();
		if (obj.dead)
		{
			modPlayer.EyeMinion = false;
		}
		if (modPlayer.EyeMinion)
		{
			((ModProjectile)this).Projectile.timeLeft = 2;
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}
}
