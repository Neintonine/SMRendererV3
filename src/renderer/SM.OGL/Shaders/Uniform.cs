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

        #region Constructors
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

        #endregion

        #region Uniform1

        /// <summary>
        /// Set a boolean as value.
        /// </summary>
        /// <param name="value"></param>
        public void SetBool(bool value)
        {
            GL.Uniform1(Location, value ? 1 : 0);
        }

        /// <summary>
        /// Sets a integer.
        /// </summary>
        /// <param name="value"></param>
        public void SetInt(int value)
        {
            GL.Uniform1(Location, value);
        }

        /// <summary>
        /// Sets an array of integers.
        /// </summary>
        /// <param name="values"></param>
        public void SetInt(params int[] values)
        {
            GL.Uniform1(Location, values.Length, values);
        }

        /// <summary>
        /// Set a unsigned integer.
        /// </summary>
        /// <param name="value"></param>
        public void SetUInt(uint value)
        {
            GL.Uniform1(Location, value);
        }

        /// <summary>
        /// Set an array of unsigned integers.
        /// </summary>
        /// <param name="values"></param>
        public void SetUInt(params uint[] values)
        {
            GL.Uniform1(Location, values.Length, values);
        }

        /// <summary>
        /// Sets a float.
        /// </summary>
        /// <param name="value"></param>
        public void SetFloat(float value)
        {
            GL.Uniform1(Location, value);
        }

        /// <summary>
        /// Sets an array of floats.
        /// </summary>
        /// <param name="values"></param>
        public void SetFloat(params float[] values)
        {
            GL.Uniform1(Location, values.Length, values);
        }

        #endregion

        #region Uniform2

        /// <summary>
        /// Sets a float vector2 by providing the values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetVector2(float x, float y)
        {
            GL.Uniform2(Location, x, y);
        }

        /// <summary>
        /// Sets a unsigned integer vector2 by providing the values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetVector2(uint x, uint y)
        {
            GL.Uniform2(Location, x, y);
        }

        /// <summary>
        /// Sets a integer vector2 by providing the values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetVector2(int x, int y)
        {
            GL.Uniform2(Location, x, y);
        }

        /// <summary>
        /// Sets a float vector2.
        /// </summary>
        /// <param name="vector2"></param>
        public void SetVector2(Vector2 vector2)
        {
            GL.Uniform2(Location, vector2);
        }

        /// <summary>
        /// Sets a float vector2 by refencing.
        /// </summary>
        /// <param name="vector2"></param>
        public void SetVector2(ref Vector2 vector2)
        {
            GL.Uniform2(Location, ref vector2);
        }

        /// <summary>
        /// Sets a array of vector2.
        /// </summary>
        /// <param name="values"></param>
        public void SetVector2(params Vector2[] values)
        {
            float[] newValues = new float[values.Length * 2];
            for(int i = 0; i < values.Length; i++)
            {
                Vector2 val = values[i];
                int newi = i * 2;
                newValues[newi] = val.X;
                newValues[newi + 1] = val.Y;
            }
            GL.Uniform2(Location, values.Length, newValues);
        }

        /// <summary>
        /// Sets a float array that get converted to a vector2 array.
        /// </summary>
        /// <param name="values"></param>
        public void SetVector2(params float[] values)
        {
            GL.Uniform2(Location, values.Length / 2, values);
        }

        /// <summary>
        /// Sets a integer array that get converted to a ivector2 array.
        /// </summary>
        /// <param name="values"></param>
        public void SetVector2(params int[] values)
        {
            GL.Uniform2(Location, values.Length / 2, values);
        }

        /// <summary>
        /// Sets a unsigned integer array that get converted to a unsigned integer vector2 array.
        /// </summary>
        /// <param name="values"></param>
        public void SetVector2(params uint[] values)
        {
            GL.Uniform2(Location, values.Length / 2, values);
        }

        #endregion

        #region Uniform3

        /// <summary>
        /// Sets a float vector3 by providing the values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void SetVector3(float x, float y, float z)
        {
            GL.Uniform3(Location, x, y, z);
        }

        /// <summary>
        /// Sets a unsigned integer vector3 by providing the values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void SetVector3(uint x, uint y, uint z)
        {
            GL.Uniform3(Location, x, y, z);
        }

        /// <summary>
        /// Sets a integer vector3 by providing the values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void SetVector3(int x, int y, int z)
        {
            GL.Uniform3(Location, x, y, z);
        }

        /// <summary>
        /// Sets a vector3.
        /// </summary>
        /// <param name="vector"></param>
        public void SetVector3(Vector3 vector)
        {
            GL.Uniform3(Location, vector);
        }
        /// <summary>
        /// Sets a vector3 by reference.
        /// </summary>
        /// <param name="vector"></param>
        public void SetVector3(ref Vector3 vector)
        {
            GL.Uniform3(Location, ref vector);
        }

        /// <summary>
        /// Sets a array of vector3.
        /// </summary>
        /// <param name="values"></param>
        public void SetVector3(params Vector3[] values)
        {
            float[] newValues = new float[values.Length * 3];
            for (int i = 0; i < values.Length; i++)
            {
                Vector3 val = values[i];
                int newi = i * 3;
                newValues[newi] = val.X;
                newValues[newi + 1] = val.Y;
                newValues[newi + 2] = val.Z;
            }
            GL.Uniform3(Location, values.Length, newValues);
        }

        /// <summary>
        /// Sets a array by providing the components.
        /// </summary>
        /// <param name="values"></param>
        public void SetVector3(params float[] values)
        {
            GL.Uniform3(Location, values.Length / 3, values);
        }
        /// <summary>
        /// Sets a array by providing the components.
        /// </summary>
        /// <param name="values"></param>
        public void SetVector3(params int[] values)
        {
            GL.Uniform3(Location, values.Length / 3, values);
        }
        /// <summary>
        /// Sets a array by providing the components.
        /// </summary>
        /// <param name="values"></param>
        public void SetVector3(params uint[] values)
        {
            GL.Uniform3(Location, values.Length / 3, values);
        }


        #endregion

        #region Uniform4

        /// <summary>
        /// Sets a vector4 by providing the values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public void SetVector4(float x, float y, float z, float w)
        {
            GL.Uniform4(Location, x, y, z, w);
        }
        /// <summary>
        /// Sets a vector4 by providing the values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public void SetVector4(uint x, uint y, uint z, uint w)
        {
            GL.Uniform4(Location, x, y, z, w);
        }
        /// <summary>
        /// Sets a vector4 by providing the values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public void SetVector4(int x, int y, int z, int w)
        {
            GL.Uniform4(Location, x, y, z, w);
        }

        /// <summary>
        /// Sets a vector4.
        /// </summary>
        /// <param name="vector"></param>
        public void SetVector4(Vector4 vector)
        {
            GL.Uniform4(Location, vector);
        }

        /// <summary>
        /// Sets a vector4.
        /// </summary>
        /// <param name="vector"></param>
        public void SetVector4(ref Vector4 vector)
        {
            GL.Uniform4(Location, ref vector);
        }

        /// <summary>
        /// Sets a array of Vector4.
        /// </summary>
        /// <param name="values"></param>
        public void SetVector4(params Vector4[] values)
        {
            float[] newValues = new float[values.Length * 4];
            for (int i = 0; i < values.Length; i++)
            {
                Vector4 val = values[i];
                int newi = i * 3;
                newValues[newi] = val.X;
                newValues[newi + 1] = val.Y;
                newValues[newi + 2] = val.Z;
                newValues[newi + 3] = val.W;
            }
            GL.Uniform3(Location, values.Length, newValues);
        }

        /// <summary>
        /// Sets a array by providing the components.
        /// </summary>
        /// <param name="values"></param>
        public void SetVector4(params float[] values)
        {
            GL.Uniform4(Location, values.Length / 4, values);
        }
        /// <summary>
        /// Sets a array by providing the components.
        /// </summary>
        /// <param name="values"></param>
        public void SetVector4(params int[] values)
        {
            GL.Uniform4(Location, values.Length / 4, values);
        }       
        /// <summary>
        /// Sets a array by providing the components.
        /// </summary>
        /// <param name="values"></param>
        public void SetVector4(params uint[] values)
        {
            GL.Uniform4(Location, values.Length / 4, values);
        }

        /// <summary>
        /// Sets a quaternion.
        /// </summary>
        /// <param name="quaternion"></param>
        public void SetQuaternion(Quaternion quaternion)
        {
            GL.Uniform4(Location, quaternion);
        }

        #endregion

        #region Matrix2

        /// <summary>
        /// Sets a matrix2.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="transpose">If true, the matrix will be transposed.</param>
        public void SetMatrix2(Matrix2 matrix, bool transpose = false)
        {
            GL.UniformMatrix2(Location, transpose, ref matrix);
        }

        /// <summary>
        /// Sets a matrix2 array.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="value"></param>
        /// <param name="transpose">If true, the matrix will be transposed.</param>
        public void SetMatrix2(int count, float[] value, bool transpose = false)
        {
            GL.UniformMatrix2(Location, count, transpose, value);
        }

        #endregion

        #region Matrix3
        /// <summary>
        /// Sets a matrix3.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="transpose">If true, the matrix will be transposed.</param>
        public void SetMatrix3(Matrix3 matrix, bool transpose = false)
        {
            GL.UniformMatrix3(Location, transpose, ref matrix);
        }

        /// <summary>
        /// Sets a matrix3 array.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="value"></param>
        /// <param name="transpose">If true, the matrix will be transposed.</param>
        public void SetMatrix3(int count, float[] value, bool transpose = false)
        {
            GL.UniformMatrix3(Location, count, transpose, value);
        }

        #endregion

        #region Matrix4

        /// <summary>
        /// Sets a matrix4.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="transpose">If true, the matrix will be transposed.</param>
        public void SetMatrix4(Matrix4 matrix, bool transpose = false)
        {
            GL.UniformMatrix4(Location, transpose, ref matrix);
        }
        /// <summary>
        /// Sets a matrix4 by reference.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="transpose">If true, the matrix will be transposed.</param>
        public void SetMatrix4(ref Matrix4 matrix, bool transpose = false)
        {
            GL.UniformMatrix4(Location, transpose, ref matrix);
        }

        /// <summary>
        /// Sets a matrix4 array.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="value"></param>
        /// <param name="transpose">If true, the matrix will be transposed.</param>
        public void SetMatrix4(int count, float[] value, bool transpose = false)
        {
            GL.UniformMatrix4(Location, count, transpose, value);
        }

        #endregion

        /// <summary>
        /// Sets the color.
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(Color4 color)
        {
            GL.Uniform4(Location, color);
        }

        /// <summary>
        ///     Try to sets the texture at the next possible position and tells the checkUniform, if worked or not.
        /// </summary>
        /// <param name="texture">The texture you want to add</param>
        /// <param name="checkUniform">The check uniform.</param>
        public void SetTexture(TextureBase texture, Uniform checkUniform)
        {
            checkUniform.SetBool(texture != null);
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
            checkUniform.SetBool(texture != null);
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
            SetInt(texturePos);
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