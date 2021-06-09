namespace SMCL.Models
{
    public enum ManifestLevel
    {
        Required,
        Banned,
    }

    public class ManifestItem
    {
        public string FilePath;
        public string Url;
        public string MD5;
        public ManifestLevel Level;
    }
}