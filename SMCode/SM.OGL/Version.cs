namespace SM.OGL
{
    public struct Version
    {
        public int MajorVersion;
        public int MinorVersion;

        public Version(int majorVersion, int minorVersion)
        {
            MinorVersion = minorVersion;
            MajorVersion = majorVersion;
        }

        public Version(string version)
        {
            string[] splits = version.Trim().Split(new []{'.'}, 2);
            MajorVersion = int.Parse(splits[0]);
            MinorVersion = int.Parse(splits[1]);
        }

        public override string ToString()
        {
            return $"{MajorVersion}.{MinorVersion}";
        }

        public static Version CreateGLVersion(string version)
        {
            return new Version(version.Substring(0, 3));
        }
    }
}