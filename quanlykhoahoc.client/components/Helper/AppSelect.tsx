import { MultiSelect, Select } from "@mantine/core";
import useSWR from "swr";
import {
  CourseSubjectMapping,
  DistrictClient,
  DistrictMapping,
  ProvinceClient,
  ProvinceMapping,
  SubjectClient,
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

export function SubjectSelect({ val, onChange }: { val?: any; onChange: any }) {
  const SubjectService = new SubjectClient();
  const { data, error, isLoading } = useFetchData("/api/subject", () =>
    SubjectService.getEntities(null, null, null, null)
  );

  if (isLoading) return <div>Loading...</div>;
  if (error) return <div>Error loading subjects</div>;

  const subjects = data?.items?.map((item) => item.name).filter(Boolean) ?? [];

  return (
    <MultiSelect
      label="Chủ Đề"
      placeholder="Chọn Chủ Đề"
      data={subjects}
      defaultValue={
        val
          ?.map((item: CourseSubjectMapping) => item.subject?.name)
          .filter(Boolean) ?? []
      }
      onChange={(selectedNames) =>
        onChange(
          selectedNames.map((name) => ({
            SubjectId: data?.items?.find((item) => item.name === name)?.id,
          }))
        )
      }
      searchable
      labelProps={{ style: { marginBottom: 6 } }}
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
  const [shouldFetch, setShouldFetch] = useState(false);

  const ProvinceService = new ProvinceClient();

  const { data } = useFetchData(shouldFetch ? "/api/province" : null, () =>
    ProvinceService.getEntities(null, null, null, null)
  );

  const provinces = data?.items?.map((item) => item.name).filter(Boolean) ?? [];

  return (
    <Select
      label="Tỉnh / Thành Phố"
      placeholder="Chọn Thành Phố"
      data={data ? provinces : value ? [value.name] : null}
      onClick={() => setShouldFetch(true)}
      defaultValue={value?.name}
      onChange={(e) => onChange(data?.items?.find((c) => c.name === e)?.id)}
      searchable
      labelProps={{ style: { marginBottom: 6 } }}
    />
  );
}

export function DistrictSelect({
  value,
  provinceId,
  onChange,
}: {
  value: DistrictMapping;
  provinceId: number;
  onChange: any;
}) {
  const [shouldFetch, setShouldFetch] = useState(false);

  const DistrictService = new DistrictClient();

  const { data } = useFetchData(
    shouldFetch ? `/api/district/${provinceId}` : null,
    () => DistrictService.getEntities(provinceId, null, null, null, null)
  );

  const districts = data?.items?.map((item) => item.name).filter(Boolean) ?? [];

  return (
    <Select
      label="Quận / Huyện"
      placeholder="Chọn Quận / Huyện"
      data={data ? districts : [value.name]}
      onClick={() => setShouldFetch(true)}
      defaultValue={value.name}
      onChange={(e) => onChange(data?.items?.find((c) => c.name === e)?.id)}
      searchable
      labelProps={{ style: { marginBottom: 6 } }}
    />
  );
}

export function WardSelect({
  value,
  districtId,
  onChange,
}: {
  value: WardMapping;
  districtId: number;
  onChange: any;
}) {
  const [shouldFetch, setShouldFetch] = useState(false);

  const WardService = new WardClient();

  const { data } = useFetchData(
    shouldFetch ? `/api/ward/${districtId}` : null,
    () => WardService.getEntities(districtId, null, null, null, null)
  );

  const wards = data?.items?.map((item) => item.name).filter(Boolean) ?? [];

  return (
    <Select
      label="Xã / Phường"
      placeholder="Chọn Xã / Phường"
      data={data ? wards : [value.name]}
      onClick={() => setShouldFetch(true)}
      defaultValue={value.name}
      onChange={(e) => onChange(data?.items?.find((c) => c.name === e)?.id)}
      searchable
      labelProps={{ style: { marginBottom: 6 } }}
    />
  );
}
