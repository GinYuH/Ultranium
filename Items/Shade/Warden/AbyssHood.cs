using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade.Warden;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class AbyssHood : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Abyss Warden Hood");
		((ModItem)this).Tooltip.SetDefault("5% increased summon damage");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 34;
		((Entity)(object)((ModItem)this).item).height = 22;
		((ModItem)this).item.value = Item.buyPrice(0, 45);
		((ModItem)this).item.rare = 7;
		((ModItem)this).item.defense = 15;
	}

	public override void UpdateEquip(Player player)
	{
		player.minionDamage += 0.05f;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ((ModItem)this).mod.ItemType("AbyssWardenBody"))
		{
			return legs.type == ((ModItem)this).mod.ItemType("AbyssWardenLegs");
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "\nWhile you are below 75%, 50%, and 25% life, you will gain an extra minion slot\nYou will also gain a 15% increase to your summon damage when you are below 50% life\nThe minion slot bonuses will stack with each other";
		if (player.statLife < (int)((float)player.statLifeMax2 * 0.75f))
		{
			player.maxMinions++;
		}
		if (player.statLife < (int)((float)player.statLifeMax2 * 0.5f))
		{
			player.minionDamage += 0.15f;
			player.maxMinions++;
		}
		if (player.statLife < (int)((float)player.statLifeMax2 * 0.25f))
		{
			player.maxMinions++;
		}
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "AbyssRockItem", 45);
		val.AddIngredient((Mod)null, "DepthGlowstoneItem", 35);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
