using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Mushroom.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class MushroomHead : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Glowing Mushroom Helmet");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 34;
		((Entity)(object)((ModItem)this).Item).height = 22;
		((ModItem)this).Item.value = Item.buyPrice(0, 0, 80);
		((ModItem)this).Item.rare = 1;
		((ModItem)this).Item.defense = 2;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ((ModItem)this).Mod.Find<ModItem>("MushroomBody").Type)
		{
			return legs.type == ((ModItem)this).Mod.Find<ModItem>("MushroomLegs").Type;
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
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(183, 12);
		val.AddTile(16);
		val.Register();
	}
}
