using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Pet;

public class DragonHornetBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Dragon Hornet");
		Description.SetDefault("A long last species...");
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = true;
		Main.vanityPet[((ModBuff)this).Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.buffTime[buffIndex] = 18000;
		player.GetModPlayer<UltraniumPlayer>().DragonHornet = true;
		if (player.ownedProjectileCounts[((ModBuff)this).Mod.Find<ModProjectile>("DragonHornet").Type] <= 0 && player.whoAmI == Main.myPlayer)
		{
			Projectile.NewProjectile(null, player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ((ModBuff)this).Mod.Find<ModProjectile>("DragonHornet").Type, 0, 0f, player.whoAmI, 0f, 0f);
		}
	}
}
