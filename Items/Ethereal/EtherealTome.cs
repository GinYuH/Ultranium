using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ethereal;

public class EtherealTome : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Ethereal Spell");
		((ModItem)this).Tooltip.SetDefault("Casts a spread of ethereal blasts");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.value = Item.buyPrice(0, 65);
		((ModItem)this).item.damage = 55;
		((ModItem)this).item.magic = true;
		((ModItem)this).item.mana = 13;
		((Entity)(object)((ModItem)this).item).width = 40;
		((Entity)(object)((ModItem)this).item).height = 40;
		((ModItem)this).item.useTime = 25;
		((ModItem)this).item.useAnimation = 25;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.knockBack = 5f;
		((ModItem)this).item.rare = 9;
		((ModItem)this).item.value = Item.buyPrice(0, 30);
		((ModItem)this).item.UseSound = SoundID.Item20;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("BigEtherealBlast");
		((ModItem)this).item.shootSpeed = 36f;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		float num = 4f;
		float num2 = MathHelper.ToRadians(12f);
		position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
		for (int i = 0; (float)i < num; i++)
		{
			Vector2 vector = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(0f - num2, num2, (float)i / (num - 1f))) * 0.2f;
			Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
		}
		return false;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "XenanisFlesh", 10);
		val.AddIngredient((Mod)null, "ShadowFlame", 5);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
