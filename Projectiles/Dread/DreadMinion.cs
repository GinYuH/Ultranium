using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dread;

public class DreadMinion : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Mini Dread");
		ProjectileID.Sets.MinionTargettingFeature[((ModProjectile)this).Projectile.type] = true;
		ProjectileID.Sets.MinionSacrificable[((ModProjectile)this).Projectile.type] = true;
		ProjectileID.Sets.CultistIsResistantTo[((ModProjectile)this).Projectile.type] = true;
		Main.projFrames[((ModProjectile)this).Projectile.type] = 3;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.minionSlots = 1f;
		Main.projPet[((ModProjectile)this).Projectile.type] = true;
		((ModProjectile)this).Projectile.CloneDefaults(388);
		((ModProjectile)this).Projectile.minion = true;
		base.AIType = 388;
	}

	public override void AI()
	{
		_ = ((ModProjectile)this).Projectile.type;
		((ModProjectile)this).Mod.Find<ModProjectile>("DreadMinion").Type;
		Player obj = Main.player[((ModProjectile)this).Projectile.owner];
		UltraniumPlayer modPlayer = obj.GetModPlayer<UltraniumPlayer>();
		if (obj.dead)
		{
			modPlayer.DreadMinion = false;
		}
		if (modPlayer.DreadMinion)
		{
			((ModProjectile)this).Projectile.timeLeft = 2;
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}
}
