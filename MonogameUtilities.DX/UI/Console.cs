//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Graphics;

//namespace Utility.UI {
//	public class Console : UIObject {
//		private Label outputbox;
//		private InputBox inputbox;
//		private List<string> log = new List<string>();
//		public SpriteFont Font { get; set; }

//		public string Text {
//			get {
//				return inputbox.Text;
//			}
//			set {
//				inputbox.Text = value;
//			}
//		}

//		private int maxLogLine = 10;

//		public Console(Point position, Vector2 size, SpriteFont font) {
//			Position = position;
//			Size = size;
//			Font = font;

//			var inputboxPosition = new Point(position.X, position.Y + (int)size.Y - 50);
//			var inputboxSize = new Vector2(size.X, 50);
//			var outputboxPosition = new Point(position.X, position.Y);
//			var outputboxSize = new Vector2(size.X, size.Y - 50);

//			inputbox = new InputBox("", inputboxPosition, inputboxSize, font, Color.White, Color.DarkGray) {
//				CaretColor = Color.LightGray
//			};
//			inputbox.ignoreCharacter.Add('`');

//			outputbox = new Label("", outputboxPosition, outputboxSize, font) {
//				ForegroundColor = Color.White,
//				BackgroundColor = Color.LightGray,
//				Origin = outputboxPosition.ToVector2() + new Vector2(5, 5)
//			};
//			maxLogLine = findMaxLogLine();

//			inputbox.KeyPress += ExecuteScript;

//			InitPythonEngine();
//		}

//		private void ExecuteScript(object sender, UIEventArgs e) {
//			if (e.currentKeyboardState.IsKeyDown(Keys.Enter) && e.lastKeyboardState.IsKeyUp(Keys.Enter)) {
//				OnCommandSubmitted(this, e);
//				//log.Add(inputbox.Text);
//				//from Microsoft.Xna.Framework import *\n
//				Behaviour
//		    inputbox.Clear();

//				//ScriptSource source = _engine.CreateScriptSourceFromString(userCode.ToString());
//				//CompiledCode code = null;
//				//catch compile error
//				try {
//					code = source.Compile();
//				}
//				catch (Exception err) {
//					ExceptionOperations eo = _engine.GetService<ExceptionOperations>();
//					Utility.HelperFunction.Log(eo.FormatException(err) + '\n' + userCode.ToString());
//					log.Add("error when compile script");
//					return;
//				}

//				//catch runtim error
//				try {
//					code.Execute(_scope);
//				}
//				catch (Exception err) {
//					ExceptionOperations eo = _engine.GetService<ExceptionOperations>();
//					Utility.HelperFunction.Log(eo.FormatException(err) + '\n' + userCode.ToString());
//					log.Add("error when execute script");
//				}
//			}
//		}

//		private void InitPythonEngine() {
//			_engine = Python.CreateEngine();
//			_runtime = _engine.Runtime;
//			_runtime.LoadAssembly(typeof(Wartorn.UIClass.Console).Assembly);
//			_scope = _engine.CreateScope();
//			_scope.SetVariable("log", log);
//			_scope.SetVariable("console", this);
//		}

//		public void SetScope(Dictionary<string, object> variableCollection) {
//			foreach (var kvp in variableCollection) {
//				if (_scope.ContainsVariable(kvp.Key)) {
//					Log("An variable with the name" + kvp.Key + " already exists");
//					return;
//				}
//				_scope.SetVariable(kvp.Key, kvp.Value);
//			}
//		}

//		public void SetVariable(string varname, object var) {
//			if (_scope.ContainsVariable(varname)) {
//				Log("An variable with the name" + varname + " already exists");
//				return;
//			}
//			_scope.SetVariable(varname, var);
//		}

//		public void Log(object obj) {
//			log.Add(obj.ToString());
//		}

//		private int findMaxLogLine() {
//			string teststr = "\n";
//			while (font.MeasureString(teststr).Y < outputbox.rect.Height) {
//				teststr += '\n';
//			}
//			return teststr.Length;
//		}

//		public override void Update(InputState inputState, InputState lastInputState) {
//			inputbox.Update(inputState, lastInputState);
//			outputbox.Update(inputState, lastInputState);

//			if (log.Count > 0) {
//				outputbox.Text = log.Skip(Math.Max(0, log.Count - maxLogLine)).Aggregate((current, next) => current + "\n" + next);
//			}
//			else {
//				outputbox.Text = "";
//			}
//		}

//		public override void Draw(SpriteBatch spriteBatch) {
//			inputbox.Draw(spriteBatch);
//			outputbox.Draw(spriteBatch);
//		}

//		public event EventHandler<UIEventArgs> CommandSubmitted;

//		protected virtual void OnCommandSubmitted(object sender, UIEventArgs e) {
//			CommandSubmitted?.Invoke(sender, e);
//		}
//	}
//}