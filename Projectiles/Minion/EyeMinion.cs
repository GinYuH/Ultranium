using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Minion;

public class EyeMinion : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Mini Eye");
		ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
		ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		Main.projFrames[Projectile.type] = 3;
	}

	public override void SetDefaults()
	{
		Projectile.minionSlots = 1f;
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
			modPlayer.EyeMinion = false;
		}
		if (modPlayer.EyeMinion)
		{
			Projectile.timeLeft = 2;
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}
}
