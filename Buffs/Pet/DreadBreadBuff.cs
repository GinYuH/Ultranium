using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Pet;

public class DreadBreadBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		// ((ModBuff)this).DisplayName.SetDefault("Dread Bread");
		// ((ModBuff)this).Description.SetDefault("Is it a loaf of bread, or a loaf of dread?");
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = true;
		Main.vanityPet[((ModBuff)this).Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.buffTime[buffIndex] = 18000;
		player.GetModPlayer<UltraniumPlayer>().DreadBread = true;
		if (player.ownedProjectileCounts[((ModBuff)this).Mod.Find<ModProjectile>("DreadBread").Type] <= 0 && player.whoAmI == Main.myPlayer)
		{
			Projectile.NewProjectile(null, player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ((ModBuff)this).Mod.Find<ModProjectile>("DreadBread").Type, 0, 0f, player.whoAmI, 0f, 0f);
		}
	}
}
