/*
 * MyMenu.cs
 * Copyright 2014 pierre (Piotr Sroczkowski) <pierre.github@gmail.com>
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA 02110-1301, USA.
 * 
 * 
 */
 

using System;
using System.Collections.Generic;using System.Windows.Forms;

namespace Mondo.Gui {
	public class MyMenu : MainMenu {
		public MyMenu(string[] str, MyItem[][] items) {
			if(str==null) return;
			if(str.Length<1) return;
			MenuItem[] mi = new MenuItem[str.Length];
			for(int i=0; i<str.Length; i++) MenuItems.Add(mi[i] = new MenuItem(str[i]));
			for(int i=0; i<items.Length; i++)
				for(int k=0; k<items[i].Length; k++)
					mi[i].MenuItems.Add(items[i][k].str, items[i][k].eh);
		}
	}
}
