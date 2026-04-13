using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ethereal;

public class AbysmalStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Abysmal Trident");
		//Tooltip.SetDefault("Conjures abysmal trident bolts");
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.damage = 75;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 22;
		Item.width = 58;
		Item.height = 56;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.noMelee = true;
		Item.knockBack = 5f;
		Item.value = Item.buyPrice(0, 30);
		Item.rare = ItemRarityID.Cyan;
		Item.UseSound = SoundID.Item20;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("AbyssBolt").Type;
		Item.shootSpeed = 10f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(ItemID.UnholyTrident, 1);
		val.AddIngredient(null, "XenanisFlesh", 5);
		val.AddIngredient(null, "ShadowFlame", 5);
		val.AddTile(TileID.MythrilAnvil);
		val.Register();
	}
}
