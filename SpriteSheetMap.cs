using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Utilities {
	public class SpriteSheetMap{
		public readonly string SpriteSheetName;
		public readonly Texture2D SpriteSheet;
		public List<Rectangle> SpriteRect;
		public Dictionary<string, int> SpriteRectDict;

		public Rectangle this[string index] {
			get { return SpriteRect[SpriteRectDict[index]]; }
		}
		public Rectangle this[int index] {
			get { return SpriteRect[index];  }
		}

		public SpriteSheetMap(string name, Texture2D spritesheet, int width = 0, int height = 0) {
			SpriteSheetName = name;
			SpriteSheet = spritesheet;
			SpriteRect = new List<Rectangle>();
			SpriteRectDict = new Dictionary<string, int>();
			
			if (width != 0 && height != 0) {
				StringBuilder tt = new StringBuilder();
				tt.AppendFormat("W = {0}; H = {1}", SpriteSheet.Width / width, SpriteSheet.Height / height);
				tt.AppendLine();
				for (int h = 0; h < SpriteSheet.Height / height; h++) {
					for (int w = 0; w < SpriteSheet.Width / width; w++) {
						AddSpriteRect(h + ":" + w , new Rectangle(w * width, h * height, width, height));
						tt.Append(h + ":" + w + "    :    ");
						tt.AppendLine(new Rectangle(w * width, h * height, width, height).ToString());
					}
				}
				System.IO.File.AppendAllText("map.txt", tt.ToString());
			}
		}

		public Rectangle GetSpriteRect(string rectname) {
			if (!SpriteRectDict.ContainsKey(rectname)) {
				throw new KeyNotFoundException(rectname);
			}

			return SpriteRect[SpriteRectDict[rectname]];
		}

		public bool AddSpriteRect(string rectname, Rectangle rect) {
			if (SpriteRectDict.ContainsKey(rectname)) {
				return false;
			}

			SpriteRect.Add(rect);
			SpriteRectDict.Add(rectname, SpriteRect.Count - 1);

			return true;
		}

		public void RenameSpriteRect(string oldname,string newname) {
			int spriteindex = SpriteRectDict[oldname];
			SpriteRectDict.Remove(oldname);
			SpriteRectDict.Add(newname, spriteindex);
		}
	}
}
