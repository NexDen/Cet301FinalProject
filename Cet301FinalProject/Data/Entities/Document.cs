using SQLite;

namespace Cet301FinalProject.Data.Entities;

[Table("documents")]

public class Document
{
    [PrimaryKey]
    public string Id { get; set; }
    [Column("created_date")]
    public DateTime CreatedDate { get; set; }
    [Column("doc_file_path")]
    public string FilePath { get; set; }
    [Column("doc_type")]
    public DocumentType DocType { get; set; }
}

public enum DocumentType
{
    License = 0,
    Certificate = 1,
    DeliveryDocument = 2
}