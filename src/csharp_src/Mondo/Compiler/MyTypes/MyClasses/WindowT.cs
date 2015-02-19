/*
 * WindowT.cs
 * Copyright 2015 pierre (Piotr Sroczkowski) <pierre.github@gmail.com>
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
using System.Windows.Forms;
using System.Drawing;
using System.Timers;
using System.Collections.Generic;
using Mondo.Engine;
using Mondo.MyCollections;

namespace Mondo.MyTypes.MyClasses {
    class WindowT : Form, IVariable {
        private System.Timers.Timer timer;
        public int ID { get; private set; }
        public DictionaryT Dictionary { get; private set; }
        public ProcedureT OnKeyPressProc { get; private set; }
        private IPrintable printable;
        private ListT drawingSquares {
            get {
                return (ListT)((PointerT)((ReferenceT)Dictionary[new ReferenceT(new StringT("squares"))]).Value).Value.Value;
            }
            set {
                //Console.WriteLine("set");
                //Dictionary[new ReferenceT(new StringT("squares"))] = new ReferenceT(value); 
            }
        }
        private int drawingSquaresWidth {
            get {
                return ((StringT)((ReferenceT)drawingSquares[0]).Value).Value.Length;
            }
            set {}
        }
        public Dictionary<char,Color> Colors = new Dictionary<char,Color>() {
            {'r', Color.Red},
                {'g', Color.Green},
                {'b', Color.Blue},
                {'y', Color.Yellow},
        };

        public WindowT(IPrintable p, DictionaryT dic) {
            ID = ObjectContainer.Instance.Add(this);
            DoubleBuffered = true;
            printable = p;
            Width = (int)((Number)((ReferenceT)dic[new ReferenceT(new StringT("w"))]).Value).Value;
            Height = (int)((Number)((ReferenceT)dic[new ReferenceT(new StringT("h"))]).Value).Value;
            try {
                int tt = (int)((Number)((ReferenceT)dic[new ReferenceT(new StringT("time"))]).Value).Value;
                timer = new System.Timers.Timer(tt);
                timer.Elapsed += onTimeEvent;
                timer.Enabled = true;
            } catch {}
            OnKeyPressProc = (ProcedureT)((ReferenceT)dic[new ReferenceT(new StringT("keypress"))]).Value;
            Dictionary = dic;
            KeyPress += new KeyPressEventHandler(onKeyPress);
        }

        private void onKeyPress(object sender, KeyPressEventArgs e) {
            OnKeyPressProc.Call(printable, new object[]{new StringT(""+e.KeyChar)});
        }

        private void onTimeEvent(Object source, ElapsedEventArgs e) {
            Refresh();
        }

        public override bool Equals(object ob) {
            return CompareTo(ob)==0;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public int CompareTo(object ob) {
            int pre = ReferenceT.PreCompare(this,ob);
            if(pre!=0) return pre;
            return 0;
        }

        public object Clone() {
            return new WindowT(printable, (DictionaryT)Dictionary.Clone());
        }

        IVariable[] ITuplable.ToArray() {
            return new IVariable[]{};
        }

        private static ClassT myClass;

        public const string ClassName = "Window";

        private static object[] lambdas = {
            "show", 	(Func<WindowT,bool>) ((x) => {x.Show(); Application.Run(x); return true;}),
            "close",	(Func<WindowT,bool>) ((x) => {x.Close(); return true;}),
        };

        public ClassT GetClass() {
            return StaticGetClass();
        }

        public static ClassT StaticGetClass() {
            if(myClass==null) myClass = 
                new BuiltinClass( ClassName, new List<ClassT>(){ObjectT.StaticGetClass()}, LambdaConverter.Convert(lambdas), PackageT.Lang, typeof(WindowT) );
            return myClass;
        }	

        public object ObValue {
            get { return this; }
            set { }
        }

        public override string ToString() {
            return "window( "+Dictionary+" )";
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            var g = e.Graphics;
            if(drawingSquares!=null) drawSquares(g);
            Console.WriteLine("ds="+drawingSquares);
            Console.WriteLine("this="+this);
            g.Dispose();
        }

        private void drawSquares(Graphics g) {
            Console.WriteLine("ds");
            int ww = Width/drawingSquaresWidth;
            int hh = Height/drawingSquares.Count;
            for(int y=0; y<drawingSquares.Count; y++) {
                var str = ((StringT)((ReferenceT)drawingSquares[y]).Value).Value;
                for(int x=0; x<str.Length; x++) {
                    var brush = new SolidBrush(Colors[str[x]]);
                    g.FillRectangle(brush, new Rectangle(x*ww, y*hh, ww, hh));
                    brush.Dispose();
                }
            }
        }
    }
}
