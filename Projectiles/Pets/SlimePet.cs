using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class SlimePet : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Slime");
		Main.projFrames[Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		Projectile.CloneDefaults(334);
		AIType = ProjectileID.Puppy;
		Main.projPet[Projectile.type] = true;
		Projectile.width = 32;
		Projectile.height = 22;
		Projectile.alpha = 50;
	}

	public override void AI()
	{
		Player obj = Main.player[Projectile.owner];
		UltraniumPlayer modPlayer = obj.GetModPlayer<UltraniumPlayer>();
		if (obj.dead)
		{
			modPlayer.SlimePet = false;
		}
		if (modPlayer.SlimePet)
		{
			Projectile.timeLeft = 2;
		}
	}

	public override void PostAI()
	{
		Projectile.frameCounter++;
		if (Projectile.frameCounter > 200)
		{
			Projectile.frame++;
			Projectile.frameCounter = 0;
		}
		if (Projectile.frame >= 2)
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
