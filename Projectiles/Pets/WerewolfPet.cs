using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class WerewolfPet : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Werewolf");
		Main.projFrames[Projectile.type] = 14;
	}

	public override void SetDefaults()
	{
		Projectile.CloneDefaults(334);
		base.AIType = 334;
		Main.projPet[Projectile.type] = true;
		Projectile.width = 34;
		Projectile.height = 52;
	}

	public override bool PreAI()
	{
		Main.player[Projectile.owner].zephyrfish = false;
		return true;
	}

	public override void AI()
	{
		Player obj = Main.player[Projectile.owner];
		UltraniumPlayer modPlayer = obj.GetModPlayer<UltraniumPlayer>();
		if (obj.dead)
		{
			modPlayer.WerewolfPet = false;
		}
		if (modPlayer.WerewolfPet)
		{
			Projectile.timeLeft = 2;
		}
	}

	public override void PostAI()
	{
		Projectile.frameCounter++;
		if (Projectile.frameCounter > 32)
		{
			Projectile.frame++;
			Projectile.frameCounter = 0;
		}
		if (Projectile.frame >= 16)
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
