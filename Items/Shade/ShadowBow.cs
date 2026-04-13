using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade;

public class ShadowBow : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Shadow Bow");
	}

	public override void SetDefaults()
	{
		Item.damage = 20;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 40;
		Item.height = 40;
		Item.useTime = 20;
		Item.useAnimation = 20;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.knockBack = 6f;
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.buyPrice(0, 8);
		Item.UseSound = SoundID.Item5;
		Item.autoReuse = true;
		Item.shoot = ProjectileID.PurificationPowder;
		Item.shootSpeed = 6.5f;
		Item.useAmmo = AmmoID.Arrow;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "NightmareBar", 12);
		val.AddTile(TileID.Anvils);
		val.Register();
	}
}
