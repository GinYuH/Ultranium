using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Mushroom;

public class ShroomStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Fungus Staff");
		//Tooltip.SetDefault("Fires mushroom spores");
	}

	public override void SetDefaults()
	{
		Item.damage = 10;
		Item.DamageType = DamageClass.Magic;
		Item.width = 16;
		Item.height = 14;
		Item.useTime = 25;
		Item.useAnimation = 25;
		Item.useStyle = 5;
		Item.staff[Item.type] = true;
		Item.noMelee = true;
		Item.knockBack = 2f;
		Item.value = Item.buyPrice(0, 0, 80);
		Item.rare = 1;
		Item.mana = 5;
		Item.UseSound = SoundID.Item8;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("ShroomSpore").Type;
		Item.shootSpeed = 2.5f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(183, 10);
		val.AddTile(16);
		val.Register();
	}
}
