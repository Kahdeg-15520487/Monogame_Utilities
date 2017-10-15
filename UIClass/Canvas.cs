﻿using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;

namespace Utilities
{
    namespace UIClass
    {
        class Canvas :UIObject
        {
            Dictionary<string, UIObject> UIelements;

            public UIObject this[string uiname]
            {
                get
                {
                    return UIelements[uiname];
                }
            }

            public List<string> UInames
            {
                get
                {
                    return UIelements.Keys.ToList();
                }
            }

            public Canvas()
            {
                UIelements = new Dictionary<string, UIObject>();
            }

            public bool AddElement(string uiName, UIObject element)
            {
                if (!UIelements.ContainsKey(uiName))
                {
                    UIelements.Add(uiName, element);
                    return true;
                }
                else
                {
                    //log stuff
                    return false;
                }
            }

            public UIObject GetElement(string uiName)
            {
                if (UIelements.ContainsKey(uiName))
                {
                    return UIelements[uiName];
                }
                else
                {
                    //log stuff
                    return null;
                }
            }

            public T GetElementAs<T>(string uiName)
            {
                if (UIelements.ContainsKey(uiName))
                {
                    return (T)(object)UIelements[uiName];
                }
                else
                {
                    //log stuff
                    return default(T);
                }
            }

            public void LoadContent()
            {
                //UIspritesheet = content.Load<Texture2D>("sprite\\UIspritesheet");
            }

            public override void Update(InputState inputState, InputState lastInputState)
            {
                if (!this.IsVisible)
                    return;

                foreach (var element in UIelements.Values)
                {
                    if (element.IsVisible)
                    {
                        element.Update(inputState, lastInputState);
                    }
                }
                lastInputState = inputState;
            }

            public override void Draw(SpriteBatch spriteBatch)
            {
                if (!this.IsVisible)
                    return;

                foreach (var element in UIelements.Values)
                {
                    if (element.IsVisible)
                    {
                        element.Draw(spriteBatch);
                    }
                }
            }
        }
    }
}