"use client";

import { Checkbox, Grid, TextInput } from "@mantine/core";
import DashboardLayout from "../../components/Layout/DashboardLayout/DashboardLayout";
import {
  SubjectDetailClient,
  SubjectDetailCreate,
  SubjectDetailUpdate,
  ISubjectDetailMapping,
} from "../../app/web-api-client";
import { handleSubmit } from "../../lib/helper";
import { useEffect, useState } from "react";
import useSWR from "swr";
import ActionButton from "../../components/Helper/ActionButton";
import { SubjectSelect } from "../Helper/AppSelect";

export default function SubjectDetailHandler({ id }: { id?: number }) {
  const SubjectDetailService = new SubjectDetailClient();

  const { data, mutate } = useSWR(
    `/api/course/${id}`,
    () => SubjectDetailService.getEntity(id),
    {
      revalidateIfStale: false,
      revalidateOnFocus: false,
      revalidateOnReconnect: false,
    }
  );

  const [subjectDetail, setSubjectDetail] = useState<ISubjectDetailMapping>({
    name: "",
    linkVideo: "",
    isActive: true,
    isFinished: true,
    subjectId: 0,
  });

  useEffect(() => {
    if (data) setSubjectDetail(data);
  }, [data]);

  return (
    <DashboardLayout>
      <Grid>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Tiêu Đề"
            placeholder="Nhập Tiêu Đề"
            value={subjectDetail.name}
            onChange={(e) =>
              setSubjectDetail((prev) => ({ ...prev, name: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Đường Dẫn Video"
            placeholder="Nhập Đường Dẫn Video"
            value={subjectDetail.linkVideo}
            onChange={(e) =>
              setSubjectDetail((prev) => ({
                ...prev,
                linkVideo: e.target.value,
              }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <SubjectSelect
            single={true}
            value={subjectDetail.subject}
            onChange={(e) => {
              setSubjectDetail((prev) => ({
                ...prev,
                subjectId: e.id,
                subject: e,
              }));
            }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <Checkbox
            label="Kích Hoạt"
            checked={subjectDetail.isActive}
            onChange={(e) =>
              setSubjectDetail((prev) => ({
                ...prev,
                isActive: e.currentTarget.checked,
              }))
            }
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <Checkbox
            label="Đã Hoàn Thành"
            checked={subjectDetail.isFinished}
            onChange={(e) =>
              setSubjectDetail((prev) => ({
                ...prev,
                isFinished: e.currentTarget.checked,
              }))
            }
          />
        </Grid.Col>
        <Grid.Col span={12}>
          <ActionButton
            size="xs"
            action={() =>
              handleSubmit(
                () => {
                  return id
                    ? SubjectDetailService.updateEntity(
                        subjectDetail.id,
                        subjectDetail as SubjectDetailUpdate
                      )
                    : SubjectDetailService.createEntity(
                        subjectDetail as SubjectDetailCreate
                      );
                },
                `${id ? "Sửa" : "Thêm"} Thành Công`,
                mutate
              )
            }
          >
            Xác Nhận
          </ActionButton>
        </Grid.Col>
      </Grid>
    </DashboardLayout>
  );
}
