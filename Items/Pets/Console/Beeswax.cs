using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Ultranium.Buffs.Pet;
using Ultranium.Projectiles.Pets;

namespace Ultranium.Items.Pets.Console;

public class Beeswax : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Beeswax");
		// ((ModItem)this).Tooltip.SetDefault("Summons a pet dragon hornet");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.rare = 3;
		((ModItem)this).Item.CloneDefaults(669);
		((ModItem)this).Item.shoot = ModContent.ProjectileType<DragonHornet>();
		((ModItem)this).Item.buffType = ModContent.BuffType<DragonHornetBuff>();
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
		{
			player.AddBuff(((ModItem)this).Item.buffType, 3600, fromNetPvP: true);
		}
	}

	public override bool CanUseItem(Player player)
	{
		return player.miscEquips[0].IsAir;
	}
}
