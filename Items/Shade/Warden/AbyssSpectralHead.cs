using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade.Warden;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class AbyssSpectralHead : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Spectral Abyss Hood");
		((ModItem)this).Tooltip.SetDefault("Increases maximum mana by 80\n10% increased magic damage and critical strike chance");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 34;
		((Entity)(object)((ModItem)this).item).height = 22;
		((ModItem)this).item.value = Item.buyPrice(0, 45);
		((ModItem)this).item.rare = 7;
		((ModItem)this).item.defense = 13;
	}

	public override void UpdateEquip(Player player)
	{
		player.statManaMax2 += 80;
		player.magicDamage += 0.1f;
		player.magicCrit += 10;
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
		player.setBonus = "\n20% reduced mana usage";
		player.manaCost -= 0.2f;
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
