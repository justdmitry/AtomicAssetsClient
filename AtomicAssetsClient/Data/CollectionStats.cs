namespace AtomicAssetsClient.Data
{
    public class CollectionStats
    {
        public long Assets { get; set; }

        public long Burned { get; set; }

        public int Templates { get; set; }

        public int Schemas { get; set; }

        public List<BurnedByTemplateItem> BurnedByTemplate { get; set; } = new List<BurnedByTemplateItem>();

        public List<BurnedBySchemaItem> BurnedBySchema { get; set; } = new List<BurnedBySchemaItem>();

        public class BurnedByTemplateItem
        {
            public int TemplateId { get; set; }

            public long Burned { get; set; }
        }

        public class BurnedBySchemaItem
        {
            public string SchemaName { get; set; } = string.Empty;

            public long Burned { get; set; }
        }
    }
}
