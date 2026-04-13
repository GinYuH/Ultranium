using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Pet;

public class WerewolfBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Pet Werewolf");
		Description.SetDefault("It seems to like you");
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = true;
		Main.vanityPet[((ModBuff)this).Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.buffTime[buffIndex] = 18000;
		player.GetModPlayer<UltraniumPlayer>().WerewolfPet = true;
		if (player.ownedProjectileCounts[((ModBuff)this).Mod.Find<ModProjectile>("WerewolfPet").Type] <= 0 && player.whoAmI == Main.myPlayer)
		{
			Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ((ModBuff)this).Mod.Find<ModProjectile>("WerewolfPet").Type, 0, 0f, player.whoAmI, 0f, 0f);
		}
	}
}
