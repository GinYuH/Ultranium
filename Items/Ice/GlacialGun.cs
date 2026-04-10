using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ice;

public class GlacialGun : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).Tooltip.SetDefault("Has a chance to shoot ice bolts");
		((ModItem)this).DisplayName.SetDefault("Glacial Pistol");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 20;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 58;
		((Entity)(object)((ModItem)this).item).height = 26;
		((ModItem)this).item.useTime = 20;
		((ModItem)this).item.useAnimation = 20;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.rare = 3;
		((ModItem)this).item.value = Item.buyPrice(0, 20);
		((ModItem)this).item.UseSound = SoundID.Item40;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = 242;
		((ModItem)this).item.shootSpeed = 12f;
		((ModItem)this).item.useAmmo = AmmoID.Bullet;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		Vector2 vector = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(4f));
		speedX = vector.X;
		speedY = vector.Y;
		if (type == 14 && Main.rand.Next(5) == 0)
		{
			type = 119;
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
