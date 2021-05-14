#region usings

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SM.OGL.Texture;

#endregion

namespace SM.OGL.Shaders
{
    /// <summary>
    ///     Manages the uniforms.
    /// </summary>
    public struct Uniform : IUniform
    {
        /// <summary>
        ///     This contains the location for the uniform.
        /// </summary>
        public int Location { get; internal set; }

        /// <summary>
        ///     This contains the Parent collection of this uniform.
        /// </summary>
        public UniformCollection Parent { get; }

        /// <summary>
        ///     This creates a new uniform manager, that has a null parent.
        /// </summary>
        /// <param name="location"></param>
        public Uniform(int location) : this(location, null)
        {
        }

        /// <summary>
        ///     This creates a new uniform manager, that get the location from the provided shader and with a null parent.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="shader"></param>
        public Uniform(string name, GenericShader shader) : this(GL.GetUniformLocation(shader, name), null)
        {

        }
        /// <summary>
        ///     This creates a new uniform manager, that get the location from the provided shader and with a parent.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="shader"></param>
        /// <param name="parent"></param>
        public Uniform(string name, GenericShader shader, UniformCollection parent) : this(GL.GetUniformLocation(shader, name), parent)
        {

        }

        /// <summary>
        ///     This create a new uniform manager
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

        public void SetUniform1(int value)
        {
            GL.Uniform1(Location, value);
        }

        public void SetUniform1(params int[] values)
        {
            GL.Uniform1(Location, values.Length, values);
        }

        public void SetUniform1(int count, ref int values)
        {
            GL.Uniform1(Location, count, ref values);
        }


        public void SetUniform1(uint value)
        {
            GL.Uniform1(Location, value);
        }

        public void SetUniform1(params uint[] values)
        {
            GL.Uniform1(Location, values.Length, values);
        }

        public void SetUniform1(int count, ref uint values)
        {
            GL.Uniform1(Location, count, ref values);
        }


        public void SetUniform1(float value)
        {
            GL.Uniform1(Location, value);
        }

        public void SetUniform1(params float[] values)
        {
            GL.Uniform1(Location, values.Length, values);
        }

        public void SetUniform1(int count, ref float value)
        {
            GL.Uniform1(Location, count, ref value);
        }


        public void SetUniform1(double value)
        {
            GL.Uniform1(Location, value);
        }

        public void SetUniform1(params double[] values)
        {
            GL.Uniform1(Location, values.Length, values);
        }

        public void SetUniform1(int count, ref double value)
        {
            GL.Uniform1(Location, count, ref value);
        }

        #endregion

        #region Uniform2

        public void SetUniform2(float x, float y)
        {
            GL.Uniform2(Location, x, y);
        }

        public void SetUniform2(double x, double y)
        {
            GL.Uniform2(Location, x, y);
        }

        public void SetUniform2(uint x, uint y)
        {
            GL.Uniform2(Location, x, y);
        }

        public void SetUniform2(int x, int y)
        {
            GL.Uniform2(Location, x, y);
        }

        public void SetUniform2(params float[] values)
        {
            GL.Uniform2(Location, values.Length / 2, values);
        }

        public void SetUniform2(params double[] values)
        {
            GL.Uniform2(Location, values.Length / 2, values);
        }

        public void SetUniform2(params int[] values)
        {
            GL.Uniform2(Location, values.Length / 2, values);
        }

        public void SetUniform2(params uint[] values)
        {
            GL.Uniform2(Location, values.Length / 2, values);
        }

        public void SetUniform2(int count, ref float values)
        {
            GL.Uniform2(Location, count, ref values);
        }

        public void SetUniform2(int count, ref double values)
        {
            GL.Uniform2(Location, count, ref values);
        }

        public void SetUniform2(int count, ref uint values)
        {
            GL.Uniform2(Location, count, ref values);
        }

        public void SetUniform2(Vector2 vector2)
        {
            GL.Uniform2(Location, vector2);
        }

        public void SetUniform2(ref Vector2 vector2)
        {
            GL.Uniform2(Location, ref vector2);
        }

        #endregion

        #region Uniform3

        public void SetUniform3(float x, float y, float z)
        {
            GL.Uniform3(Location, x, y, z);
        }

        public void SetUniform3(double x, double y, double z)
        {
            GL.Uniform3(Location, x, y, z);
        }

        public void SetUniform3(uint x, uint y, uint z)
        {
            GL.Uniform3(Location, x, y, z);
        }

        public void SetUniform3(int x, int y, int z)
        {
            GL.Uniform3(Location, x, y, z);
        }

        public void SetUniform3(params float[] values)
        {
            GL.Uniform3(Location, values.Length / 3, values);
        }

        public void SetUniform3(params double[] values)
        {
            GL.Uniform3(Location, values.Length / 3, values);
        }

        public void SetUniform3(params int[] values)
        {
            GL.Uniform3(Location, values.Length / 3, values);
        }

        public void SetUniform3(params uint[] values)
        {
            GL.Uniform3(Location, values.Length / 3, values);
        }

        public void SetUniform3(int count, ref float values)
        {
            GL.Uniform3(Location, count, ref values);
        }

        public void SetUniform3(int count, ref double values)
        {
            GL.Uniform3(Location, count, ref values);
        }

        public void SetUniform3(int count, ref uint values)
        {
            GL.Uniform3(Location, count, ref values);
        }

        public void SetUniform3(Vector3 vector)
        {
            GL.Uniform3(Location, vector);
        }

        public void SetUniform3(ref Vector3 vector)
        {
            GL.Uniform3(Location, ref vector);
        }

        #endregion

        #region Uniform4

        public void SetUniform4(float x, float y, float z, float w)
        {
            GL.Uniform4(Location, x, y, z, w);
        }

        public void SetUniform4(double x, double y, double z, double w)
        {
            GL.Uniform4(Location, x, y, z, w);
        }

        public void SetUniform4(uint x, uint y, uint z, uint w)
        {
            GL.Uniform4(Location, x, y, z, w);
        }

        public void SetUniform4(int x, int y, int z, int w)
        {
            GL.Uniform4(Location, x, y, z, w);
        }

        public void SetUniform4(params float[] values)
        {
            GL.Uniform4(Location, values.Length / 4, values);
        }

        public void SetUniform4(params double[] values)
        {
            GL.Uniform4(Location, values.Length / 4, values);
        }

        public void SetUniform4(params int[] values)
        {
            GL.Uniform4(Location, values.Length / 4, values);
        }

        public void SetUniform4(params uint[] values)
        {
            GL.Uniform4(Location, values.Length / 4, values);
        }

        public void SetUniform4(int count, ref float values)
        {
            GL.Uniform4(Location, count, ref values);
        }

        public void SetUniform4(int count, ref double values)
        {
            GL.Uniform4(Location, count, ref values);
        }

        public void SetUniform4(int count, ref uint values)
        {
            GL.Uniform4(Location, count, ref values);
        }

        public void SetUniform4(Vector4 vector)
        {
            GL.Uniform4(Location, vector);
        }

        public void SetUniform4(ref Vector4 vector)
        {
            GL.Uniform4(Location, ref vector);
        }

        public void SetUniform4(Color4 color)
        {
            GL.Uniform4(Location, color);
        }

        public void SetUniform4(Quaternion quaternion)
        {
            GL.Uniform4(Location, quaternion);
        }

        #endregion

        #region Matrix2

        public void SetMatrix2(Matrix2 matrix, bool transpose = false)
        {
            GL.UniformMatrix2(Location, transpose, ref matrix);
        }

        public void SetMatrix2(int count, ref double value, bool transpose = false)
        {
            GL.UniformMatrix2(Location, count, transpose, ref value);
        }

        public void SetMatrix2(int count, ref float value, bool transpose = false)
        {
            GL.UniformMatrix2(Location, count, transpose, ref value);
        }

        public void SetMatrix2(int count, double[] value, bool transpose = false)
        {
            GL.UniformMatrix2(Location, count, transpose, value);
        }

        public void SetMatrix2(int count, float[] value, bool transpose = false)
        {
            GL.UniformMatrix2(Location, count, transpose, value);
        }

        #endregion

        #region Matrix3

        public void SetMatrix3(Matrix3 matrix, bool transpose = false)
        {
            GL.UniformMatrix3(Location, transpose, ref matrix);
        }

        public void SetMatrix3(int count, ref double value, bool transpose = false)
        {
            GL.UniformMatrix3(Location, count, transpose, ref value);
        }

        public void SetMatrix3(int count, ref float value, bool transpose = false)
        {
            GL.UniformMatrix3(Location, count, transpose, ref value);
        }

        public void SetMatrix3(int count, double[] value, bool transpose = false)
        {
            GL.UniformMatrix3(Location, count, transpose, value);
        }

        public void SetMatrix3(int count, float[] value, bool transpose = false)
        {
            GL.UniformMatrix3(Location, count, transpose, value);
        }

        #endregion

        #region Matrix4

        public void SetMatrix4(Matrix4 matrix, bool transpose = false)
        {
            GL.UniformMatrix4(Location, transpose, ref matrix);
        }

        public void SetMatrix4(ref Matrix4 matrix, bool transpose = false)
        {
            GL.UniformMatrix4(Location, transpose, ref matrix);
        }

        public void SetMatrix4(int count, ref double value, bool transpose = false)
        {
            GL.UniformMatrix4(Location, count, transpose, ref value);
        }

        public void SetMatrix4(int count, ref float value, bool transpose = false)
        {
            GL.UniformMatrix4(Location, count, transpose, ref value);
        }

        public void SetMatrix4(int count, double[] value, bool transpose = false)
        {
            GL.UniformMatrix4(Location, count, transpose, value);
        }

        public void SetMatrix4(int count, float[] value, bool transpose = false)
        {
            GL.UniformMatrix4(Location, count, transpose, value);
        }

        #endregion

        /// <summary>
        ///     Try to sets the texture at the next possible position and tells the checkUniform, if worked or not.
        /// </summary>
        /// <param name="texture">The texture you want to add</param>
        /// <param name="checkUniform">The check uniform.</param>
        public void SetTexture(TextureBase texture, Uniform checkUniform)
        {
            checkUniform.SetUniform1(texture != null);
            if (texture != null) SetTexture(texture);
        }

        /// <summary>
        ///     Try to sets the texture at the specified position and tells the checkUniform, if worked or not.
        /// </summary>
        /// <param name="texture">The texture you want to add</param>
        /// <param name="pos">The position</param>
        /// <param name="checkUniform">The check uniform.</param>
        public void SetTexture(TextureBase texture, int pos, Uniform checkUniform)
        {
            checkUniform.SetUniform1(texture != null);
            if (texture != null) SetTexture(texture);
        }

        /// <summary>
        ///     Sets the texture to the next possible position.
        /// </summary>
        /// <param name="texture"></param>
        public void SetTexture(TextureBase texture)
        {
            if (Parent != null) SetTexture(texture, Parent.NextTexture);
        }

        /// <summary>
        ///     Sets the texture to the specified position.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="texturePos"></param>
        public void SetTexture(TextureBase texture, int texturePos)
        {
            Parent.NextTexture = texturePos + 1;
            GL.ActiveTexture(TextureUnit.Texture0 + texturePos);
            GL.BindTexture(texture.Target, texture);
            SetUniform1(texturePos);
        }

        /// <summary>
        ///     Returns the location from the uniform
        /// </summary>
        /// <param name="u"></param>
        public static implicit operator int(Uniform u)
        {
            return u.Location;
        }
    }
}