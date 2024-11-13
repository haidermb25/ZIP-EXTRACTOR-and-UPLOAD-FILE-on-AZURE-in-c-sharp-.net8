namespace Image_Convertor.Models
{
    public class fileUpload
    {
        public string Name { get; set; }
        public IFormFile file { get; set; }
    }
    public class IconPack
    {
        public int ID { get; set; }
        public string PackName { get; set; }
        public string PackType { get; set; }
        public int Payment { get; set; }
    }

    public class Icon
    {
        public int IconID { get; set; }
        public int IconPackID { get; set; }
        public string IconFile { get; set; }
        public string IconName { get; set; }
    }
}
