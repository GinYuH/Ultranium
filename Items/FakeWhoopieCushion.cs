using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items;

public class FakeWhoopieCushion : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Whoopie Cushion?");
		((ModItem)this).Tooltip.SetDefault("'May annoy others'\nLegends say that it's farts can be harvested...");
		ItemID.Sets.ExtractinatorMode[((ModItem)this).item.type] = ((ModItem)this).item.type;
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 16;
		((Entity)(object)((ModItem)this).item).height = 14;
		((ModItem)this).item.maxStack = 1;
		((ModItem)this).item.useStyle = 4;
		((ModItem)this).item.useTime = 30;
		((ModItem)this).item.useAnimation = 30;
		((ModItem)this).item.UseSound = SoundID.Item16;
		((ModItem)this).item.rare = 2;
		((ModItem)this).item.useTurn = true;
		((ModItem)this).item.autoReuse = false;
	}

	public override void ExtractinatorUse(ref int resultType, ref int resultStack)
	{
		string[] array = new string[1] { "TheFart" };
		int num = Main.rand.Next(array.Length);
		resultType = ((ModItem)this).mod.ItemType(array[num]);
		resultStack = 1;
		if (!UltraniumWorld.TheFart)
		{
			UltraniumWorld.TheFart = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7);
			}
		}
		int num2 = Main.LocalPlayer.FindItem(((ModItem)this).mod.ItemType("FakeWhoopieCushion"));
		Main.LocalPlayer.inventory[num2].TurnToAir();
	}
}
