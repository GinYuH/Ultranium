using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Buffs.Pet;
using Ultranium.Projectiles.Pets;

namespace Ultranium.Items.Pets;

public class DreadBreadItem : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Suspicious Looking Bread");
		//Tooltip.SetDefault("Summons... Dread?");
	}

	public override void SetDefaults()
	{
		Item.CloneDefaults(ItemID.Fish);
		Item.shoot = ModContent.ProjectileType<DreadBread>();
		Item.buffType = ModContent.BuffType<DreadBreadBuff>();
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
		{
			player.AddBuff(Item.buffType, 3600, quiet: false);
		}
	}

	public override bool CanUseItem(Player player)
	{
		return player.miscEquips[0].IsAir;
	}
}
