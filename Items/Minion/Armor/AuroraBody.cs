using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Minion.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class AuroraBody : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Aurora Crystal Breastplate");
		// ((ModItem)this).Tooltip.SetDefault("4% increased summon damage and +1 max minions");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 34;
		((Entity)(object)((ModItem)this).Item).height = 22;
		((ModItem)this).Item.value = 10000;
		((ModItem)this).Item.rare = 1;
		((ModItem)this).Item.defense = 3;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Summon) += 0.04f;
		player.maxMinions++;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "AuroraBar", 7);
		val.AddTile(16);
		val.Register();
	}
}
