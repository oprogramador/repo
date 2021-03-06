/*
 * TokenTypes.cs
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
 

using System.Windows.Forms;

namespace Mondo.MyCollections {
	public enum TokenTypes {
		Symbol,
		Number,
		Operator,
		BracketOpen,
		BracketClose,
		SquareOpen,
		SquareClose,
		CurlyOpen,
		CurlyClose,
		DollarOpen,
		HashOpen,
		OneString,
		BiString,
		TriString,
		SoftLink,
		OneLineComment,
		MultiLineComment,
		WhiteSpace,
		EndLine,
	}

	public static class TokenTypesExtension {
		public static bool IsString(this TokenTypes t) {
			return t==TokenTypes.OneString || t==TokenTypes.BiString || t==TokenTypes.TriString;
		}

		public static bool IsComment(this TokenTypes t) {
			return t==TokenTypes.OneLineComment || t==TokenTypes.MultiLineComment;
		}

		public static bool IsValue(this TokenTypes t) {
			return t==TokenTypes.Symbol || t==TokenTypes.Number || t.IsString() || t==TokenTypes.SoftLink;
		}
	}
}
