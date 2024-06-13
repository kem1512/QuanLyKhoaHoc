"use client";

import { Grid, NumberInput, TextInput } from "@mantine/core";
import DashboardLayout from "../../components/Layout/DashboardLayout/DashboardLayout";
import {
  CourseClient,
  CourseUpdate,
  CourseCreate,
  ICourseMapping,
} from "../../app/web-api-client";
import { handleSubmit } from "../../lib/helper";
import { useEffect, useState } from "react";
import useSWR from "swr";
import { SubjectSelect } from "../../components/Helper/AppSelect";
import ActionButton from "../../components/Helper/ActionButton";
import Editor from "../Editor/RichTextEditor/RichTextEditor";

export default function CourseHandler({ id }: { id?: number }) {
  const CourseService = new CourseClient();

  const { data } = useSWR(
    `/api/course/${id}`,
    () => CourseService.getEntity(id),
    {
      revalidateIfStale: false,
      revalidateOnFocus: false,
      revalidateOnReconnect: false,
    }
  );

  const [course, setCourse] = useState<ICourseMapping>({
    code: "",
    imageCourse: "",
    introduce: "",
    name: "",
    creatorId: 0,
    id: 0,
    numberOfPurchases: 0,
    numberOfStudent: 0,
    price: 0,
    totalCourseDuration: 0,
    courseSubjects: [],
  });

  useEffect(() => {
    if (data) setCourse(data);
  }, [data]);

  return (
    <DashboardLayout>
      <Grid>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Tên Khóa Học"
            placeholder="Nhập Tên Khóa Học"
            value={course.name}
            onChange={(e) =>
              setCourse((prev) => ({ ...prev, name: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Mã Khóa Học"
            placeholder="Nhập Mã Khóa Học"
            value={course.code}
            onChange={(e) =>
              setCourse((prev) => ({ ...prev, code: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <SubjectSelect
            value={course.courseSubjects}
            onChange={(e: []) =>
              setCourse((prev) => ({
                ...prev,
                courseSubjects: e,
              }))
            }
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Ảnh Khóa Học"
            placeholder="Nhập Đường Dẫn Ảnh"
            value={course.imageCourse}
            onChange={(e) =>
              setCourse((prev) => ({ ...prev, imageCourse: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <NumberInput
            label="Số Người Mua"
            placeholder="Nhập Số Người Mua"
            value={course.numberOfPurchases}
            onChange={(e: number) =>
              setCourse((prev) => ({
                ...prev,
                numberOfPurchases: e,
              }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <NumberInput
            label="Số Lượng Học Sinh"
            placeholder="Nhập Số Lượng Học Sinh"
            value={course.numberOfStudent}
            onChange={(e: number) =>
              setCourse((prev) => ({
                ...prev,
                numberOfStudent: e,
              }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <NumberInput
            label="Giá"
            placeholder="Nhập Giá"
            value={course.price}
            onChange={(e: number) =>
              setCourse((prev) => ({ ...prev, price: e }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <NumberInput
            label="Tổng Thời Lượng Khóa Học"
            placeholder="Nhập Tổng Thời Lượng"
            value={course.totalCourseDuration}
            onChange={(e: number) =>
              setCourse((prev) => ({
                ...prev,
                totalCourseDuration: e,
              }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={12}>
          <Editor
            content={course.introduce}
            onChange={(e) => setCourse((prev) => ({ ...prev, introduce: e }))}
          />
        </Grid.Col>
        <Grid.Col span={12}>
          <ActionButton
            size="xs"
            action={() =>
              handleSubmit(() => {
                return id
                  ? CourseService.updateEntity(
                      course.id,
                      course as CourseUpdate
                    )
                  : CourseService.createEntity((course) as CourseCreate);
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
