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
            /// <summary>
            /// Surprise! Should't, but can be null! E.g. in `byronartset1` collection.
            /// </summary>
            public int? TemplateId { get; set; }

            public int Burned { get; set; }
        }

        public class BurnedBySchemaItem
        {
            public string SchemaName { get; set; } = string.Empty;

            public long Burned { get; set; }
        }
    }
}
