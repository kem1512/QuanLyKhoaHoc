namespace QuanLyKhoaHoc.Application
{
    public class ApplicationMapping : Profile
    {
        public ApplicationMapping()
        {
            CreateMap<Subject, SubjectMapping>().ReverseMap();
            CreateMap<Subject, SubjectCreate>().ReverseMap();
            CreateMap<Subject, SubjectUpdate>().ReverseMap();

            CreateMap<Course, CourseMapping>().ReverseMap();
            CreateMap<Course, CourseCreate>().ReverseMap();
            CreateMap<Course, CourseUpdate>().ReverseMap();

            CreateMap<Blog, BlogMapping>().ReverseMap();
            CreateMap<Blog, BlogCreate>().ReverseMap();
            CreateMap<Blog, BlogUpdate>().ReverseMap();

            CreateMap<Province, ProvinceMapping>().ReverseMap();
            CreateMap<Province, ProvinceCreate>().ReverseMap();
            CreateMap<Province, ProvinceUpdate>().ReverseMap();

            CreateMap<District, DistrictMapping>().ReverseMap();
            CreateMap<District, DistrictCreate>().ReverseMap();
            CreateMap<District, DistrictUpdate>().ReverseMap();

            CreateMap<Ward, WardMapping>().ReverseMap();
            CreateMap<Ward, WardCreate>().ReverseMap();
            CreateMap<Ward, WardUpdate>().ReverseMap();

            CreateMap<User, UserMapping>().ReverseMap();
            CreateMap<User, UserCreate>().ReverseMap();
            CreateMap<User, UserUpdate>().ReverseMap();

            CreateMap<Role, RoleMapping>().ReverseMap();
            CreateMap<Role, RoleCreate>().ReverseMap();
            CreateMap<Role, RoleUpdate>().ReverseMap();

            CreateMap<SubjectDetail, SubjectDetailMapping>().ReverseMap();
            CreateMap<SubjectDetail, SubjectDetailCreate>().ReverseMap();
            CreateMap<SubjectDetail, SubjectDetailUpdate>().ReverseMap();

            CreateMap<TestCase, TestCaseMapping>().ReverseMap();
            CreateMap<TestCase, TestCaseCreate>().ReverseMap();
            CreateMap<TestCase, TestCaseUpdate>().ReverseMap();

            CreateMap<ProgramingLanguage, ProgramingLanguageMapping>().ReverseMap();
            CreateMap<ProgramingLanguage, ProgramingLanguageCreate>().ReverseMap();
            CreateMap<ProgramingLanguage, ProgramingLanguageUpdate>().ReverseMap();

            CreateMap<Certificate, CertificateMapping>().ReverseMap();
            CreateMap<Certificate, CertificateCreate>().ReverseMap();
            CreateMap<Certificate, CertificateUpdate>().ReverseMap();

            CreateMap<Practice, PracticeMapping>().ReverseMap();
            CreateMap<Practice, PracticeCreate>().ReverseMap();
            CreateMap<Practice, PracticeUpdate>().ReverseMap();

            CreateMap<LikeBlog, LikeBlogMapping>().ReverseMap();
            CreateMap<LikeBlog, LikeBlogCreate>().ReverseMap();
            CreateMap<LikeBlog, LikeBlogUpdate>().ReverseMap();

            CreateMap<Bill, BillMapping>().ReverseMap();
            CreateMap<Bill, BillCreate>().ReverseMap();
            CreateMap<Bill, BillUpdate>().ReverseMap();

            CreateMap<BillStatus, BillStatusMapping>().ReverseMap();
            CreateMap<BillStatus, BillStatusCreate>().ReverseMap();
            CreateMap<BillStatus, BillStatusUpdate>().ReverseMap();

            CreateMap<BillStatus, BillStatusMapping>().ReverseMap();

            CreateMap<CommentBlog, CommentBlogMapping>().ReverseMap();
            CreateMap<CommentBlog, CommentBlogCreate>().ReverseMap();
            CreateMap<CommentBlog, CommentBlogUpdate>().ReverseMap();

            CreateMap<RegisterStudy, RegisterStudyMapping>().ReverseMap();
            CreateMap<RegisterStudy, RegisterStudyCreate>().ReverseMap();
            CreateMap<RegisterStudy, RegisterStudyUpdate>().ReverseMap();

            CreateMap<CertificateType, CertificateTypeMapping>().ReverseMap();
            CreateMap<CertificateType, CertificateTypeCreate>().ReverseMap();
            CreateMap<CertificateType, CertificateTypeUpdate>().ReverseMap();

            CreateMap<CourseSubject, CourseSubjectMapping>().ReverseMap();

            CreateMap<Permission, PermissionMapping>().ReverseMap();

            CreateMap<User, UserInfo>().ReverseMap();
            CreateMap<User, UserInfoUpdate>().ReverseMap();
        }
    }
}
