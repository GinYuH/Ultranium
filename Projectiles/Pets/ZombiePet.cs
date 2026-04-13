using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class ZombiePet : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Zombie");
		Main.projFrames[Projectile.type] = 3;
		Main.projPet[Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		Projectile.CloneDefaults(111);
		AIType = ProjectileID.Bunny;
		Projectile.width = 34;
		Projectile.height = 44;
		Projectile.timeLeft = 999999999;
		Projectile.timeLeft *= 999999999;
	}

	public override void AI()
	{
		Player player = Main.player[Projectile.owner];
		Projectile.frameCounter++;
		if (Projectile.frameCounter > 18)
		{
			Projectile.frame++;
			Projectile.frameCounter = 0;
		}
		if (Projectile.frame >= 3)
		{
			Projectile.frame = 0;
		}
		if (Projectile.localAI[0] >= 800f)
		{
			Projectile.localAI[0] = 0f;
		}
		if (Vector2.Distance(player.Center, Projectile.Center) > 500f)
		{
			Projectile.position.X = player.position.X;
			Projectile.position.Y = player.position.Y;
		}
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (player.dead)
		{
			modPlayer.ZombiePet = false;
		}
		if (modPlayer.ZombiePet)
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
