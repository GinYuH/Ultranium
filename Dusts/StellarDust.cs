using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Dusts;

public class StellarDust : ModDust
{
	public override void OnSpawn(Dust dust)
	{
		dust.velocity.Y *= 1f;
		dust.velocity.X *= 1f;
		dust.scale *= 1f;
		dust.noGravity = true;
		dust.noLight = false;
	}

	public override Color? GetAlpha(Dust dust, Color lightColor)
	{
		return Color.White;
	}
}
