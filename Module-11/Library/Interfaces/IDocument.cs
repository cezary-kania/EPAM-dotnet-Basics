namespace Library.Interfaces;

public interface IDocument
{
    public int DocumentNumber { get; set; }
    public string GetCardInfo();
}