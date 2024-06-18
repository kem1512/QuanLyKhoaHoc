"use client";

import { Grid, TextInput, NumberInput, Checkbox, Select } from "@mantine/core";
import DashboardLayout from "../../components/Layout/DashboardLayout/DashboardLayout";
import {
  PracticeClient,
  PracticeCreate,
  PracticeUpdate,
  IPracticeMapping,
  Level,
} from "../../app/web-api-client";
import { handleSubmit } from "../../lib/helper";
import { useEffect, useState } from "react";
import useSWR from "swr";
import ActionButton from "../../components/Helper/ActionButton";
import {
  ProgramingLanguageSelect,
  SubjectDetailSelect,
} from "../Helper/AppSelect";

export default function PracticeHandler({ id }: { id?: number }) {
  const PracticeService = new PracticeClient();

  const { data, mutate } = useSWR(
    `/api/course/${id}`,
    () => PracticeService.getEntity(id),
    {
      revalidateIfStale: false,
      revalidateOnFocus: false,
      revalidateOnReconnect: false,
    }
  );

  const [practice, setPractice] = useState<IPracticeMapping>({
    title: "",
    practiceCode: "",
    topic: "",
    expectOutput: "",
    mediumScore: 0,
    subjectDetailId: 0,
    languageProgrammingId: 0,
    isDeleted: false,
    isRequired: true,
    level: Level.Beginner,
  });

  useEffect(() => {
    if (data) setPractice(data);
  }, [data]);

  return (
    <DashboardLayout>
      <Grid>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Tiêu Đề"
            placeholder="Nhập Tiêu Đề"
            value={practice.title}
            onChange={(e) =>
              setPractice((prev) => ({ ...prev, title: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Mã Bài Tập"
            placeholder="Nhập Mã Bài Tập"
            value={practice.practiceCode}
            onChange={(e) =>
              setPractice((prev) => ({ ...prev, practiceCode: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Chủ Đề"
            placeholder="Nhập Tên Chủ Đề"
            value={practice.topic}
            onChange={(e) =>
              setPractice((prev) => ({ ...prev, topic: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <SubjectDetailSelect
            onChange={(e) => {
              setPractice((prev) => ({ ...prev, subjectDetailId: e }));
            }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <ProgramingLanguageSelect
            onChange={(e) => {
              setPractice((prev) => ({ ...prev, languageProgrammingId: e }));
            }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Kết Quả Mong Đợi"
            placeholder="Nhập Kết Quả Mong Đợi"
            value={practice.expectOutput}
            onChange={(e) =>
              setPractice((prev) => ({ ...prev, expectOutput: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <NumberInput
            label="Điểm Trung Bình"
            placeholder="Nhập Điểm Trung Bình"
            value={practice.mediumScore}
            onChange={(value: number) =>
              setPractice((prev) => ({ ...prev, mediumScore: value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <Select
            label="Cấp Độ"
            placeholder="Chọn Cấp Độ"
            value={practice.level}
            onChange={(value: Level) =>
              setPractice((prev) => ({ ...prev, level: value }))
            }
            data={[
              { value: Level.Beginner, label: "Beginner" },
              { value: Level.Intermediate, label: "Intermediate" },
              { value: Level.Advanced, label: "Advanced" },
            ]}
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <Checkbox
            label="Đã Xóa"
            checked={practice.isDeleted}
            onChange={(e) =>
              setPractice((prev) => ({
                ...prev,
                isDeleted: e.currentTarget.checked,
              }))
            }
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <Checkbox
            label="Bắt Buộc"
            checked={practice.isRequired}
            onChange={(e) =>
              setPractice((prev) => ({
                ...prev,
                isRequired: e.currentTarget.checked,
              }))
            }
          />
        </Grid.Col>
        <Grid.Col span={12}>
          <ActionButton
            size="xs"
            action={() =>
              handleSubmit(() => {
                return id
                  ? PracticeService.updateEntity(
                      practice.id,
                      practice as PracticeUpdate
                    )
                  : PracticeService.createEntity(practice as PracticeCreate);
              }, `${id ? "Sửa" : "Thêm"} Thành Công`, mutate)
            }
          >
            Xác Nhận
          </ActionButton>
        </Grid.Col>
      </Grid>
    </DashboardLayout>
  );
}
