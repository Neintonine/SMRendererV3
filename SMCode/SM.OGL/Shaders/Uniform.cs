using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SM.OGL.Texture;

namespace SM.OGL.Shaders
{
    public struct Uniform
    {
        /// <summary>
        /// This contains the location for the uniform.
        /// </summary>
        private int Location;
        /// <summary>
        /// This contains the Parent collection of this uniform.
        /// </summary>
        internal UniformCollection Parent;


        /// <summary>
        /// This create a new uniform manager
        /// </summary>
        /// <param name="location">Location id</param>
        /// <param name="parent">Parent collection</param>
        public Uniform(int location, UniformCollection parent)
        {
            Location = location;
            Parent = parent;
        }

        #region Uniform1

        public void SetUniform1(bool value)
        {
            GL.Uniform1(Location, value ? 1 : 0);
        }

        public void SetUniform1(int value) { GL.Uniform1(Location, value); }
        public void SetUniform1(int count, params int[] values) { GL.Uniform1(Location, count, values); }
        public void SetUniform1(int count, ref int values) { GL.Uniform1(Location, count, ref values); }


        public void SetUniform1(uint value) { GL.Uniform1(Location, value); }
        public void SetUniform1(int count, params uint[] values) { GL.Uniform1(Location, count, values); }
        public void SetUniform1(int count, ref uint values) { GL.Uniform1(Location, count, ref values); }


        public void SetUniform1(float value) { GL.Uniform1(Location, value); }
        public void SetUniform1(int count, params float[] values) { GL.Uniform1(Location, count, values); }
        public void SetUniform1(int count, ref float value) { GL.Uniform1(Location, count, ref value); }


        public void SetUniform1(double value) { GL.Uniform1(Location, value); }
        public void SetUniform1(int count, params double[] values) { GL.Uniform1(Location, count, values); }
        public void SetUniform1(int count, ref double value) { GL.Uniform1(Location, count, ref value); }

        #endregion

        #region Uniform2

        public void SetUniform2(float x, float y) { GL.Uniform2(Location, x, y); }
        public void SetUniform2(double x, double y) { GL.Uniform2(Location, x, y); }
        public void SetUniform2(uint x, uint y) { GL.Uniform2(Location, x, y); }
        public void SetUniform2(int x, int y) { GL.Uniform2(Location, x, y); }

        public void SetUniform2(int count, params float[] values) { GL.Uniform2(Location, count, values); }
        public void SetUniform2(int count, params double[] values) { GL.Uniform2(Location, count, values); }
        public void SetUniform2(int count, params int[] values) { GL.Uniform2(Location, count, values); }
        public void SetUniform2(int count, params uint[] values) { GL.Uniform2(Location, count, values); }

        public void SetUniform2(int count, ref float values) { GL.Uniform2(Location, count, ref values); }
        public void SetUniform2(int count, ref double values) { GL.Uniform2(Location, count, ref values); }
        public void SetUniform2(int count, ref uint values) { GL.Uniform2(Location, count, ref values); }

        public void SetUniform2(Vector2 vector2) { GL.Uniform2(Location, vector2); }
        public void SetUniform2(ref Vector2 vector2) { GL.Uniform2(Location, ref vector2); }

        #endregion

        #region Uniform3

        public void SetUniform3(float x, float y, float z) { GL.Uniform3(Location, x, y, z); }
        public void SetUniform3(double x, double y, double z) { GL.Uniform3(Location, x, y, z); }
        public void SetUniform3(uint x, uint y, uint z) { GL.Uniform3(Location, x, y, z); }
        public void SetUniform3(int x, int y, int z) { GL.Uniform3(Location, x, y, z); }

        public void SetUniform3(int count, params float[] values) { GL.Uniform3(Location, count, values); }
        public void SetUniform3(int count, params double[] values) { GL.Uniform3(Location, count, values); }
        public void SetUniform3(int count, params int[] values) { GL.Uniform3(Location, count, values); }
        public void SetUniform3(int count, params uint[] values) { GL.Uniform3(Location, count, values); }

        public void SetUniform3(int count, ref float values) { GL.Uniform3(Location, count, ref values); }
        public void SetUniform3(int count, ref double values) { GL.Uniform3(Location, count, ref values); }
        public void SetUniform3(int count, ref uint values) { GL.Uniform3(Location, count, ref values); }

        public void SetUniform3(Vector3 vector) { GL.Uniform3(Location, vector); }
        public void SetUniform3(ref Vector3 vector) { GL.Uniform3(Location, ref vector); }

        #endregion

        #region Uniform4

        public void SetUniform4(float x, float y, float z, float w) { GL.Uniform4(Location, x, y, z, w); }
        public void SetUniform4(double x, double y, double z, double w) { GL.Uniform4(Location, x, y, z, w); }
        public void SetUniform4(uint x, uint y, uint z, uint w) { GL.Uniform4(Location, x, y, z, w); }
        public void SetUniform4(int x, int y, int z, int w) { GL.Uniform4(Location, x, y, z, w); }

        public void SetUniform4(int count, params float[] values) { GL.Uniform4(Location, count, values); }
        public void SetUniform4(int count, params double[] values) { GL.Uniform4(Location, count, values); }
        public void SetUniform4(int count, params int[] values) { GL.Uniform4(Location, count, values); }
        public void SetUniform4(int count, params uint[] values) { GL.Uniform4(Location, count, values); }

        public void SetUniform4(int count, ref float values) { GL.Uniform4(Location, count, ref values); }
        public void SetUniform4(int count, ref double values) { GL.Uniform4(Location, count, ref values); }
        public void SetUniform4(int count, ref uint values) { GL.Uniform4(Location, count, ref values); }

        public void SetUniform4(Vector4 vector) { GL.Uniform4(Location, vector); }
        public void SetUniform4(ref Vector4 vector) { GL.Uniform4(Location, ref vector); }

        public void SetUniform4(Color4 color) { GL.Uniform4(Location, color); }
        public void SetUniform4(Quaternion quaternion) { GL.Uniform4(Location, quaternion); }

        #endregion

        #region Matrix2

        public void SetMatrix2(ref Matrix2 matrix, bool transpose = false) { GL.UniformMatrix2(Location, transpose, ref matrix); }

        public void SetMatrix2(int count, ref double value, bool transpose = false) { GL.UniformMatrix2(Location, count, transpose, ref value); }
        public void SetMatrix2(int count, ref float value, bool transpose = false) { GL.UniformMatrix2(Location, count, transpose, ref value); }

        public void SetMatrix2(int count, double[] value, bool transpose = false) { GL.UniformMatrix2(Location, count, transpose, value); }
        public void SetMatrix2(int count, float[] value, bool transpose = false) { GL.UniformMatrix2(Location, count, transpose, value); }

        #endregion

        #region Matrix3

        public void SetMatrix3(ref Matrix3 matrix, bool transpose = false) { GL.UniformMatrix3(Location, transpose, ref matrix); }

        public void SetMatrix3(int count, ref double value, bool transpose = false) { GL.UniformMatrix3(Location, count, transpose, ref value); }
        public void SetMatrix3(int count, ref float value, bool transpose = false) { GL.UniformMatrix3(Location, count, transpose, ref value); }

        public void SetMatrix3(int count, double[] value, bool transpose = false) { GL.UniformMatrix3(Location, count, transpose, value); }
        public void SetMatrix3(int count, float[] value, bool transpose = false) { GL.UniformMatrix3(Location, count, transpose, value); }

        #endregion

        #region Matrix4

        public void SetMatrix4(Matrix4 matrix, bool transpose = false)
        {
            GL.UniformMatrix4(Location, transpose, ref matrix);
        }
        public void SetMatrix4(ref Matrix4 matrix, bool transpose = false) { GL.UniformMatrix4(Location, transpose, ref matrix); }

        public void SetMatrix4(int count, ref double value, bool transpose = false) { GL.UniformMatrix4(Location, count, transpose, ref value); }
        public void SetMatrix4(int count, ref float value, bool transpose = false) { GL.UniformMatrix4(Location, count, transpose, ref value); }

        public void SetMatrix4(int count, double[] value, bool transpose = false) { GL.UniformMatrix4(Location, count, transpose, value); }
        public void SetMatrix4(int count, float[] value, bool transpose = false) { GL.UniformMatrix4(Location, count, transpose, value); }

        #endregion

        public void SetTexture(TextureBase texture, Uniform checkUniform)
        {
            checkUniform.SetUniform1(texture != null);
            if (texture != null) SetTexture(texture);
        }

        public void SetTexture(TextureBase texture, int pos, Uniform checkUniform)
        {
            checkUniform.SetUniform1(texture != null);
            if (texture != null) SetTexture(texture);
        }
        public void SetTexture(TextureBase texture) => SetTexture(texture, Parent.NextTexture++);

        public void SetTexture(TextureBase texture, int texturePos)
        {
            GL.ActiveTexture(TextureUnit.Texture0 + texturePos);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            SetUniform1(texturePos);
        }

        public static implicit operator int(Uniform u) => u.Location;
    }
}