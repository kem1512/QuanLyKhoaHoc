"use client";

import { Alert, Checkbox, SimpleGrid, TextInput } from "@mantine/core";
import DashboardLayout from "../../../../components/Layout/DashboardLayout";
import {
  ISubjectUpdate,
  SubjectClient,
  SubjectUpdate,
} from "../../../web-api-client";
import { handleSubmit } from "../../../../lib/helper";
import { useEffect, useState } from "react";
import useSWR from "swr";
import ActionButton from "../../../../components/Helper/ActionButton";

export default function DashboardSubjectUpdate({
  params,
}: {
  params: { id: number };
}) {
  const { id } = params;

  const SubjectService = new SubjectClient();

  const { data } = useSWR(`/api/subject/${id}`, () =>
    SubjectService.getEntity(id)
  );

  const [subject, setSubject] = useState<ISubjectUpdate>({
    name: "",
    symbol: "",
    isActive: true,
  });

  useEffect(() => {
    if (data) setSubject(data);
  }, [data]);

  return (
    <DashboardLayout>
      {data ? (
        <SimpleGrid cols={{ base: 1, lg: 2 }}>
          <TextInput
            label="Tên Chủ Đề"
            placeholder="Nhập Tên Chủ Đề"
            value={subject.name}
            onChange={(e) =>
              setSubject((prev) => ({ ...prev, name: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
          <TextInput
            label="Biểu Tượng"
            placeholder="Nhập Biểu Tượng"
            value={subject.symbol}
            onChange={(e) =>
              setSubject((prev) => ({ ...prev, symbol: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
          <Checkbox
            size="sm"
            label="Kích Hoạt"
            checked={subject.isActive}
            onChange={() =>
              setSubject((prev) => ({ ...prev, isActive: !subject.isActive }))
            }
          />
          <ActionButton
            size="xs"
            ms={"auto"}
            action={() =>
              handleSubmit(() => {
                return SubjectService.updateEntity(
                  subject.id,
                  subject as SubjectUpdate
                );
              }, "Cập Nhật Thành Công")
            }
          >
            Xác Nhận
          </ActionButton>
        </SimpleGrid>
      ) : (
        <Alert>Không Có Gì Ở Đây Cả</Alert>
      )}
    </DashboardLayout>
  );
}
