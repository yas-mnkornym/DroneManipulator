using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

using SharpGL;

namespace DroneManipulator.OpenGLMonitor.ViewModels
{
	internal class MonitorControlViewModel : BaseNotifyPropertyChanged
	{
		Models.IDroneDrawer droneDrawer_ = null;
		DronePosture[] postures_ = null;

		public MonitorControlViewModel(Models.IDroneDrawer droneDrawer, Dispatcher dispatcher)
			: base(dispatcher)
		{
			droneDrawer_ = droneDrawer;
		}

		public void Update(DronePosture[] postures)
		{
			postures_ = postures;
		}

		#region OpenGL
		void Initialize(OpenGL gl, double windowWidth, double windowHeight)
		{
			gl.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
			gl.Enable(OpenGL.GL_DEPTH_TEST);
			gl.MatrixMode(OpenGL.GL_PROJECTION);
			gl.LoadIdentity();
			gl.Perspective(
				45.0,
				(double)640 / 480,
				0.1,
				100.0);
			gl.LookAt(
				0.0, 0.0, 0.0,
				0.0, 0.0, 1.0,
				0.0, 1.0, 0.0);
		}

		#region Draw
		#region fields
		float[][] vertices_ = new float[][]{
			new float[]{-0.5f,-0.5f,0.5f},
			new float[] {0.5f,-0.5f,0.5f},
			new float[] {0.5f,0.5f,0.5f},
			new float[] {-0.5f,0.5f,0.5f},
			new float[] {0.5f,-0.5f,-0.5f},
			new float[] {-0.5f,-0.5f,-0.5f},
			new float[] {-0.5f,0.5f,-0.5f},
			new float[] {0.5f,0.5f,-0.5f}
		};
		#endregion // fields

		void Draw(OpenGL gl, double windowWidth, double windowHeight)
		{
			gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
			gl.Viewport(0, 0, (int)windowWidth, (int)windowHeight);
			gl.LoadIdentity();

			gl.PushMatrix();
			gl.Translate(0.0, 0.0, -2);
			gl.Rotate(60f, 150f, 60f);

			
			gl.Color(1.0, 0.0, 1.0, 1.0);
			gl.Begin(OpenGL.GL_POLYGON);
			gl.Vertex(-0.5f, -0.5f, 0.0f);
			gl.Vertex(0.5f, -0.5f, 0.0f);
			gl.Vertex(0.5f, 0.5f, 0.0f);
			gl.Vertex(-0.5f, 0.5f, 0.0f);
			/*
			gl.Begin(OpenGL.GL_POLYGON);
			gl.Vertex(vertices_[0]);
			gl.Vertex(vertices_[1]);
			gl.Vertex(vertices_[2]);
			gl.Vertex(vertices_[3]);
			gl.End();

			gl.Color(0, 1, 0, 1.0);
			gl.Begin(OpenGL.GL_POLYGON);
			gl.Vertex(vertices_[4]);
			gl.Vertex(vertices_[5]);
			gl.Vertex(vertices_[6]);
			gl.Vertex(vertices_[7]);
			gl.End();

			gl.Color(0, 0, 1, 1.0);
			gl.Begin(OpenGL.GL_POLYGON);
			gl.Vertex(vertices_[1]);
			gl.Vertex(vertices_[4]);
			gl.Vertex(vertices_[7]);
			gl.Vertex(vertices_[2]);
			gl.End();


			gl.Color(1, 1, 0, 1.0);
			gl.Begin(OpenGL.GL_POLYGON);
			gl.Vertex(vertices_[5]);
			gl.Vertex(vertices_[0]);
			gl.Vertex(vertices_[3]);
			gl.Vertex(vertices_[6]);
			gl.End();

			gl.Color(1, 0, 1, 1.0);
			gl.Begin(OpenGL.GL_POLYGON);
			gl.Vertex(vertices_[3]);
			gl.Vertex(vertices_[2]);
			gl.Vertex(vertices_[7]);
			gl.Vertex(vertices_[6]);
			gl.End();

			gl.Color(1, 1, 1, 1.0);
			gl.Begin(OpenGL.GL_POLYGON);
			gl.Vertex(vertices_[1]);
			gl.Vertex(vertices_[0]);
			gl.Vertex(vertices_[5]);
			gl.Vertex(vertices_[4]);
			gl.End();

			gl.Color(0.5, 0.5, 1.0, 1.0);
			gl.Begin(OpenGL.GL_POLYGON);
			gl.Vertex(vertices_[0]);
			gl.Vertex(vertices_[1]);
			gl.Vertex(vertices_[2]);
			gl.Vertex(vertices_[3]);
			gl.End();

			gl.Color(0.5, 0.5, 0.0, 1.0);
			gl.Begin(OpenGL.GL_POLYGON);
			gl.Vertex(vertices_[0]);
			gl.Vertex(vertices_[1]);
			gl.Vertex(vertices_[2]);
			gl.Vertex(vertices_[3]);
			 * */
			gl.End();

			gl.PopMatrix();
		}
		#endregion // Draw
		#endregion // OpenGL

		#region Commands
		#region DrawCommand
		DelegateCommand drawCommand_ = null;
		public DelegateCommand DrawCommand
		{
			get
			{
				if (drawCommand_ == null) {
					drawCommand_ = new DelegateCommand {
						ExecuteHandler = param => {
							var glParam = param as Views.Behaviors.OpenGLCommandParameter;
							if (glParam != null) {
								Draw(glParam.OpenGL, glParam.ControlWidth, glParam.ControlHeight);
							}
						}
					};
				}
				return drawCommand_;
			}
		}
		#endregion

		#region InitializeCommand
		DelegateCommand initializeCommand_ = null;
		public DelegateCommand InitializeCommand
		{
			get
			{
				if (initializeCommand_ == null) {
					initializeCommand_ = new DelegateCommand {
						ExecuteHandler = param => {
							var glParam = param as Views.Behaviors.OpenGLCommandParameter;
							if (glParam != null) {
								Initialize(glParam.OpenGL, glParam.ControlWidth, glParam.ControlHeight);
							}
						}
					};
				}
				return initializeCommand_;
			}
		}
		#endregion
		#endregion // Commands
	}
}
