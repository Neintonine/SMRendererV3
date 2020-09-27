namespace SM.OGL
{
    /// <summary>
    /// Helper struct to manage versions.
    /// </summary>
    public struct Version
    {
        /// <summary>
        /// The major version.
        /// </summary>
        public int MajorVersion;
        /// <summary>
        /// The minor version.
        /// </summary>
        public int MinorVersion;

        /// <summary>
        /// Creates the struct with specific major and minor versions.
        /// </summary>
        /// <param name="majorVersion"></param>
        /// <param name="minorVersion"></param>
        public Version(int majorVersion, int minorVersion)
        {
            MinorVersion = minorVersion;
            MajorVersion = majorVersion;
        }

        /// <summary>
        /// Creates the struct by reading it out of a string.
        /// </summary>
        /// <param name="version"></param>
        public Version(string version)
        {
            string[] splits = version.Trim().Split(new []{'.'}, 2);
            MajorVersion = int.Parse(splits[0]);
            MinorVersion = int.Parse(splits[1]);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{MajorVersion}.{MinorVersion}";
        }

        /// <summary>
        /// Create a version struct, with a OpenGL Version string.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static Version CreateGLVersion(string version)
        {
            return new Version(version.Substring(0, 3));
        }
    }
}