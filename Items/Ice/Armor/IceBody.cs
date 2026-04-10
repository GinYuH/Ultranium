using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ice.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class IceBody : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Ice Walker's Chestplate");
		((ModItem)this).Tooltip.SetDefault("5% increased damage");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 34;
		((Entity)(object)((ModItem)this).item).height = 22;
		((ModItem)this).item.value = Item.buyPrice(0, 2, 50);
		((ModItem)this).item.rare = 3;
		((ModItem)this).item.defense = 7;
	}

	public override void UpdateEquip(Player player)
	{
		player.meleeDamage += 0.05f;
		player.rangedDamage += 0.05f;
		player.minionDamage += 0.05f;
		player.magicDamage += 0.05f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(664, 55);
		val.AddIngredient((Mod)null, "IcePelt", 12);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
