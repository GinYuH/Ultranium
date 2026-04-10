using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ice;

public class GlacialFlail : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Glacial Flail");
		((ModItem)this).Tooltip.SetDefault("The flail shoots icy bolts at nearby enemies");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 30;
		((Entity)(object)((ModItem)this).item).height = 10;
		((ModItem)this).item.rare = 3;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.UseSound = SoundID.Item1;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.useAnimation = 40;
		((ModItem)this).item.useTime = 40;
		((ModItem)this).item.knockBack = 7.5f;
		((ModItem)this).item.damage = 35;
		((ModItem)this).item.noUseGraphic = true;
		((ModItem)this).item.melee = true;
		((ModItem)this).item.channel = true;
		((ModItem)this).item.value = Item.buyPrice(0, 20);
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("GlacialFlail");
		((ModItem)this).item.shootSpeed = 15f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(664, 10);
		val.AddIngredient((Mod)null, "IcePelt", 7);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
