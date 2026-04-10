using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Cosmic;

public class CosmicStaff : ModItem
{
	private Color[] itemNameCycleColors = new Color[2]
	{
		new Color(93, 215, 195),
		new Color(72, 37, 169)
	};

	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Protostar Scepter");
		// ((ModItem)this).Tooltip.SetDefault("Casts protostars that explode into bolts");
		Item.staff[((ModItem)this).Item.type] = true;
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 380;
		((ModItem)this).Item.DamageType = DamageClass.Magic;
		((ModItem)this).Item.mana = 10;
		((Entity)(object)((ModItem)this).Item).width = 40;
		((Entity)(object)((ModItem)this).Item).height = 40;
		((ModItem)this).Item.useTime = 16;
		((ModItem)this).Item.useAnimation = 16;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.knockBack = 5f;
		((ModItem)this).Item.value = Item.buyPrice(2);
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.UseSound = SoundID.Item20;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("Protostar").Type;
		((ModItem)this).Item.shootSpeed = 10f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		foreach (TooltipLine tooltip in tooltips)
		{
			if (tooltip.Mod == "Terraria" && tooltip.Name == "ItemName")
			{
				float amount = (float)(Main.GameUpdateCount % 60) / 60f;
				int num = (int)(Main.GameUpdateCount / 60 % 2);
				tooltip.OverrideColor = Color.Lerp(itemNameCycleColors[num], itemNameCycleColors[(num + 1) % 2], amount);
			}
		}
	}
}
