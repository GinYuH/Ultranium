using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Pet;

public class TacoBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("The devil on Dylan's shoulder");
		//Description.SetDefault("...");
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = true;
		Main.vanityPet[((ModBuff)this).Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.buffTime[buffIndex] = 18000;
		player.GetModPlayer<UltraniumPlayer>().TacoDemon = true;
		if (player.ownedProjectileCounts[((ModBuff)this).Mod.Find<ModProjectile>("TacoDemon").Type] <= 0 && player.whoAmI == Main.myPlayer)
		{
			Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ((ModBuff)this).Mod.Find<ModProjectile>("TacoDemon").Type, 0, 0f, player.whoAmI, 0f, 0f);
		}
	}
}
