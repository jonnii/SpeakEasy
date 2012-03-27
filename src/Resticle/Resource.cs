namespace Resticle
{
    public class Resource
    {
        private readonly string path;

        public Resource(string path)
        {
            this.path = path;
        }

        public int NumParameters
        {
            get { return 0; }
        }

        public bool HasParameter(string name)
        {
            return true;
        }

        public string Merge(object segments)
        {
            return string.Empty;
        }
    }
}
