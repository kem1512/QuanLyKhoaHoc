import { MultiSelect, Select } from "@mantine/core";
import useSWR from "swr";
import {
  CourseSubject,
  DistrictClient,
  DistrictMapping,
  ProvinceClient,
  ProvinceMapping,
  RoleClient,
  RoleMapping,
  SubjectClient,
  WardClient,
  WardMapping,
} from "../../app/web-api-client";
import { useEffect, useMemo, useState } from "react";

function useFetchData<T>(url: string, fetcher: () => Promise<T>) {
  const { data, error } = useSWR(url, fetcher, {
    revalidateIfStale: false,
    revalidateOnFocus: false,
    revalidateOnReconnect: false,
  });

  return { data, error, isLoading: !data && !error };
}

export function SubjectSelect({
  value,
  onChange,
}: {
  value?: CourseSubject[];
  onChange: any;
}) {
  const [shouldFetch, setShouldFetch] = useState(false);

  const SubjectService = new SubjectClient();

  const { data } = useFetchData(shouldFetch ? "/api/subject" : null, () =>
    SubjectService.getEntities(null, null, null, null)
  );

  const subjects = data?.items?.map((item) => item.name) ?? [];

  return (
    <MultiSelect
      label="Chủ Đề"
      placeholder="Chọn Chủ Đề"
      data={
        data ? subjects : value ? value.map((item) => item.subject.name) : null
      }
      onClick={() => setShouldFetch(true)}
      defaultValue={value?.map((item) => item.subject?.name)}
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

  const provinces = data?.items?.map((item) => item.name) ?? [];

  return (
    <>
      <Select
        label="Tỉnh / Thành Phố"
        placeholder="Chọn Thành Phố"
        data={data ? provinces : value ? [value.name] : null}
        onClick={() => setShouldFetch(true)}
        defaultValue={value?.name}
        onChange={(e) => onChange(data.items?.find((c) => c.name === e)?.id)}
        searchable
        labelProps={{ style: { marginBottom: 6 } }}
      />
    </>
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

  const districts = data?.items?.map((item) => item.name) ?? [];

  return (
    <Select
      label="Quận / Huyện"
      placeholder="Chọn Quận / Huyện"
      data={data ? districts : value ? [value.name] : []}
      onClick={() => setShouldFetch(true)}
      defaultValue={value?.name}
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

  const wards = data?.items?.map((item) => item.name) ?? [];

  return (
    <Select
      label="Xã / Phường"
      placeholder="Chọn Xã / Phường"
      data={data ? wards : value ? [value.name] : null}
      onClick={() => setShouldFetch(true)}
      defaultValue={value?.name}
      onChange={(e) => onChange(data?.items?.find((c) => c.name === e)?.id)}
      searchable
      labelProps={{ style: { marginBottom: 6 } }}
    />
  );
}

export function RoleSelect({
  value,
  onChange,
}: {
  value?: RoleMapping;
  onChange: any;
}) {
  const [shouldFetch, setShouldFetch] = useState(false);

  const RoleService = new RoleClient();

  const { data } = useFetchData(shouldFetch ? "/api/role" : null, () =>
    RoleService.getEntities(null, null, null, null)
  );

  const roles = data?.items?.map((item) => item.roleName) ?? [];

  return (
    <Select
      label="Chọn Vai Trò"
      placeholder="Chọn Vai Trò"
      data={data ? roles : value ? [value.roleName] : null}
      onClick={() => setShouldFetch(true)}
      defaultValue={value?.roleName}
      onChange={(e) => onChange(data?.items?.find((c) => c.roleName === e)?.id)}
      searchable
      labelProps={{ style: { marginBottom: 6 } }}
    />
  );
}
