////using System;
//using System.IO;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Graphics;
//using Utility.ScreenManager;
//using Utility.UI;
//using Utility;

//namespace Utility.Screens {
//	class FileBrowsingScreen : Screen {
//		Canvas canvas;
//		List<Button> filelist;

//		public string SelectedFile { get; private set; } = string.Empty;

//		public FileBrowsingScreen(GraphicsDevice device) : base(device, "FileBrowsingScreen") { }

//		public override bool Init() {
//			InitUI();

//			return base.Init();
//		}

//		private void InitUI() {
//			canvas = new Canvas();

//			InitMapList();

//			Button button_open = new Button("Play", new Point(600, 10), new Vector2(60, 30), CONTENT_MANAGER.Fonts["defaultfont"]);
//			button_open.MouseClick += (o, e) => {
//				SCREEN_MANAGER.go_back();
//			};
//			canvas.AddElement("button_open", button_open);
//		}

//		private void InitMapList(string startingdir, string searchpattern) {
//			var files = Directory.GetFiles(startingdir, searchpattern);
//			var y = 10;
//			filelist = new List<Button>();
//			foreach (var m in files) {
//				Button bt = new Button(Path.GetFileName(m), new Point(10, y), new Vector2(120, 30), CONTENT_MANAGER.Fonts["defaultfont"]) {
//					Origin = new Vector2(10, 0),
//					ForegroundColor = Color.Black
//				};

//				bt.MouseClick += (o, e) => {
//					SelectedFile = bt.Text;
//				};

//				y += 35;
//				filelist.Add(bt);
//			}

//			foreach (var m in filelist) {
//				canvas.AddElement(m.Text, m);
//			}
//		}

//		public override void Shutdown() {
//			base.Shutdown();
//		}

//		public override void Update(GameTime gameTime) {
//			canvas.Update(gameTime, CONTENT_MANAGER.CurrentInputState, CONTENT_MANAGER.LastInputState);
//		}

//		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
//			canvas.Draw(spriteBatch, gameTime);
//		}
//	}
//}
