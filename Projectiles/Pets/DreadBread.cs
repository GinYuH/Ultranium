using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class DreadBread : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Dread Bread");
		Main.projFrames[Projectile.type] = 3;
		Main.projPet[Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		Projectile.CloneDefaults(112);
		AIType = ProjectileID.Penguin;
		Projectile.width = 24;
		Projectile.height = 26;
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
			modPlayer.DreadBread = false;
		}
		if (modPlayer.DreadBread)
		{
			Projectile.timeLeft = 2;
		}
	}
}
