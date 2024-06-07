using QuanLyKhoaHoc.Application.Common.Models;
namespace QuanLyKhoaHoc.Application.Common.Interfaces
{
    public interface IApplicationService<Model, QueryModel, CreateModel, UpdateModel>
    {
        Task<PagingModel<Model>> Get(QueryModel query, CancellationToken cancellation);

        Task<Model?> Get(int id, CancellationToken cancellation);

        Task<Result> Create(CreateModel entity, CancellationToken cancellation);

        Task<Result> Update(int id, UpdateModel entity, CancellationToken cancellation);

        Task<Result> Delete(int id, CancellationToken cancellation);
    }
}