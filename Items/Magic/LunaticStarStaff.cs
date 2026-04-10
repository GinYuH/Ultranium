using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Magic;

public class LunaticStarStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Ancient Light Staff");
		// Tooltip.SetDefault("Casts a fast moving ancient light");
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.damage = 100;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 22;
		Item.width = 58;
		Item.height = 56;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 5f;
		Item.value = Item.buyPrice(1);
		Item.rare = 8;
		Item.UseSound = SoundID.Item8;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("LunaticStar").Type;
		Item.shootSpeed = 20f;
	}
}
