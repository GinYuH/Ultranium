using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dread;

public class DreadMinion : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Mini Dread");
		ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
		ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		Main.projFrames[Projectile.type] = 3;
	}

	public override void SetDefaults()
	{
		Projectile.minionSlots = 1f;
		Main.projPet[Projectile.type] = true;
		Projectile.CloneDefaults(388);
		Projectile.minion = true;
		base.AIType = 388;
	}

	public override void AI()
	{
		_ = Projectile.type;
		Player obj = Main.player[Projectile.owner];
		UltraniumPlayer modPlayer = obj.GetModPlayer<UltraniumPlayer>();
		if (obj.dead)
		{
			modPlayer.DreadMinion = false;
		}
		if (modPlayer.DreadMinion)
		{
			Projectile.timeLeft = 2;
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}
}
