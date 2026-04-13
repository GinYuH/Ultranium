using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class GuineaPig : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Guinea Pig");
		Main.projFrames[Projectile.type] = 8;
		Main.projPet[Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		Projectile.CloneDefaults(111);
		base.AIType = ProjectileID.Bunny;
		Projectile.height = 36;
		Projectile.width = 30;
		Projectile.timeLeft = 999999999;
		Projectile.timeLeft *= 999999999;
	}

	public override void AI()
	{
		Player obj = Main.player[Projectile.owner];
		UltraniumPlayer modPlayer = obj.GetModPlayer<UltraniumPlayer>();
		if (obj.dead)
		{
			modPlayer.GuineaPig = false;
		}
		if (modPlayer.GuineaPig)
		{
			Projectile.timeLeft = 2;
		}
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return true;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}
}
