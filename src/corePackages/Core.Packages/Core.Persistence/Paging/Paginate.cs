namespace Core.Persistence.Paging;

public class Paginate<T>
{
    public Paginate()
    {
        Items = Array.Empty<T>();
    }

    public int Size { get; set; } // Sayfamızda ki data sayısı
    public int Index { get; set; } // Bulunduğumuz sayfa
    public int Count { get; set; } // Toplam kayıt sayısı
    public int Pages { get; set; } // Toplam kaç sayfa var
    public IList<T> Items { get; set; }
    public bool HasPrevious => Index > 0;
    public bool HasNext => Index + 1 < Pages;
}