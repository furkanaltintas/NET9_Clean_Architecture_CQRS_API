using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories;

public interface IRepository<TEntity, TEntityId> : IQuery<TEntity>
    where TEntity : Entity<TEntityId>
{
    // TEntity => Veri tabanında ki bir varlığı temsil eder. Örnek: Product
    // TEntityId => Varlığın birincil anahtar tipini belirtir. Örnek: int, Guid
    // IQuery<TEntity> => Sorgu işlemlerini genişletmek için kullanılmıştır.
    // where TEntity : Entity<TEntityId> => TEntity sınıfının Entity<TEntityId>'den türetilmesi gerektiğini belirtir.


    TEntity? Get(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool withDeleted = false,
            bool enableTracking = true);


    Paginate<TEntity> GetList(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true);


    /*
    Tek bir varlığı döndürür bulamazsa null bir değer olabilir.
    predicate: Belirli bir koşula göre sorgulama yapar
    include: İlişkili tabloları yüklemek için kullanılır.
    withDeleted: Silinmiş kayıtları dahil edip etmeyeceğini belirler.
    enableTracking: EF Core'un takip mekanizmasını açıp kapatır,
    index: Hangi sayfanın istenildiğini belirtir.
    size: Sayfa başına kaç kayıt döndürüleceğini belirler
    Paginate<TEntity>: Sonuçları bir sayfalama nesnesi içinde döndürür
    DynamicQuery: Kullanıcıdan gelen isteğe göre filtreleme/sıralama yapmayı sağlar.
    */


    Paginate<TEntity> GetListByDynamic(
        DynamicQuery dynamic,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true);


    bool Any(
        Expression<Func<TEntity, bool>>? predicate = null,
        bool withDeleted = false,
        bool enableTracking = true);


    TEntity Add(TEntity entity);


    ICollection<TEntity> AddRange(ICollection<TEntity> entities);


    TEntity Update(TEntity entity);


    ICollection<TEntity> UpdateRange(ICollection<TEntity> entities);


    TEntity Delete(TEntity entity, bool permanent = false);


    ICollection<TEntity> DeleteRange(ICollection<TEntity> entity, bool permanent = false);
}