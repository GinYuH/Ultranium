using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Pumpkin.Eldritch;

public class EldritchPumpkinScepter : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Eldritch Pumpkin Scepter");
		((ModItem)this).Tooltip.SetDefault("Summons a stationary eldritch pumpkin that shoots flaming seeds at enemies\nOnly one pumpkin sentry can be active at once");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 26;
		((Entity)(object)((ModItem)this).item).height = 28;
		((ModItem)this).item.rare = 4;
		((ModItem)this).item.mana = 20;
		((ModItem)this).item.damage = 45;
		((ModItem)this).item.knockBack = 1f;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.useTime = 30;
		((ModItem)this).item.useAnimation = 30;
		((ModItem)this).item.summon = true;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.UseSound = SoundID.Item117;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("EldritchPumpkinSentry");
		((ModItem)this).item.shootSpeed = 0f;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		for (int i = 0; i < Main.projectile.Length; i++)
		{
			Projectile projectile = Main.projectile[i];
			if (((Entity)projectile).active && projectile.type == ((ModItem)this).item.shoot && projectile.owner == player.whoAmI)
			{
				((Entity)projectile).active = false;
			}
		}
		Vector2 vector = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
		position = vector;
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "PumpkinSummon", 1);
		val.AddIngredient((Mod)null, "ShadowEssence", 20);
		val.AddIngredient(521, 10);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
