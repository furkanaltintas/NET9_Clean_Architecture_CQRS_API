namespace Core.Persistence.Dynamic;

public class DynamicQuery
{
    public DynamicQuery()
    {

    }

    public DynamicQuery(IEnumerable<Sort>? sort, Filter? filter)
    {
        Sort = sort;
        Filter = filter;
    }

    public IEnumerable<Sort>? Sort { get; set; }
    public Filter? Filter { get; set; }
}

// ADO: select * from Cars where unitPrice < 100 and (transmission = '1' or vb...)
// p => p.unitPrice < 100 && (p.transmission = 1 vb...)