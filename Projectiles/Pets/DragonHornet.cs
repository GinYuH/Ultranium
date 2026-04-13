using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class DragonHornet : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Dragon Hornet");
		Main.projFrames[Projectile.type] = 3;
	}

	public override void SetDefaults()
	{
		Projectile.CloneDefaults(198);
		base.AIType = ProjectileID.BabyHornet;
		Main.projPet[Projectile.type] = true;
		Projectile.width = 42;
		Projectile.height = 36;
	}

	public override void AI()
	{
		Player obj = Main.player[Projectile.owner];
		UltraniumPlayer modPlayer = obj.GetModPlayer<UltraniumPlayer>();
		if (obj.dead)
		{
			modPlayer.DragonHornet = false;
		}
		if (modPlayer.DragonHornet)
		{
			Projectile.timeLeft = 2;
		}
		Projectile.frameCounter++;
		if (Projectile.frameCounter > 9)
		{
			Projectile.frameCounter = 0;
		}
		if (Projectile.frame >= 3)
		{
			Projectile.frame = 0;
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
