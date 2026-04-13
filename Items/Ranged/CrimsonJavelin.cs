using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class CrimsonJavelin : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Crimtane Pike");
		//Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		Item.damage = 18;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 66;
		Item.height = 60;
		Item.useTime = 23;
		Item.useAnimation = 23;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 12);
		Item.rare = ItemRarityID.Blue;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("CrimsonJavelin").Type;
		Item.shootSpeed = 8f;
		Item.useTurn = true;
		Item.maxStack = 1;
		Item.noUseGraphic = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(ItemID.CrimtaneBar, 10);
		val.AddTile(TileID.Anvils);
		val.Register();
	}
}
