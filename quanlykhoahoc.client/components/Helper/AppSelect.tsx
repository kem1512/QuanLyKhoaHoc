import { MultiSelect, Select } from "@mantine/core";
import useSWR from "swr";
import {
  CertificateClient,
  CertificateMapping,
  CertificateTypeClient,
  CourseSubject,
  DistrictClient,
  DistrictMapping,
  PagingModelOfCertificateTypeMapping,
  PagingModelOfCourseMapping,
  PagingModelOfDistrictMapping,
  PagingModelOfPracticeMapping,
  PagingModelOfProgramingLanguageMapping,
  PagingModelOfProvinceMapping,
  PagingModelOfSubjectDetailMapping,
  PagingModelOfWardMapping,
  PracticeClient,
  PracticeMapping,
  ProgramingLanguage,
  ProgramingLanguageClient,
  ProvinceClient,
  ProvinceMapping,
  RoleClient,
  RoleMapping,
  SubjectClient,
  SubjectDetailClient,
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
      defaultValue={valueMapper(value)}
      onChange={(selectedName) => onChange(idMapper(data, selectedName))}
      searchable
      labelProps={{ style: { marginBottom: 6 } }}
    />
  );
}

export function SubjectSelect({
  value,
  onChange,
}: {
  value?: CourseSubject[];
  onChange: any;
}) {
  const SubjectService = new SubjectClient();

  return (
    <Selectable
      label="Chủ Đề"
      placeholder="Chọn Chủ Đề"
      value={value}
      onChange={onChange}
      serviceClient={SubjectService}
      fetchUrl="/api/subject"
      dataMapper={(data: PagingModelOfCourseMapping) =>
        data?.items?.map((item) => item.name) ?? []
      }
      valueMapper={(value) => value?.map((item) => item.subject?.name)}
      idMapper={(data, selectedName) =>
        data?.items?.find((item) => item.name === selectedName)?.id
      }
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
      label="Chủ Chi Tiết Đề"
      placeholder="Chọn Chi Tiết Đề"
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
  value?: CertificateMapping[];
  onChange: any;
}) {
  return (
    <Selectable
      label="Loại Chứng Chỉ"
      placeholder="Chọn Loại Chứng Chỉ"
      value={value}
      onChange={onChange}
      serviceClient={new CertificateTypeClient()}
      fetchUrl="/api/certificateType"
      dataMapper={(data: PagingModelOfCertificateTypeMapping) =>
        data?.items?.map((item) => item.name) ?? []
      }
      valueMapper={(value) => value?.map((item) => item.certificateType?.name)}
      idMapper={(data, selectedName) =>
        data?.items?.find((item) => item.name === selectedName)?.id
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
      label="Chủ Ngôn Ngữ Lập Trình"
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
      valueMapper={(item) => item.name}
      idMapper={(data, selectedName) =>
        data?.items?.find((item) => item.name === selectedName)?.id
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
      valueMapper={(item) => item.name}
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
  value?: DistrictMapping;
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
      valueMapper={(item) => item.name}
      idMapper={(data, selectedName) =>
        data?.items?.find((item) => item.name === selectedName)?.id
      }
    />
  );
}
