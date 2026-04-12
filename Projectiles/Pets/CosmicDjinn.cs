using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class CosmicDjinn : ModProjectile
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Cosmic Djinn");
		Main.projFrames[Projectile.type] = 4;
		Main.projPet[Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		Projectile.CloneDefaults(198);
		base.AIType = 198;
		Projectile.timeLeft = 999999999;
		Projectile.timeLeft *= 999999999;
		Projectile.penetrate = -1;
	}

	public override void AI()
	{
		Player obj = Main.player[Projectile.owner];
		UltraniumPlayer modPlayer = obj.GetModPlayer<UltraniumPlayer>();
		if (obj.dead)
		{
			modPlayer.CosmicDjinn = false;
		}
		if (modPlayer.CosmicDjinn)
		{
			Projectile.timeLeft = 2;
		}
	}
}
