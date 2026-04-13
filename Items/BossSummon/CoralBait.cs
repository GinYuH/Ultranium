using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.BossSummon;

public class CoralBait : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Coral Bait");
		//Tooltip.SetDefault("A strange bait made with coral shards...\nAttracts the zephyr squid, only when used as bait in the ocean\nCan be used as normal fishing bait anywhere else");
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 20;
		Item.maxStack = 20;
		Item.rare = ItemRarityID.LightRed;
		Item.bait = 25;
		Item.consumable = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(ItemID.Coral, 10);
		val.AddRecipeGroup("Ultranium:ShadowScale/TissueSample", 6);
		val.AddTile(TileID.WorkBenches);
		val.Register();
	}
}
