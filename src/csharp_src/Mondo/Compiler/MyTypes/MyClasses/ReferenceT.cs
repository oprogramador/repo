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
 

using Mondo.MyCollections;
using System;
using System.Collections.Generic;
using Mondo.lib;

namespace Mondo.MyTypes.MyClasses {
	public class ReferenceT : Pointer<IVariable>, IVariable, ITypeConvertible {
		public bool IsLocked { get; private set; }
		private string criptoKey;
		private IVariable _value;
		public override IVariable Value {
			get { return _value; }
			set {
				if(!IsLocked) _value = value;
			}
		}

		public ReferenceT(IVariable iv) :  base(iv) {
			ID = ObjectContainer.Instance.Add(this);
		}

		public static Type GetType(object ob) {
			if(ob is ReferenceT) return ((ReferenceT)ob).Value.GetType();
			return ob.GetType();
		}

		public int ID { get; private set; }

		public override bool Equals(object ob) {
			return CompareTo(ob)==0;
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public int CompareTo(object ob) {
			int pre = ReferenceT.PreCompare(this,ob);
			if(pre!=0) return pre;
			return Value.CompareTo(((ReferenceT)ob).Value);
		}

		private static string getClassName(object ob) {
			var iv = (IVariable)TypeTrans.dereference(ob);
			if(iv is StringT) return "string";
			return iv.GetClass().Name;
		}

		public static int PreCompare(object a, object b) {
			if(b is bool) {
				if(!(a is BooleanT || a is ReferenceT)) return 1;
				if(a is ReferenceT) if(!(((ReferenceT)a).Value is BooleanT)) return 1;
			}
			if(a is IVariable && b is IVariable) return getClassName(a).CompareTo(getClassName(b));
			return 0;
		}

		public object Clone() {
			return new ReferenceT(Value);
		}
		
		IVariable[] ITuplable.ToArray() {
			var ar = Value.ToArray();
			if(ar.Length!=1) return ar;
			return new IVariable[]{this};
		}

		public IVariable[] RefToArray() {
			return null;
		}

		private static ClassT myClass;
		
		public const string ClassName = "Reference";

		private static object[] lambdas = {

		};
		
		public ClassT GetClass() {
			return Value.GetClass();
		}

		public static ClassT StaticGetClass() {
			if(myClass==null) myClass = 
				new BuiltinClass( ClassName, new List<ClassT>(){ObjectT.StaticGetClass()}, LambdaConverter.Convert(lambdas), PackageT.Lang, typeof(ReferenceT) );
			return myClass;
		}	

		public override string ToString() {
			return ""+Value;
		}

		string IStringable.ToString() {
			return ((IStringable)Value).ToString();
		}

		public object Convert() {
			return Value;
		}

		public string Lock() {
			IsLocked = true;
			var key = SString.Rand("0123456789",120);
			criptoKey = MyCrypto.Sha256(key);
			return key;
		}

		public ReferenceT Unlock(string key) {
			if(MyCrypto.Sha256(key).Equals( criptoKey )) IsLocked = false;
			return this;
		}
	}
}
