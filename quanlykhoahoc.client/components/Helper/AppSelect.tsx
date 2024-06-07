import { MultiSelect } from "@mantine/core";
import useSWR from "swr";
import { CourseSubjectMapping, SubjectClient } from "../../app/web-api-client";

export function SubjectSelect({ val, onChange }: { val?: any; onChange: any }) {
  const SubjectService = new SubjectClient();

  const { data } = useSWR(
    "/api/tags",
    () => SubjectService.getSubjects(null, null, null, null),
    {
      revalidateIfStale: false,
      revalidateOnFocus: false,
      revalidateOnReconnect: false,
    }
  );

  if (!data || !data.items) {
    return null;
  }

  return (
    <MultiSelect
      label="Chủ Đề"
      placeholder="Chọn Tối Thiểu 1 Chủ Đề. Tối Đa 10 Chủ Đề"
      data={data.items
        .map((item) => item.name)
        .filter((name): name is string => name !== undefined)}
      defaultValue={
        val &&
        Array.from<CourseSubjectMapping>(val)
          .map((item) => item.subject?.name)
          .filter((name): name is string => name !== undefined)
      }
      onChange={(selectedNames) =>
        onChange(
          selectedNames.map((name) => ({
            SubjectId: data.items?.find((tag) => tag.name === name)?.id,
          }))
        )
      }
      searchable
      labelProps={{ style: { marginBottom: 6 } }}
    />
  );
}
