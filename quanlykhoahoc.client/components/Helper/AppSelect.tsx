import { MultiSelect, Select } from "@mantine/core";
import useSWR from "swr";
import {
  BillStatusClient,
  BillStatusMapping,
  CertificateClient,
  CertificateMapping,
  CourseClient,
  CourseMapping,
  CourseSubject,
  DistrictClient,
  DistrictMapping,
  PagingModelOfBillStatusMapping,
  PagingModelOfCertificateMapping,
  PagingModelOfCourseMapping,
  PagingModelOfDistrictMapping,
  PagingModelOfPracticeMapping,
  PagingModelOfProgramingLanguageMapping,
  PagingModelOfProvinceMapping,
  PagingModelOfSubjectDetailMapping,
  PagingModelOfSubjectMapping,
  PagingModelOfUserMapping,
  PagingModelOfWardMapping,
  PermissionMapping,
  PracticeClient,
  PracticeMapping,
  ProgramingLanguage,
  ProgramingLanguageClient,
  ProvinceClient,
  ProvinceMapping,
  RoleClient,
  SubjectClient,
  SubjectDetailClient,
  UserClient,
  UserMapping,
  WardClient,
  WardMapping,
} from "../../app/web-api-client";
import { useState } from "react";

function useFetchData<T>(url: string, fetcher: () => Promise<T>) {
  const { data, error } = useSWR(url, fetcher, {
    revalidateIfStale: false,
    revalidateOnFocus: false,
    revalidateOnReconnect: false,
  });

  return { data, error, isLoading: !data && !error };
}

interface SelectableProps<T> {
  label: string;
  placeholder: string;
  value?: T;
  onChange: any;
  serviceClient: any;
  fetchUrl: string;
  dataMapper: (data: any) => string[];
  valueMapper: (data: any) => string;
  idMapper: (data: any, selectedName: string) => any;
  entityFetchArgs?: any[];
  defaultValue?: any;
}

function Selectable<T>({
  label,
  placeholder,
  value,
  onChange,
  serviceClient,
  fetchUrl,
  dataMapper,
  valueMapper,
  idMapper,
  entityFetchArgs = [],
}: SelectableProps<T>) {
  const [shouldFetch, setShouldFetch] = useState(false);

  const { data } = useFetchData(shouldFetch ? fetchUrl : null, () =>
    serviceClient.getEntities(...entityFetchArgs)
  );

  const items = dataMapper(data) ?? [];

  return (
    <Select
      label={label}
      placeholder={placeholder}
      data={data ? items : value ? [valueMapper(value)] : null}
      onClick={() => setShouldFetch(true)}
      value={valueMapper(value)}
      onChange={(selectedName) => onChange(idMapper(data, selectedName))}
      searchable
      labelProps={{ style: { marginBottom: 6 } }}
    />
  );
}

export function SubjectSelect({
  value,
  onChange,
  single,
}: {
  value?: CourseSubject[] | CourseSubject;
  onChange: any;
  single?: boolean;
}) {
  const SubjectService = new SubjectClient();

  const { data } = useSWR("/api/subject", () =>
    SubjectService.getEntities(null, null, null, null)
  );

  return single ? (
    <Selectable
      label="Chủ Đề"
      placeholder="Chọn Chủ Đề"
      value={value}
      onChange={onChange}
      serviceClient={new SubjectClient()}
      fetchUrl="/api/subject"
      dataMapper={(data: PagingModelOfSubjectMapping) =>
        data?.items?.map((item) => item.name) ?? []
      }
      valueMapper={(item) => item?.name}
      idMapper={(data, selectedName) =>
        data?.items?.find((item) => item.name === selectedName)
      }
    />
  ) : (
    <MultiSelect
      label="Chủ Đề"
      placeholder="Chọn Chủ Đề"
      data={data?.items.map((item) => item.name)}
      value={(value as CourseSubject[])?.map((item) => item.subject.name)}
      onChange={(e) => {
        return onChange(
          e.map((item) => {
            var subject = data.items.find((c) => c.name === item);
            return { subjectId: subject.id, subject: subject };
          })
        );
      }}
    />
  );
}

export function SubjectDetailSelect({
  value,
  onChange,
}: {
  value?: CourseSubject[];
  onChange: any;
}) {
  return (
    <Selectable
      label="Chủ Chi Tiết Chủ Đề"
      placeholder="Chọn Chi Tiết Chủ Đề"
      value={value}
      onChange={onChange}
      serviceClient={new SubjectDetailClient()}
      fetchUrl="/api/subjectDetail"
      dataMapper={(data: PagingModelOfSubjectDetailMapping) =>
        data?.items?.map((item) => item.name) ?? []
      }
      valueMapper={(value) => value?.map((item) => item.subject?.name)}
      idMapper={(data, selectedName) =>
        data?.items?.find((item) => item.name === selectedName)?.id
      }
    />
  );
}

export function CertificateSelect({
  value,
  onChange,
}: {
  value?: CertificateMapping;
  onChange: any;
}) {
  return (
    <Selectable
      label="Chứng Chỉ"
      placeholder="Chọn Chứng Chỉ"
      value={value}
      onChange={onChange}
      serviceClient={new CertificateClient()}
      fetchUrl="/api/certificate"
      dataMapper={(data: PagingModelOfCertificateMapping) =>
        data?.items?.map((item) => item.name) ?? []
      }
      valueMapper={(value) => value?.name}
      idMapper={(data, selectedName) =>
        data?.items?.find((item) => item.name === selectedName)
      }
    />
  );
}

export function RoleSelect({
  value,
  onChange,
}: {
  value?: PermissionMapping[];
  onChange: any;
}) {
  const RoleService = new RoleClient();

  const { data } = useSWR("/api/role", () =>
    RoleService.getEntities(null, null, null, null)
  );

  return (
    <MultiSelect
      label="Vai Trò"
      placeholder="Chọn Vai Trò"
      data={data?.items.map((item) => item.roleName)}
      value={value?.map((item) => item.role.roleName)}
      onChange={(e) =>
        onChange(
          e.map((item) => ({
            role: data.items.find((c) => c.roleName === item),
          }))
        )
      }
    />
  );
}

export function PracticeSelect({
  value,
  onChange,
}: {
  value?: PracticeMapping[];
  onChange: any;
}) {
  return (
    <Selectable
      label="Chứng Chỉ"
      placeholder="Chọn Chứng Chỉ"
      value={value}
      onChange={onChange}
      serviceClient={new PracticeClient()}
      fetchUrl="/api/practice"
      dataMapper={(data: PagingModelOfPracticeMapping) =>
        data?.items?.map((item) => item.title) ?? []
      }
      valueMapper={(value) => value?.map((item) => item.certificateType?.title)}
      idMapper={(data, selectedName) =>
        data?.items?.find((item) => item.title === selectedName)?.id
      }
    />
  );
}

export function ProgramingLanguageSelect({
  value,
  onChange,
}: {
  value?: ProgramingLanguage[];
  onChange: any;
}) {
  const ProgramingLanguageService = new ProgramingLanguageClient();

  return (
    <Selectable
      label="Ngôn Ngữ Lập Trình"
      placeholder="Chọn Ngôn Ngữ Lập Trình"
      value={value}
      onChange={onChange}
      serviceClient={ProgramingLanguageService}
      fetchUrl="/api/programing-language"
      dataMapper={(data: PagingModelOfProgramingLanguageMapping) =>
        data?.items?.map((item) => item.languageName) ?? []
      }
      valueMapper={(value) => value?.map((item) => item.languageName)}
      idMapper={(data, selectedName) =>
        data?.items?.find((item) => item.languageName === selectedName)?.id
      }
    />
  );
}

export function ProvinceSelect({
  value,
  onChange,
}: {
  value?: ProvinceMapping;
  onChange: any;
}) {
  return (
    <Selectable
      label="Tỉnh / Thành Phố"
      placeholder="Chọn Tỉnh / Thành Phố"
      value={value}
      onChange={onChange}
      serviceClient={new ProvinceClient()}
      fetchUrl="/api/province"
      dataMapper={(data: PagingModelOfProvinceMapping) =>
        data?.items?.map((item) => item.name) ?? []
      }
      valueMapper={(item) => item?.name}
      idMapper={(data, selectedName) =>
        data?.items?.find((item) => item.name === selectedName)
      }
    />
  );
}

export function DistrictSelect({
  value,
  onChange,
  provinceId,
}: {
  value?: DistrictMapping;
  onChange: any;
  provinceId: number;
}) {
  return (
    <Selectable
      label="Quận / Huyện"
      placeholder="Chọn Quận / Huyện"
      value={value}
      onChange={onChange}
      serviceClient={new DistrictClient()}
      fetchUrl={`/api/district/${provinceId}`}
      dataMapper={(data: PagingModelOfDistrictMapping) =>
        data?.items?.map((item) => item.name) ?? []
      }
      entityFetchArgs={[provinceId]}
      valueMapper={(item) => item?.name}
      idMapper={(data, selectedName) =>
        data?.items?.find((item) => item.name === selectedName)?.id
      }
    />
  );
}

export function WardSelect({
  value,
  onChange,
  districtId,
}: {
  value?: WardMapping;
  onChange: any;
  districtId: number;
}) {
  return (
    <Selectable
      label="Xã / Phường"
      placeholder="Chọn Xã / Phường"
      value={value}
      onChange={onChange}
      serviceClient={new WardClient()}
      fetchUrl={`/api/ward/${districtId}`}
      dataMapper={(data: PagingModelOfWardMapping) =>
        data?.items?.map((item) => item.name) ?? []
      }
      entityFetchArgs={[districtId]}
      valueMapper={(item) => item?.name}
      idMapper={(data, selectedName) =>
        data?.items?.find((item) => item.name === selectedName)?.id
      }
    />
  );
}

export function UserSelect({
  value,
  onChange,
}: {
  value?: UserMapping;
  onChange: any;
}) {
  return (
    <Selectable
      label="Người Dùng"
      placeholder="Chọn Người Dùng"
      value={value}
      onChange={onChange}
      serviceClient={new UserClient()}
      fetchUrl="/api/user"
      dataMapper={(data: PagingModelOfUserMapping) =>
        data?.items?.map((item) => item.email) ?? []
      }
      valueMapper={(item) => item?.email}
      idMapper={(data, selectedName) =>
        data?.items?.find((item) => item.email === selectedName)
      }
    />
  );
}

export function BillStatusSelect({
  value,
  onChange,
}: {
  value?: BillStatusMapping;
  onChange: any;
}) {
  return (
    <Selectable
      label="Trạng Thái"
      placeholder="Chọn Trạng Thái"
      value={value}
      onChange={onChange}
      serviceClient={new BillStatusClient()}
      fetchUrl="/api/billStatus"
      dataMapper={(data: PagingModelOfBillStatusMapping) =>
        data?.items?.map((item) => item.name) ?? []
      }
      valueMapper={(item) => item?.name}
      idMapper={(data, selectedName) =>
        data?.items?.find((item) => item.name === selectedName)
      }
    />
  );
}

export function CourseSelect({
  value,
  onChange,
}: {
  value?: CourseMapping;
  onChange: any;
}) {
  return (
    <Selectable
      label="Khóa Học"
      placeholder="Chọn Khóa Học"
      value={value}
      onChange={onChange}
      serviceClient={new CourseClient()}
      fetchUrl="/api/course"
      dataMapper={(data: PagingModelOfCourseMapping) =>
        data?.items?.map((item) => item.name) ?? []
      }
      valueMapper={(item) => item?.name}
      idMapper={(data, selectedName) =>
        data?.items?.find((item) => item.name === selectedName)
      }
    />
  );
}
