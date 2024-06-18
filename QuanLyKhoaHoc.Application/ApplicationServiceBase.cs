namespace QuanLyKhoaHoc.Application
{
    public abstract class ApplicationServiceBase<Model, QueryModel, CreateModel, UpdateModel> : IApplicationService<Model, QueryModel, CreateModel, UpdateModel>
    {
        public IApplicationDbContext _context { get; set; }

        public IMapper _mapper { get; set; }

        public IUser _user { get; set; }

        public ApplicationServiceBase(IApplicationDbContext context, IMapper mapper, IUser user)
        {
            _context = context;
            _mapper = mapper;
            _user = user;
        }

        public abstract Task<PagingModel<Model>> Get(QueryModel query, CancellationToken cancellation);

        public abstract Task<Model?> Get(int id, CancellationToken cancellation);

        public abstract Task<Result> Create(CreateModel entity, CancellationToken cancellation);

        public abstract Task<Result> Update(int id, UpdateModel entity, CancellationToken cancellation);

        public abstract Task<Result> Delete(int id, CancellationToken cancellation);
    }
}
