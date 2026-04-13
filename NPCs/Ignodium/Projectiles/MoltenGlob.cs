using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ignodium.Projectiles;

public class MoltenGlob : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Molten Glob");
	}

	public override void SetDefaults()
	{
		Main.projFrames[Projectile.type] = 4;
		Projectile.CloneDefaults(467);
		base.AIType = 467;
	}

	public override bool PreKill(int timeLeft)
	{
		Projectile.type = 467;
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}
}
