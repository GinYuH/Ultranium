using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Pet;

public class CosmicDjinnBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Cosmic Djinn");
		Description.SetDefault("The cosmic djinn follows you");
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = true;
		Main.vanityPet[((ModBuff)this).Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.GetModPlayer<UltraniumPlayer>().CosmicDjinn = true;
		if (player.ownedProjectileCounts[((ModBuff)this).Mod.Find<ModProjectile>("CosmicDjinn").Type] <= 0 && player.whoAmI == Main.myPlayer)
		{
			Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ((ModBuff)this).Mod.Find<ModProjectile>("CosmicDjinn").Type, 0, 0f, player.whoAmI, 0f, 0f);
		}
	}
}
