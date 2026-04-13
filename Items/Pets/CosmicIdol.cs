using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Buffs.Pet;
using Ultranium.Projectiles.Pets;

namespace Ultranium.Items.Pets;

public class CosmicIdol : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Cosmic Insignia");
		//Tooltip.SetDefault("Summons a pet cosmic djinn");
	}

	public override void SetDefaults()
	{
		Item.rare = ItemRarityID.Purple;
		Item.CloneDefaults(ItemID.Fish);
		Item.shoot = ModContent.ProjectileType<CosmicDjinn>();
		Item.buffType = ModContent.BuffType<CosmicDjinnBuff>();
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
