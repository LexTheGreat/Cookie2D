using System;
using System.Collections.Generic;
using SFML.Window;
using SFML.Graphics;
using NetEXT.TimeFunctions;
using Tao.OpenGl;

namespace CookieLib.Interface.Screens
{
	public class ScreenManager
	{
		#region Variables
		private RenderWindow _gamewindow = null;
		private View _camera = null;
		private List<ScreenProvider> _screenmanagerstack = new List<ScreenProvider>();
		private Time _timestep = Time.Zero;
		private Color _clearcolor = Color.Black;
		private bool _stoploop = false;
		#endregion

		#region Properties
		public RenderWindow GameWindow
		{
			get
			{
				return _gamewindow;
			}
		}
		public View Camera
		{
			get
			{
				return _camera;
			}
			set
			{
				_camera = value;
			}
		}
		public Time TimeStep
		{
			get
			{
				return _timestep;
			}
			set
			{
				_timestep = value;
			}
		}
		public Color ClearColor
		{
			get
			{
				return _clearcolor;
			}
			set
			{
				_clearcolor = value;
			}
		}
		public bool StopLoop
		{
			get
			{
				return _stoploop;
			}
			set
			{
				_stoploop = value;
			}
		}
		#endregion

		#region Constructors
		public ScreenManager(GameSettings settings, ScreenProvider InitialScreenManager, Time TimeStep)
		{
			_gamewindow = settings.Create();
			_camera = _gamewindow.GetView ();
			InitialScreenManager.InitializeGUI(_gamewindow);
			_screenmanagerstack.Add(InitialScreenManager);
			BindWindowEvents();
			_screenmanagerstack[_screenmanagerstack.Count - 1].SwitchScreen += OnSwitchScreen;
			_screenmanagerstack[_screenmanagerstack.Count - 1].CloseScreen += OnCloseScreen;
			_screenmanagerstack [_screenmanagerstack.Count - 1].rndTarget = _gamewindow;
			_screenmanagerstack[_screenmanagerstack.Count - 1].ScreenActivated();
			_timestep = TimeStep;
		}
		#endregion

		#region Functions
		private void BindWindowEvents()
		{
			_gamewindow.MouseWheelMoved += (sender, e) => { _screenmanagerstack[_screenmanagerstack.Count - 1].window_MouseWheelMoved(_gamewindow, e); };
			_gamewindow.TextEntered += (sender, e) => { _screenmanagerstack[_screenmanagerstack.Count - 1].window_TextEntered(_gamewindow, e); };
			_gamewindow.KeyPressed += (sender, e) => { _screenmanagerstack[_screenmanagerstack.Count - 1].window_KeyPressed(_gamewindow, e); };
			_gamewindow.KeyReleased += (sender, e) => { _screenmanagerstack[_screenmanagerstack.Count - 1].window_KeyReleased(_gamewindow, e); };
			_gamewindow.MouseMoved += (sender, e) => { _screenmanagerstack[_screenmanagerstack.Count - 1].window_MouseMoved(_gamewindow, e); };
			_gamewindow.MouseButtonPressed += (sender, e) => { _screenmanagerstack[_screenmanagerstack.Count - 1].window_MouseButtonPressed(_gamewindow, e); };
			_gamewindow.MouseButtonReleased += (sender, e) => { _screenmanagerstack[_screenmanagerstack.Count - 1].window_MouseButtonReleased(_gamewindow, e); };
			_gamewindow.Closed += (sender, e) => { _gamewindow.Close(); };
		}
		private void OnSwitchScreen(ScreenProvider NewScreenManager)
		{
			if (_screenmanagerstack.Contains(NewScreenManager)) return;
			_screenmanagerstack[_screenmanagerstack.Count - 1].DisposeGUI ();
			_screenmanagerstack [_screenmanagerstack.Count - 1].rndTarget = null;
			_screenmanagerstack[_screenmanagerstack.Count - 1].ScreenDeactivated();
			_screenmanagerstack[_screenmanagerstack.Count - 1].SwitchScreen -= OnSwitchScreen;
			_screenmanagerstack[_screenmanagerstack.Count - 1].CloseScreen -= OnCloseScreen;
			_screenmanagerstack.Add(NewScreenManager);
			_screenmanagerstack[_screenmanagerstack.Count - 1].InitializeGUI (_gamewindow);
			_screenmanagerstack[_screenmanagerstack.Count - 1].SwitchScreen += OnSwitchScreen;
			_screenmanagerstack[_screenmanagerstack.Count - 1].CloseScreen += OnCloseScreen;
			_screenmanagerstack [_screenmanagerstack.Count - 1].rndTarget = _gamewindow;
			_screenmanagerstack[_screenmanagerstack.Count - 1].ScreenActivated();
		}
		private void OnCloseScreen()
		{
			_screenmanagerstack[_screenmanagerstack.Count - 1].DisposeGUI();
			_screenmanagerstack [_screenmanagerstack.Count - 1].rndTarget = null;
			_screenmanagerstack[_screenmanagerstack.Count - 1].ScreenDeactivated();
			_screenmanagerstack[_screenmanagerstack.Count - 1].SwitchScreen -= OnSwitchScreen;
			_screenmanagerstack[_screenmanagerstack.Count - 1].CloseScreen -= OnCloseScreen;
			_screenmanagerstack.RemoveAt(_screenmanagerstack.Count - 1);
			if (_screenmanagerstack.Count <= 0) _gamewindow.Close();
			else
			{
				_screenmanagerstack[_screenmanagerstack.Count - 1].InitializeGUI (_gamewindow);
				_screenmanagerstack[_screenmanagerstack.Count - 1].SwitchScreen += OnSwitchScreen;
				_screenmanagerstack[_screenmanagerstack.Count - 1].CloseScreen += OnCloseScreen;
				_screenmanagerstack [_screenmanagerstack.Count - 1].rndTarget = _gamewindow;
				_screenmanagerstack[_screenmanagerstack.Count - 1].ScreenActivated();
			}
		}
		public void RunLoop()
		{
			Clock frameclock = new Clock();
			Time elapsedtime = Time.Zero;
			while (_gamewindow.IsOpen() && !_stoploop)
			{
				elapsedtime += frameclock.Restart();
				_gamewindow.Clear(_clearcolor);
				// Clear depth buffer
				Gl.glClear(Gl.GL_DEPTH_BUFFER_BIT | Gl.GL_COLOR_BUFFER_BIT);
				while (elapsedtime >= TimeStep)
				{
					elapsedtime -= TimeStep;
					_gamewindow.SetView (_camera);
					_screenmanagerstack[_screenmanagerstack.Count - 1].Update(TimeStep);
				}
				if (_screenmanagerstack.Count >= 1) 
				{
					_screenmanagerstack[_screenmanagerstack.Count - 1].Draw(_gamewindow);
					_screenmanagerstack [_screenmanagerstack.Count - 1].GameGUI.RenderCanvas();
				}
				_gamewindow.Display();
				_gamewindow.DispatchEvents();
			}
		}
		#endregion
	}
}