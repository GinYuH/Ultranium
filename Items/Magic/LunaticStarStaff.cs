using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Magic;

public class LunaticStarStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Ancient Light Staff");
		((ModItem)this).Tooltip.SetDefault("Casts a fast moving ancient light");
		Item.staff[((ModItem)this).item.type] = true;
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 100;
		((ModItem)this).item.magic = true;
		((ModItem)this).item.mana = 22;
		((Entity)(object)((ModItem)this).item).width = 58;
		((Entity)(object)((ModItem)this).item).height = 56;
		((ModItem)this).item.useTime = 30;
		((ModItem)this).item.useAnimation = 30;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.knockBack = 5f;
		((ModItem)this).item.value = Item.buyPrice(1);
		((ModItem)this).item.rare = 8;
		((ModItem)this).item.UseSound = SoundID.Item8;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("LunaticStar");
		((ModItem)this).item.shootSpeed = 20f;
	}
}
