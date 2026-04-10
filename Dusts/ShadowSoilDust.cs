using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Dusts;

public class ShadowSoilDust : ModDust
{
	public override void OnSpawn(Dust dust)
	{
		dust.velocity.Y *= 1f;
		dust.velocity.X *= 1f;
		dust.scale *= 1f;
		dust.noGravity = false;
		dust.noLight = true;
	}
}
