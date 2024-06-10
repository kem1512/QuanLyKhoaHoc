"use client";

import { Checkbox, Grid, TextInput } from "@mantine/core";
import DashboardLayout from "../../components/Layout/DashboardLayout/DashboardLayout";
import {
  ISubjectMapping,
  SubjectClient,
  SubjectCreate,
  SubjectUpdate,
} from "../../app/web-api-client";
import { handleSubmit } from "../../lib/helper";
import { useEffect, useState } from "react";
import useSWR from "swr";
import ActionButton from "../../components/Helper/ActionButton";

export default function SubjectHandler({ id }: { id?: number }) {
  const SubjectService = new SubjectClient();

  const { data } = useSWR(`/api/subject/${id}`, () =>
    SubjectService.getEntity(id)
  );

  const [subject, setSubject] = useState<ISubjectMapping>({
    name: "",
    symbol: "",
    isActive: true,
  });

  useEffect(() => {
    if (data) setSubject(data);
  }, [data]);

  return (
    <DashboardLayout>
      <Grid>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Tên Chủ Đề"
            placeholder="Nhập Tên Chủ Đề"
            value={subject.name}
            onChange={(e) =>
              setSubject((prev) => ({ ...prev, name: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Biểu Tượng"
            placeholder="Nhập Biểu Tượng"
            value={subject.symbol}
            onChange={(e) =>
              setSubject((prev) => ({ ...prev, symbol: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <Checkbox
            size="sm"
            label="Kích Hoạt"
            checked={subject.isActive}
            onChange={() =>
              setSubject((prev) => ({ ...prev, isActive: !subject.isActive }))
            }
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <ActionButton
            size="xs"
            action={() =>
              handleSubmit(() => {
                return id
                  ? SubjectService.updateEntity(
                      subject.id,
                      subject as SubjectUpdate
                    )
                  : SubjectService.createEntity(subject as SubjectCreate);
              }, `${id ? "Sửa" : "Thêm"} Thành Công`)
            }
          >
            Xác Nhận
          </ActionButton>
        </Grid.Col>
      </Grid>
    </DashboardLayout>
  );
}
