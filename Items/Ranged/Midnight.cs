using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class Midnight : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).Tooltip.SetDefault("Turns normal bullets into midnight blasts");
		((ModItem)this).DisplayName.SetDefault("Midnight");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.value = Item.buyPrice(0, 1);
		((ModItem)this).item.damage = 13;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 58;
		((Entity)(object)((ModItem)this).item).height = 26;
		((ModItem)this).item.useTime = 12;
		((ModItem)this).item.useAnimation = 12;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.rare = 3;
		((ModItem)this).item.UseSound = SoundID.Item40;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = 242;
		((ModItem)this).item.shootSpeed = 2f;
		((ModItem)this).item.useAmmo = AmmoID.Bullet;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		Vector2 vector = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(4f));
		speedX = vector.X;
		speedY = vector.Y;
		if (type == 14)
		{
			type = ((ModItem)this).mod.ProjectileType("MidnightPro");
		}
		return true;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-6f, 0f);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(117, 10);
		val.AddIngredient((Mod)null, "ShadowEssence", 15);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
