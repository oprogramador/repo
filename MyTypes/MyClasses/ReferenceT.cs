/*
 * ReferenceT.cs
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
 

using MyCollections;
using System;
using System.Collections.Generic;

namespace MyTypes.MyClasses {
	public class ReferenceT : Pointer<IVariable>, IVariable {
		public ReferenceT(IVariable iv) :  base(iv) {
			ID = ObjectContainer.Instance.Add(this);
			Console.WriteLine("reference ctor");
		}

		public static Type GetType(object ob) {
			if(ob is ReferenceT) return ((ReferenceT)ob).Value.GetType();
			return ob.GetType();
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			int pre = TypeT.PreCompare(this,ob);
			if(pre!=0) return pre;
			return Value.CompareTo(((ReferenceT)ob).Value);
		}

		public object Clone() {
			return new ReferenceT(Value);
		}
		
		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public IVariable[] RefToArray() {
			return null;
		}

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;


		private static object[] lambdas = {

		};
		
		static ReferenceT() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "Reference", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(ReferenceT) ); 
		}

		public ClassT GetClass() {
			return Value.GetClass();
		}

		string IStringable.ToString() {
			return ""+Value;
		}
	}
}
