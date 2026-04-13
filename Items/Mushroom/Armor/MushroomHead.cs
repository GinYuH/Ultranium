using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Mushroom.Armor;

[AutoloadEquip(EquipType.Head)]
public class MushroomHead : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Glowing Mushroom Helmet");
	}

	public override void SetDefaults()
	{
		Item.width = 34;
		Item.height = 22;
		Item.value = Item.buyPrice(0, 0, 80);
		Item.rare = ItemRarityID.Blue;
		Item.defense = 2;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == Mod.Find<ModItem>("MushroomBody").Type)
		{
			return legs.type == Mod.Find<ModItem>("MushroomLegs").Type;
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "\nYou constantly emit a feint blue glow\nYou have a chance to spawn lingering mushroom spores around you when hit";
		player.GetModPlayer<UltraniumPlayer>().MushroomSet = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(ItemID.GlowingMushroom, 12);
		val.AddTile(TileID.Anvils);
		val.Register();
	}
}
