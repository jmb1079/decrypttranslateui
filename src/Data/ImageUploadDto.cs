namespace DecryptTranslateUi.Data
{
    public class ImageUploadDto
    {
        public int CaseNumber { get; set; }
        public string ContainerName { get; set; }
        public string FileName { get; set; }
        public BinaryData Content { get; set; }
    }
}