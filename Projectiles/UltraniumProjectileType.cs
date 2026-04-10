using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles;

public class UltraniumProjectileType : GlobalProjectile
{
	public bool whip;

	public int whipAliveTime = 30;

	public int specialPenetrate = -2;

	public override bool InstancePerEntity => true;

	public UltraniumProjectileType()
	{
		whip = false;
	}

	public override void SetDefaults(Projectile projectile)
	{
		if (projectile.type == 611)
		{
			whip = true;
			whipAliveTime = 30;
		}
	}
}
