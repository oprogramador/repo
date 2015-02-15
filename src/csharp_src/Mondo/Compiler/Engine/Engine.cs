/*
 * Engine.cs
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
using System.Collections.Generic;

namespace Mondo.Engine {
	class Engine {
		private static Engine instance;
		public SymbolMap SymbolMap { get; private set; }
		public IOMap IOMap { get; private set; }
		public static readonly Random Random = new Random();

		private Engine() {
			SymbolMap = new SymbolMap();
			IOMap = new IOMap();
		}

		public static Engine GetInstance() {
			if(instance==null) instance = new Engine();
			return instance;
		}
	}
}