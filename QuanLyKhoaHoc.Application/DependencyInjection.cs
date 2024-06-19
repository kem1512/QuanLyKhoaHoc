namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<ApplicationServiceBase<SubjectMapping, SubjectQuery, SubjectCreate, SubjectUpdate>, SubjectService>();

        services.AddScoped<ApplicationServiceBase<CourseMapping, CourseQuery, CourseCreate, CourseUpdate>, CourseService>();

        services.AddScoped<ApplicationServiceBase<BlogMapping, BlogQuery, BlogCreate, BlogUpdate>, BlogService>();

        services.AddScoped<ApplicationServiceBase<ProvinceMapping, ProvinceQuery, ProvinceCreate, ProvinceUpdate>, ProvinceService>();

        services.AddScoped<ApplicationServiceBase<DistrictMapping, DistrictQuery, DistrictCreate, DistrictUpdate>, DistrictService>();

        services.AddScoped<ApplicationServiceBase<UserMapping, UserQuery, UserCreate, UserUpdate>, UserService>();

        services.AddScoped<ApplicationServiceBase<WardMapping, WardQuery, WardCreate, WardUpdate>, WardService>();

        services.AddScoped<ApplicationServiceBase<RoleMapping, RoleQuery, RoleCreate, RoleUpdate>, RoleService>();

        services.AddScoped<ApplicationServiceBase<BillMapping, BillQuery, BillCreate, BillUpdate>, BillService>();

        services.AddScoped<ApplicationServiceBase<CourseSubjectMapping, CourseSubjectQuery, CourseSubjectCreate, CourseSubjectUpdate>, CourseSubjectService>();

        services.AddScoped<ApplicationServiceBase<BillStatusMapping, BillStatusQuery, BillStatusCreate, BillStatusUpdate>, BillStatusService>();

        services.AddScoped<ApplicationServiceBase<SubjectDetailMapping, SubjectDetailQuery, SubjectDetailCreate, SubjectDetailUpdate>, SubjectDetailService>();

        services.AddScoped<ApplicationServiceBase<ProgramingLanguageMapping, ProgramingLanguageQuery, ProgramingLanguageCreate, ProgramingLanguageUpdate>, ProgramingLanguageService>();

        services.AddScoped<ApplicationServiceBase<TestCaseMapping, TestCaseQuery, TestCaseCreate, TestCaseUpdate>, TestCaseService>();

        services.AddScoped<ApplicationServiceBase<CertificateMapping, CertificateQuery, CertificateCreate, CertificateUpdate>, CertificateService>();

        services.AddScoped<ApplicationServiceBase<PracticeMapping, PracticeQuery, PracticeCreate, PracticeUpdate>, PracticeService>();

        services.AddScoped<ApplicationServiceBase<CertificateTypeMapping, CertificateTypeQuery, CertificateTypeCreate, CertificateTypeUpdate>, CertificateTypeService>();

        services.AddScoped<ApplicationServiceBase<CommentBlogMapping, CommentBlogQuery, CommentBlogCreate, CommentBlogUpdate>, CommentBlogService>();

        services.AddScoped<ApplicationServiceBase<LikeBlogMapping, LikeBlogQuery, LikeBlogCreate, LikeBlogUpdate>, LikeBlogService>();

        services.AddScoped<ApplicationServiceBase<RegisterStudyMapping, RegisterStudyQuery, RegisterStudyCreate, RegisterStudyUpdate>, RegisterStudyService>();

        services.AddTransient<IAuthService, AuthService>();

        services.AddTransient<ITokenService, TokenService>();

        services.AddTransient<IAccountService, AccountService>();

        return services;
    }
}