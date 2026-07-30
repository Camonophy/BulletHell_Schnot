using System;
using Sandbox;

public sealed class Inventory : Component
{

	public int ActiveInventorySlot = 0;

	public int MaxInventorySlots = 9;
	protected override void OnUpdate()
	{
		if (Input.MouseWheel.y != 0)
		{
			// "%" ist weird, die Operation -1%9 => -1, an dieser Stelle will man aber, dass -1%9 => 8 ist.
			ActiveInventorySlot = (ActiveInventorySlot + Math.Sign(Input.MouseWheel.y) + MaxInventorySlots) % MaxInventorySlots;
		}
	}
}
