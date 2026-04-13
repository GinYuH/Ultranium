using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread;

public class DreadStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Fear's Flame");
		//Tooltip.SetDefault("Casts dread fire balls");
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.damage = 50;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 12;
		Item.width = 58;
		Item.height = 56;
		Item.useTime = 16;
		Item.useAnimation = 16;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.noMelee = true;
		Item.knockBack = 5f;
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.buyPrice(0, 12);
		Item.UseSound = SoundID.Item20;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("DreadFlameBall").Type;
		Item.shootSpeed = 6f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "DreadFlame", 10);
		val.AddIngredient(null, "DreadScale", 5);
		val.AddTile(TileID.MythrilAnvil);
		val.Register();
	}
}
