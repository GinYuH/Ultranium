using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Mushroom;

public class ShroomStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Fungus Staff");
		// ((ModItem)this).Tooltip.SetDefault("Fires mushroom spores");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 10;
		((ModItem)this).Item.DamageType = DamageClass.Magic;
		((Entity)(object)((ModItem)this).Item).width = 16;
		((Entity)(object)((ModItem)this).Item).height = 14;
		((ModItem)this).Item.useTime = 25;
		((ModItem)this).Item.useAnimation = 25;
		((ModItem)this).Item.useStyle = 5;
		Item.staff[((ModItem)this).Item.type] = true;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.knockBack = 2f;
		((ModItem)this).Item.value = Item.buyPrice(0, 0, 80);
		((ModItem)this).Item.rare = 1;
		((ModItem)this).Item.mana = 5;
		((ModItem)this).Item.UseSound = SoundID.Item8;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("ShroomSpore").Type;
		((ModItem)this).Item.shootSpeed = 2.5f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(183, 10);
		val.AddTile(16);
		val.Register();
	}
}
