"use client";

import { Checkbox, SimpleGrid, TextInput } from "@mantine/core";
import DashboardLayout from "../../../../components/Layout/DashboardLayout";
import {
  ISubjectCreate,
  SubjectClient,
  SubjectCreate,
} from "../../../web-api-client";
import { handleSubmit } from "../../../../lib/helper";
import { useState } from "react";
import ActionButton from "../../../../components/Helper/ActionButton";

export default function DashboardSubjectCreate() {
  const SubjectService = new SubjectClient();

  const [subject, setSubject] = useState<ISubjectCreate>({
    name: "",
    symbol: "",
    isActive: true,
  });

  return (
    <DashboardLayout>
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
              setSubject({
                name: "",
                symbol: "",
                isActive: true,
              });
              return SubjectService.createEntity(subject as SubjectCreate);
            }, "Thêm Thành Công")
          }
        >
          Xác Nhận
        </ActionButton>
      </SimpleGrid>
    </DashboardLayout>
  );
}
