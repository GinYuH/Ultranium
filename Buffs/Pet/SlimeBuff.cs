using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Pet;

public class SlimeBuff : ModBuff
{
	public override void SetDefaults()
	{
		((ModBuff)this).DisplayName.SetDefault("Pet Slime");
		((ModBuff)this).Description.SetDefault("A strange friendly glob with bacteria in it");
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = true;
		Main.vanityPet[((ModBuff)this).Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.buffTime[buffIndex] = 18000;
		player.GetModPlayer<UltraniumPlayer>().SlimePet = true;
		if (player.ownedProjectileCounts[((ModBuff)this).mod.ProjectileType("SlimePet")] <= 0 && player.whoAmI == Main.myPlayer)
		{
			Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ((ModBuff)this).mod.ProjectileType("SlimePet"), 0, 0f, player.whoAmI, 0f, 0f);
		}
	}
}
